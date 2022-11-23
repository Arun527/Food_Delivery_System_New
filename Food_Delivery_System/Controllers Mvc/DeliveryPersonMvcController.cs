using Food_Delivery.Models;
using Food_Delivery.RepositoryInterface;
using Microsoft.AspNetCore.Mvc;

namespace Food_Delivery.Controllers_Mvc
{
    public class DeliveryPersonMvcController : Controller
    {
        private readonly ILogger<HotelMvcController> _logger;

        IDeliveryPerson _person;
        public DeliveryPersonMvcController(ILogger<DeliveryPersonMvcController> logger, IDeliveryPerson person)
        {
           
            _person = person;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DeliveryPersonDetail()
        {
            var deliveryPerson = _person.GetAllDeliveryPersons();
            return View(deliveryPerson);
        }

        public IActionResult AddPerson()
        {
            
            return View();
        }
        public IActionResult Add(DeliveryPerson deliveryPerson)
        {
            var deliveryperson = _person.InsertDeliveryPerson(deliveryPerson);
            return RedirectToAction("AddPerson");
        }

        public IActionResult DeletePerson(int DeliveryPersonId)
        {
            var obj = _person.DeleteDeliveryPerson(DeliveryPersonId);
            return Json(obj);
        }
    }
}
