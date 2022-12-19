﻿using Food_Delivery.Models;
using Food_Delivery.RepositoryInterface;
using Microsoft.AspNetCore.Mvc;
using static Food_Delivery.Models.Messages;

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

        [HttpGet("Getall")]
        public IActionResult GetAll()
        {
            var obj = _customer.GetAll();
            return (obj==null)? NotFound("Customer list is empty"):Ok(obj);
        }

        [HttpGet("{customerId}")]
        public IActionResult GetCustomerDetailById(int customerId)
        {
            var obj = _customer.GetCustomerDetailById(customerId);
            return (obj==null)?NotFound("Customer id is not found") :Ok(obj);
        }

        [HttpGet("IsActive/{isActive}")]   
        public IActionResult GetCustomerDetailByIsActive(bool isActive)
        {
            var obj = _customer.GetCustomerDetailByIsActive(isActive);
            return (obj==null)?NotFound("Customer list is empty"):Ok(obj);
        }

        [HttpGet("ContactNumber/{contactNumber}")]
        public IActionResult GetCustomerDetailByNumber(string contactNumber)
        {
            var obj = _customer.GetCustomerDetailByNumber(contactNumber);
            return  (obj==null)?NotFound("Customer number is not found"):Ok(obj);
        }

        [HttpPost]
        public IActionResult InsertCustomerDetail(Customer customer)
        {
            var insertCustomer = _customer.InsertCustomerDetail(customer);
            return Output(insertCustomer);
        }

        [HttpPut]
        public IActionResult UpdateCustomerDetail([FromBody] Customer customer)
        {
            var updateCustomer = _customer.UpdateCustomerDetail(customer);
            return Output(updateCustomer);  
        }

        [HttpDelete("{customerId}")]
        public IActionResult DeleteCustomerDetail(int customerId)
        {  
            var deleteCustomer = _customer.DeleteCustomerDetail(customerId);
            return Output(deleteCustomer);
        }
        private IActionResult Output(Messages result)
        {
            switch (result.Status)
            {
                case Statuses.BadRequest:
                    return BadRequest(result.Message);
                case Statuses.NotFound:
                    return NotFound(result.Message);
                case Statuses.Conflict:
                    return Conflict(result.Message);
                case Statuses.Created:
                    return Created("",result.Message);
            }
            return Ok(result.Message);
        }
    }
}
