using CodeChallenge.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeChallenge.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(IHttpClientFactory httpClientFactory, ILogger<CustomerController> logger)
        {
            // Inject an instance of HttpClient using IHttpClientFactory
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://getinvoices.azurewebsites.net/");
            _logger = logger;
        }

        [HttpGet("CreateCustomerList")]
        public async Task<IActionResult> CreateCustomerList()
        {
            try
            {
                // Make a GET request to the CreateCustomerList API
                HttpResponseMessage response = await _httpClient.GetAsync("api/CreateCustomerList");
                response.EnsureSuccessStatusCode(); // Ensure a successful response

                // Log success
                _logger.LogInformation("CreateCustomerList API called successfully.");

                // For example, you can return a success message
                return Ok("Customer list created successfully");
            }
            catch (Exception ex)
            {
                // Log error
                _logger.LogError(ex, "Error calling CreateCustomerList API.");


                // Handle exceptions here
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            try
            {
                // Make a GET request to the Customers API
                HttpResponseMessage response = await _httpClient.GetAsync("api/Customers");
                response.EnsureSuccessStatusCode(); // Ensure a successful response

                // Deserialize the response content into a list of Customer objects
                List<Customer>? customers = await response.Content.ReadFromJsonAsync<List<Customer>>();

                // Log success
                _logger.LogInformation("GetCustomers API called successfully.");

                return Ok(customers);
            }
            catch (Exception ex)
            {
                // Log error
                _logger.LogError(ex, "Error calling GetCustomers API.");

                // Handle exceptions here
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("CreateCustomer")]
        public async Task<IActionResult> CreateCustomer([FromBody] Customer customer)
        {
            try
            {
                // Make a POST request to the CreateCustomer API
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/Customer", customer);
                response.EnsureSuccessStatusCode(); // Ensure a successful response

                // Log success
                _logger.LogInformation("CreateCustomer API called successfully.");

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log error
                _logger.LogError(ex, "Error calling CreateCustomer API.");

                // Handle exceptions here
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetCustomerById/{id}")]
        public async Task<IActionResult> GetCustomerById(string id)
        {
            try
            {
                // Make a GET request to the GetCustomerById API
                HttpResponseMessage response = await _httpClient.GetAsync($"api/Customer/{id}");
                response.EnsureSuccessStatusCode(); // Ensure a successful response

                // Deserialize the response content into a Customer object
                Customer? customer = await response.Content.ReadFromJsonAsync<Customer>();

                if (customer == null)
                {
                    return NotFound();
                }

                // Log success
                _logger.LogInformation("GetCustomerById API called successfully.");

                return Ok(customer);
            }
            catch (Exception ex)
            {
                // Log error
                _logger.LogError(ex, "Error calling GetCustomerById API.");

                // Handle exceptions here
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdateCustomer/{id}")]
        public async Task<IActionResult> UpdateCustomer(string id, [FromBody] Customer customer)
        {
            try
            {
                // Make a POST request to the UpdateCustomer API
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"api/Customer/{id}", customer);
                response.EnsureSuccessStatusCode(); // Ensure a successful response

                // Log success
                _logger.LogInformation("UpdateCustomer API called successfully.");

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log error
                _logger.LogError(ex, "Error calling UpdateCustomer API.");

                // Handle exceptions here
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteCustomer/{id}")]
        public async Task<IActionResult> DeleteCustomer(string id)
        {
            try
            {
                // Make a DELETE request to the DeleteCustomer API
                HttpResponseMessage response = await _httpClient.DeleteAsync($"api/Customer/{id}");
                response.EnsureSuccessStatusCode(); // Ensure a successful response

                // Log success
                _logger.LogInformation("DeleteCustomer API called successfully.");

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log error
                _logger.LogError(ex, "Error calling DeleteCustomer API.");

                // Handle exceptions here
                return BadRequest(ex.Message);
            }
        }

    }
}
