using Food_Delivery.Controllers_Mvc;
using Food_Delivery.Models;
using Food_Delivery.RepositoryInterface;
using Microsoft.AspNetCore.Mvc;

namespace Food_Delivery_System.Controllers_Mvc
{
    public class DeliveryMvcController : Controller
    {


        private readonly ILogger<DeliveryMvcController> _logger;

        ICustomer _customer;
        IHotel _hotel;
        IDeliveryPerson _deliveryPerson;
        IOrderDetail _orderDetail;
        IOrderShipmentDetail _orderShipmentDetail;
        public DeliveryMvcController(ILogger<DeliveryMvcController> logger, ICustomer obj, IHotel hotel, IDeliveryPerson person, IOrderDetail orderDetail,IOrderShipmentDetail orderShipmentDetail)
        {
            _logger = logger;
            _customer = obj;
            _hotel = hotel;
            _deliveryPerson = person;
            _orderDetail = orderDetail;
            _orderShipmentDetail= orderShipmentDetail;
           
        }
        public IActionResult Add()
        {
            return View();
        }

        public IActionResult AddPerson(DeliveryPerson  deliveryPerson)
        {
            var delivery=_deliveryPerson.InsertDeliveryPerson(deliveryPerson);
            if(delivery.Message== "DeliveryPerson Added Succesfully")
            {
                TempData["AlertMessage"] = "DeliverPerson Created Successfully.. !";
                return RedirectToAction("DeliveryPersonView");
            }
            else
            {
                TempData["AlertMessage"] = "The Contact Number Already Exist";
                return RedirectToAction("Add");
            }
         
        }

        public IActionResult DeliveryPersonView()
        {
            var delivery = _deliveryPerson.GetAllDeliveryPersons();
            return View(delivery);
        }
        public IActionResult Edit(int deliveryPersonId)
        {

            var delivery = _deliveryPerson.GetDeliveryPerson(deliveryPersonId);
            return View(delivery);
        }

        public IActionResult EditDetail(DeliveryPerson deliveryPerson)
        {
            int id = deliveryPerson.DeliveryPersonId;
            var update=_deliveryPerson.UpdateDeliveryPerson(deliveryPerson);
            if(update.Message== "DeliveryPerson Updated Succesfully!!")
            {
                TempData["AlertMessage"] = "DeliverPerson Updated Successfully.. !";
                return RedirectToAction("DeliveryPersonView");
            } 
            else
            {
                TempData["AlertMessage"] = "Contact Number Already Exist!!";
                return Redirect("Edit?deliveryPersonId=" + id);
            }
        }

        public IActionResult GetDeliveryPersonByIdInvoice(int Id)
        {
            var delivery = _orderShipmentDetail.GetdeliveryPersonById(Id);
            if (delivery.Count() == 0)
            {
                return NotFound("This Delivery Person Don't Delivery Any Order");
            }
            return View(delivery);
        }

        public IActionResult  DeleteDeliveryPerson(int DeliveryPersonId)
        {
            var delivery=_deliveryPerson.DeleteDeliveryPerson(DeliveryPersonId); 
            TempData["AlertMessage"] = "DeliverPerson Deleted Successfully.. !";
            return RedirectToAction("DeliveryPersonView");
        }
    }
}
