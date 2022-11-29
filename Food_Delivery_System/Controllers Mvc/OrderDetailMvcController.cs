using Food_Delivery.Controllers_Mvc;
using Food_Delivery.RepositoryInterface;
using Food_Delivery_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Food_Delivery_System.Controllers_Mvc
{
    public class OrderDetailMvcController : Controller
    {

        private readonly ILogger<OrderDetailMvcController> _logger;

        ICustomer _customer;
        IHotel _hotel;
        IDeliveryPerson _deliveryPerson;
        public OrderDetailMvcController(ILogger<OrderDetailMvcController> logger, ICustomer obj, IHotel hotel, IDeliveryPerson person)
        {
            _logger = logger;
            _customer = obj;
            _hotel = hotel;
            _deliveryPerson = person;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            OrderDto types = new OrderDto();
            var customer = _customer.GetAll();
            types.CustomerList = new List<SelectListItem>();
            types.CustomerList.Add(new SelectListItem() { Value = "0", Text = "Select Customer" });
            types.CustomerList.AddRange(_customer.GetAll().Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.CustomerId.ToString(),
            }));
            return View(types);
        }
        public IActionResult AddOrder()
        {

            return View();
        }

    }
}
