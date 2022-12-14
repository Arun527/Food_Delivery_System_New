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
         

        [HttpGet("/api/Customer/{customerId}")]
        public IActionResult GetCustomerDetailById(int customerId)
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
        [HttpGet("IsActive/{isActive}")]
        public IActionResult GetCustomerDetailByIsActive(bool isActive)
        {
           
            var obj = _customer.GetCustomerDetailByIsActive(isActive);

            if (obj == null)
            {
                return NotFound("Customer Id Is Not Found");
            }

            if (obj.Count() == 0)
            {
                return NotFound("Customer List Is Empty");
            }
            return Ok(obj);
        }

        [HttpGet("ContactNumber/{contactNumber}")]
        public IActionResult GetCustomerDetailByNumber(string contactNumber)
        {
            Messages messages = new Messages();
            messages.Message = "Customer Id Is Not Found";
            var obj = _customer.GetCustomerDetailByNumber(contactNumber);
            if (obj == null)
            {
                return NotFound(messages.Message);
            }
            return Ok(obj);
        }

        [HttpPost("/api/Customer")]
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
            
            
            return Created(customer.CustomerId+"",insertCustomer);
        }

        [HttpPut("/api/Customer")]
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
                return NotFound("The Customer Id Is Not Found");
            }
            var updateCustomer = _customer.UpdateCustomerDetail(customer);

            if (updateCustomer.Success==false)
            {
               
                return Conflict(updateCustomer.Message);
            }

          

            return Ok(updateCustomer);  
                
        }


        [HttpDelete("/api/Customer/{customerId}")]
        public IActionResult DeleteCustomerDetail(int customerId)
        {
            Messages messages =new Messages();
            var id = _customer.GetCustomerDetailById(customerId);
            if (id == null)
            {
                return NotFound("The Customer Id Not Found");
            }
            
            var deleteCustomer = _customer.DeleteCustomerDetail(customerId);
            if (deleteCustomer.Success == false)
            {
                return BadRequest("The Customer Id Not Deleted,Because This Customer  Order The Product ");
            }
            return Ok(deleteCustomer);
        }

    }
}
