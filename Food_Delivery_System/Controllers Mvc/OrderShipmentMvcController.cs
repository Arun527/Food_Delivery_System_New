using Food_Delivery.RepositoryInterface;
using Food_Delivery_System.Models;
using Microsoft.AspNetCore.Mvc;

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
            return View();
        }

        public IActionResult AddView()
        {
            OrderShipmentRequest shipment = new OrderShipmentRequest();
            var delivery=_deliveryPerson.GetAllDeliveryPersons();

            return View();
        }


        public IActionResult GetAllShipment()
        {
            OrderShipmentRequest shipment = new OrderShipmentRequest();
            var delivery = _orderShipmentDetail.GetAllInvoiceDetail();

            return View(delivery);
        }


        public IActionResult GetShipmentByUser(int customerId)
        {
            OrderShipmentRequest shipment = new OrderShipmentRequest();
            var delivery = _orderShipmentDetail.GetCustomerOrderDetailsById(customerId);

            return View(delivery);
        }
    }
}
