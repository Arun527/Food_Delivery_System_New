using Food_Delivery.Models;
using Food_Delivery.RepositoryInterface;
using Microsoft.AspNetCore.Mvc;

namespace Food_Delivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        ICustomer _customer;
        public CustomerController(ICustomer customer)
        {
            _customer = customer;
        }

        [HttpGet("/api/Customer/Getall")]
        public IActionResult GetAll()
        {
            var obj = _customer.GetAll();
            return Ok(obj);
        }
         

        [HttpGet("/api/Customer/Get/{customerId}")]
        public IActionResult GetCustomerDetail(int customerId)
        {
            var obj = _customer.GetCustomerDetail(customerId);
            if(obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }


        [HttpPost("/api/Customer/Add")]
        public IActionResult InsertCustomerDetail(Customer customer)
        {
            var insertCustomer = _customer.InsertCustomerDetail(customer);
            return Ok(insertCustomer);
        }

        [HttpPut("/api/Customer/Update")]
        public IActionResult UpdateCustomerDetail([FromBody] Customer customer)
        {
                
                var updateCustomer = _customer.UpdateCustomerDetail(customer);
                return Ok(updateCustomer);  
                
        }


        [HttpDelete("/api/Customer/Delete/{customerId}")]
        public IActionResult DeleteCustomerDetail(int customerId)
        {
            var deleteCustomer = _customer.DeleteCustomerDetail(customerId);
            return Ok(deleteCustomer);
        }

    }
}
