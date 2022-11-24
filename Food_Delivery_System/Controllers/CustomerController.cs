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
            Messages messages = new Messages();
            messages.Message = "Customer List Is Empty";
            var obj = _customer.GetAll();
            if (obj == null)
            {
                return NotFound(messages.Message);
            }

            return Ok(obj);
        }


        [HttpGet("{customerId}")]
        public IActionResult GetCustomerDetail(int customerId)
        {
            Messages messages = new Messages();
            messages.Message = "Customer Id Is Not Found";
            var obj = _customer.GetCustomerDetailById(customerId);
            if(obj == null)
            {
                return NotFound(messages.Message);
            }
            return Ok(obj);
        }


        [HttpPost("")]
        public IActionResult InsertCustomerDetail(Customer customer)
        {
            Messages messages = new Messages();
            var number = _customer.GetCustomerDetailByNumber(customer.ContactNumber);
            var email=_customer.GetCustomerDetailByEmail(customer.Email);
            

            if (number != null)
            {
                messages.Message = "The Phone  Number Already Taked";
                return Conflict(messages.Message);
            }
            if (email != null)
            {
                messages.Message = "The Email  Id Already Taked";
                return Conflict(messages.Message);
            }
            var insertCustomer = _customer.InsertCustomerDetail(customer);
            
            
            return Created("https://localhost:7187/Api/customer/"+customer.CustomerId+"", insertCustomer);
        }

        [HttpPut("")]
        public IActionResult UpdateCustomerDetail([FromBody] Customer customer)
        {
            Messages messages=new Messages();
            if (customer.CustomerId == 0)
            {
               
                return BadRequest("The CustomerId Field Is Required");
            }
            var id = _customer.GetCustomerDetailById(customer.CustomerId);
            if (id == null)
            {
                return NotFound(id);
            }
            var updateCustomer = _customer.UpdateCustomerDetail(customer);
           
            if(updateCustomer.Success== false)
            {
                messages.Message = "The Contact Number Already Taked";
                return  Conflict(messages.Message);
            }
           
            return Ok(updateCustomer);  
                
        }


        [HttpDelete("{customerId}")]
        public IActionResult DeleteCustomerDetail(int customerId)
        {
            var id = _customer.GetCustomerDetailById(customerId);
            if (id == null)
            {
                return NotFound(id);
            }
            var deleteCustomer = _customer.DeleteCustomerDetail(customerId);
            return Ok(deleteCustomer);
        }

    }
}
