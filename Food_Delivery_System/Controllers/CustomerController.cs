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
            return Ok(obj);
        }


        [HttpPost("/api/Customer/Add")]
        public Messages InsertCustomerDetail(Customer customer)
        {
            var insertCustomer = _customer.InsertCustomerDetail(customer);
            return insertCustomer;
        }

        [HttpPut("/api/Customer/Update")]
        public Messages UpdateCustomerDetail([FromBody] Customer customer)
        {
            Messages messages = new Messages();
            var check = _customer.Equals(customer.ContactNumber);
            if (check )
            {
                var updateCustomer = _customer.UpdateCustomerDetail(customer);
                return updateCustomer;  
            }
            else
            {
                messages.Message = "Mobile number already taked";
            }
            return messages;
        }


        [HttpDelete("/api/Customer/Delete/{customerId}")]
        public Messages DeleteCustomerDetail(int customerId)
        {
            var deleteCustomer = _customer.DeleteCustomerDetail(customerId);
            return deleteCustomer;
        }

    }
}
