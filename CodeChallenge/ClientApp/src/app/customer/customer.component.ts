import { HttpClient } from '@angular/common/http';
import { Component, OnInit, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { Customer } from './Customer';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: []
})
export class CustomerComponent implements OnInit {
  customers: Customer[] = []; // Define the customer array
  base: string = "";

  // Pagination properties
  currentPage: number = 1;
  itemsPerPage: number = 10; // Number of items to display per page

  constructor(private http: HttpClient, private router: Router, @Inject('BASE_URL') baseUrl: string) {
    this.base = baseUrl;
  }

  ngOnInit(): void {
    // Fetch the list of customers when the component is initialized
    this.getCustomers();
  }

  getCustomers(): void {
    // Make an HTTP GET request to retrieve the list of customers
    this.http.get<Customer[]>(this.base + 'customer').subscribe((data) => {
      this.customers = data;
    });
  }

  addCustomer(): void {
    // Navigate to the add customer page
    this.router.navigate(['/customers/add']);
  }

  editCustomer(customerId: string): void {
    // Navigate to the edit customer page with the customer's ID
    this.router.navigate(['/customers/edit', customerId]);
  }

  deleteCustomer(customerId: string): void {
    // Make an HTTP DELETE request to delete the customer
    this.http.delete(this.base + `customer/DeleteCustomer/${customerId}`).subscribe(() => {
      // Refresh the list of customers after deletion
      this.getCustomers();
    });
  }
}
