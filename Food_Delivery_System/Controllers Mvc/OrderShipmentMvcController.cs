using Food_Delivery.Models;
using Food_Delivery.RepositoryInterface;
using Food_Delivery_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Food_Delivery_System.Controllers_Mvc
{
    public class OrderShipmentMvcController : Controller
    {

        private readonly ILogger<OrderShipmentMvcController> _logger;
        ICustomer _customer;
        IHotel _hotel;
        IDeliveryPerson _deliveryPerson;
        IOrderDetail _orderDetail;
        IOrderShipmentDetail _orderShipmentDetail;
        public OrderShipmentMvcController(ILogger<OrderShipmentMvcController> logger, ICustomer obj, IHotel hotel, IDeliveryPerson person, IOrderDetail orderDetail, IOrderShipmentDetail orderShipmentDetail)
        {
            _logger = logger;
            _customer = obj;
            _hotel = hotel;
            _deliveryPerson = person;
            _orderDetail = orderDetail;
            _orderShipmentDetail = orderShipmentDetail;

        }
        public IActionResult AddShipment()
        {


            OrderShipmentDto shipment = new OrderShipmentDto();
            shipment.DeliveryList = new List<SelectListItem>();
            shipment.DeliveryList.Add(new SelectListItem() { Value = "0", Text = "Select Delivery Person" });
            shipment.DeliveryList.AddRange(_deliveryPerson.GetAllDeliveryPersons().Select(a => new SelectListItem
            {
                Text = a.DeliveryPersonName,
                Value = a.DeliveryPersonId.ToString(),
            }));
            shipment.ShipmentList = new List<SelectListItem>();
            shipment.ShipmentList.Add(new SelectListItem() { Value = "0", Text = "Select Order Id" });
            shipment.ShipmentList.AddRange(_orderDetail.GetAll().Select(a => new SelectListItem
            {
                Text = a.OrderDetailId.ToString(),
                Value = a.OrderDetailId.ToString(),
            }));
            return View(shipment);
        }

        public IActionResult Add([FromBody] OrderShipmentRequest order)
        {

            var orderdetail = _orderShipmentDetail.InsertOrderShipmentDetail(order);

            return Json("AddShipment");
        }


        public IActionResult GetAllShipment()
        {
            var delivery = _orderShipmentDetail.GetAllInvoiceDetail();
            return View(delivery);
        }


        public IActionResult GetShipmentByUser(int customerId)
        {
            var delivery = _orderShipmentDetail.GetCustomerOrderDetailsById(customerId);
            return View(delivery);
        }
    }
}
