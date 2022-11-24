
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
            return View();
        }

        public IActionResult GetAll(JqueryDatatableParam param)
        {
            var customerdetail = _customer.GetAll();

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                customerdetail = customerdetail.Where(x => x.Name.Contains(param.sSearch.ToLower())
                                              || x.ContactNumber.ToLower().Contains(param.sSearch.ToLower())
                                              || x.Email.ToLower().Contains(param.sSearch.ToLower())
                                              || x.Address.ToString().Contains(param.sSearch.ToLower()));
            }


            var displayResult = customerdetail.Skip(param.iDisplayStart)
                .Take(param.iDisplayLength).ToList();
            var totalRecords = customerdetail.Count();



            return Json(new
            {
                param.sEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = displayResult
            });
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
