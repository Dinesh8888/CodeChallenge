import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Customer } from '../customer/Customer';

@Component({
  selector: 'app-customer-form',
  templateUrl: './customer-form.component.html',
  styleUrls: ['./customer-form.component.css']
})

export class CustomerFormComponent implements OnInit {
  customerForm!: FormGroup; // The form group to manage form controls
  isEditing: boolean = false; // Indicates whether we are editing an existing customer
  customerId: string | null = null; // ID of the customer being edited
  base: string = "";

  constructor(private http: HttpClient,
    private formBuilder: FormBuilder,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    @Inject('BASE_URL') baseUrl: string
  ) {
    this.base = baseUrl;
  }

  ngOnInit(): void {
    this.customerForm = this.formBuilder.group({
      id: ['', Validators.required],
      firstname: ['', Validators.required],
      lastname: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phone_Number: [''],
      country_code: [''],
      gender: [''],
      balance: [0, Validators.min(0)], // Assuming balance cannot be negative
      currency: ['']
    });

    // Check if we are in edit mode (URL contains an ID)
    this.customerId = this.activatedRoute.snapshot.params['id'];
    if (this.customerId) {
      this.isEditing = true;
      this.http.get<Customer>(this.base + 'customer/GetCustomerById/' + this.customerId).subscribe((data) => {
        this.customerForm = this.formBuilder.group({
          id: data.id,
          firstname: data.firstname,
          lastname: data.lastname,
          email: data.email,
          phone_Number: data.phone_Number,
          country_code: data.country_code,
          gender: data.gender,
          balance: data.balance,
          currency: data.currency
        });
      });
    }
  }

  saveCustomer(): void {
    if (this.customerForm.valid) {
      const formData: Customer = this.customerForm.value;

      if (this.customerId) {
        this.http.post(this.base + 'customer/updatecustomer/' + this.customerId, formData).subscribe(() => {
          setTimeout(() => { this.router.navigate(['']); }, 2000);
        });
      }
      else {
        this.http.post(this.base + 'customer/createcustomer', formData).subscribe(() => {
          setTimeout(() => { this.router.navigate(['']); }, 2000);
        });
      }
      
    } else {
      this.markFormGroupTouched(this.customerForm);
    }
  }

  // Helper method to mark all form controls as touched to trigger validation messages
  private markFormGroupTouched(formGroup: FormGroup): void {
    Object.values(formGroup.controls).forEach(control => {
      control.markAsTouched();

      if (control instanceof FormGroup) {
        this.markFormGroupTouched(control);
      }
    });
  }
}
