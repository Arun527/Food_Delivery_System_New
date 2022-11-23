
using Food_Delivery.Models;
using Food_Delivery.RepositoryInterface;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Food_Delivery.Controllers_Mvc
{
    public class CustomerMvcController : Controller
    {
        private readonly ILogger<CustomerMvcController> _logger;

        ICustomer _customer;
        IHotel _hotel;
        IDeliveryPerson _deliveryPerson;
        public CustomerMvcController(ILogger<CustomerMvcController> logger, ICustomer obj, IHotel hotel,IDeliveryPerson person)
        {
            _logger = logger;
            _customer = obj;
            _hotel = hotel;
            _deliveryPerson=person;
        }
        public IActionResult Index()
        {
            return View();
        }

      
        public IActionResult CreateCustomer()
        {

            return View();
        }

      
        public IActionResult Create(Customer customer)
        {
            var create = _customer.InsertCustomerDetail(customer);
            return RedirectToAction("CreateCustomer");
        }

        public IActionResult CustomerDetail()
        {
            var customerdetail = _customer.GetAll();
            return View(customerdetail);
        }

        public IActionResult UpdateCustomer(int CustomerId)
        {
            var update=_customer.GetCustomerDetail(CustomerId);
            return View(update);
        }



        public IActionResult Update(Customer obj)
        {
            int id=obj.CustomerId;
            var update=_customer.UpdateCustomerDetail(obj);
            return  Redirect("CustomerDetail?CustomerId="+id);
        }



    }
}
