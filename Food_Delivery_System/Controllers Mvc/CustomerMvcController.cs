
using Food_Delivery.Models;
using Food_Delivery.RepositoryInterface;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using static Food_Delivery.Models.Messages;

namespace Food_Delivery.Controllers_Mvc
{
    public class CustomerMvcController : Controller
    {
        private readonly ILogger<CustomerMvcController> _logger;

        ICustomer _customer;   
        IHotel _hotel;
        IDeliveryPerson _deliveryPerson; 
        IOrderShipmentDetail _deliveryShipmentDetail;
        public CustomerMvcController(ILogger<CustomerMvcController> logger, ICustomer obj, IHotel hotel,IDeliveryPerson person ,IOrderShipmentDetail deliveryShipmentDetail)
        {
            _logger = logger;
            _customer = obj;
            _hotel = hotel;
            _deliveryPerson=person;
            _deliveryShipmentDetail = deliveryShipmentDetail;
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
            if (create.Status==Statuses.Created)
            {
                TempData["AlertMessage"] = create.Message;
                return RedirectToAction("CustomerDetail");
            }
            else 
            {
                if (!ModelState.IsValid)
                {
                    return View(customer);
                }
                TempData["AlertMessage"] = create.Message;
                return View("CreateCustomer");
            }
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
                                              || x.ContactNumber.ToString().Contains(param.sSearch.ToLower())
                                              || x.Email.ToLower().Contains(param.sSearch.ToLower())
                                              || x.Gender.ToLower().Contains(param.sSearch.ToLower())
                                              || x.Address.ToLower().Contains(param.sSearch.ToLower()));
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
            var update = _customer.GetCustomerDetailById(CustomerId);
            return View(update);
        }


        public IActionResult Update(Customer obj)
        {
            var update = _customer.UpdateCustomerDetail(obj);
            if (update.Status==Statuses.Success)
            {
                TempData["AlertMessage"] = update.Message;
                return RedirectToAction("CustomerDetail");
            } 
            else 
            {
                TempData["AlertMessage"] =update.Message;
                return Redirect("UpdateCustomer?customerId="+obj.CustomerId);
            }
        }

        public IActionResult Delete(int customerId)
        {
            var delete=_customer.DeleteCustomerDetail(customerId);
            TempData["AlertMessage"] = delete.Message;
            return View("CustomerDetail");
        }
    }
}
