
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
           
            if (create.Message== "This  Customer Added Succesfully")
            {
                TempData["AlertMessage"] = " Customer Added Succesfully..!";
                return RedirectToAction("CustomerDetail");
            }
            else if(create.Message== "This Contact Number Already Exists")
            {
                TempData["AlertMessage"] = "Contact Number Already Exists..!";
                return RedirectToAction("CreateCustomer");
            }
            else
            {
                TempData["AlertMessage"] = "This Email Id Already Exists..!";
                return RedirectToAction("CreateCustomer");
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
                                              || x.ContactNumber.ToString().Contains(param.sSearch.ToString())
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
            int id = obj.CustomerId;
            var update = _customer.UpdateCustomerDetail(obj);
            if(update.Message == "Customer Updated Succesfully!!")
            {
                TempData["AlertMessage"] = "Customer Updated Successfully.. !";
                return RedirectToAction("CustomerDetail");
            } 
            else if(update.Message == "This Contact Number Already taked")
            {
                TempData["AlertMessage"] = "Contact Number Already Exists..!";
                return Redirect("UpdateCustomer?customerId="+id);
            }
            else
            {
                TempData["AlertMessage"] = "Email Id Already Exists..!";
                return Redirect("UpdateCustomer?customerId=" + id);
            }
           
        }

        public IActionResult Delete(int customerId)
        {
            var delete=_customer.DeleteCustomerDetail(customerId);
            if (delete.Message == "Customer Deleted Succesfully")
            {
                TempData["AlertMessage"] = "Customer Deleted Successfully.. !";
                return View("CustomerDetail");
            }
            else
            {
                TempData["AlertMessage"] = "This Customer Ordered Food..!";
                return View("CustomerDetail");
            }
        }
    }
}
