﻿using Food_Delivery.Models;
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
            messages.Message = "Customer list is empty";
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
            messages.Message = "Customer id is not found";
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
                return NotFound("Customer id is not found");
            }
            if (obj.Count() == 0)
            {
                return NotFound("Customer list is empty");
            }
            return Ok(obj);
        }

        [HttpGet("ContactNumber/{contactNumber}")]
        public IActionResult GetCustomerDetailByNumber(string contactNumber)
        {
            Messages messages = new Messages();
            messages.Message = "Customer id is not found";
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
                messages.Message = "The phone  number already taked";
                return Conflict(messages.Message);
            }
            if (email != null)
            {
                messages.Message = "The Email  id already taked";
                return Conflict(messages.Message);
            }
            var insertCustomer = _customer.InsertCustomerDetail(customer);
            return Created("https://localhost:7187/Api/customer/"+customer.CustomerId+"", insertCustomer);
        }

        [HttpPut("/api/Customer")]
        public IActionResult UpdateCustomerDetail([FromBody] Customer customer)
        {
            Messages messages=new Messages();
            if (customer.CustomerId == 0)
            {
                return BadRequest("The customer id field is required");
            }
            var id = _customer.GetCustomerDetailById(customer.CustomerId);
            if (id == null)
            {
                return NotFound("The customer id is not found");
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
                return NotFound("The customer id not found");
            }
            var deleteCustomer = _customer.DeleteCustomerDetail(customerId);
            if (deleteCustomer.Success==false)
            {
                return BadRequest("This customer already order the food. So can't delete this customer.");
            }
            return Ok(deleteCustomer);
        }
    }
}
