﻿using Food_Delivery.Controllers_Mvc;
using Food_Delivery.Models;
using Food_Delivery.RepositoryInterface;
using Microsoft.AspNetCore.Mvc;
using static Food_Delivery.Models.Messages;

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
            if (delivery.Status == Statuses.Success)
            {
                TempData["AlertMessage"] = delivery.Message;
                return RedirectToAction("DeliveryPersonView");
            }
            else
            {
                TempData["AlertMessage"] = delivery.Message;
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
            if (update.Status == Statuses.Success) 
            {
                TempData["AlertMessage"] = update.Message;
                return RedirectToAction("DeliveryPersonView");
            } 
            else
            {
                update.Status=Statuses.Conflict;
                TempData["AlertMessage"] = update.Message;
                return Redirect("Edit?deliveryPersonId=" + id);
            }
        }
        public IActionResult GetDeliveryPersonByIdInvoice(int Id)
        {
            var delivery = _orderShipmentDetail.GetdeliveryPersonById(Id);
            return View(delivery);
        }

        public IActionResult  DeleteDeliveryPerson(int DeliveryPersonId)
        {
            var delivery=_deliveryPerson.DeleteDeliveryPerson(DeliveryPersonId); 
            TempData["AlertMessage"] = delivery.Message;
            return RedirectToAction("DeliveryPersonView");
        }
    }
}