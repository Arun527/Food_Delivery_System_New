using Food_Delivery.Models;
using Food_Delivery.RepositoryInterface;
using Food_Delivery_System.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Food_Delivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderShipmentDetailController : ControllerBase
    {
        
        IOrders _order;
        IOrderShipmentDetail _orderShipmentDetail;
        IOrderDetail _orderDetail;
        IDeliveryPerson _deliveryPerson;
        ICustomer _customer;

        public OrderShipmentDetailController(IOrders order, IOrderShipmentDetail orderShipmentDetail, IOrderDetail orderDetail,IDeliveryPerson deliveryPerson,ICustomer customer)
        {
            _order = order;
            _orderShipmentDetail=orderShipmentDetail;
            _orderDetail=orderDetail;
            _deliveryPerson=deliveryPerson;
            _customer=customer;
            
        }


        [HttpGet("getall")]
        public IActionResult GetAllOrderShipmentDetail()
        {
            Messages messages = new Messages();
            messages.Message = "OrderShipmentDetail List Is Empty";
            var ShipmentDetail = _orderShipmentDetail.GetAllOrderShipmentDetail();
            if(ShipmentDetail == null)
            {
                return NotFound(messages.Message);
            }
            return Ok(ShipmentDetail);
        }


        [HttpGet("GetDeliveryPerson/{Id}")]
        public IActionResult GetDeliveryPersonById(int Id)
        {
            Messages messages = new Messages();
            messages.Message = "DeliveryPerson Id Is NotFound";
            var obj = _orderShipmentDetail.GetDeliveryPersonById(Id);
            if(obj == null)
            {
                return NotFound(messages.Message);
            }
            return Ok(obj);
        }

        [HttpGet("{Id}")]
        public IActionResult GetOrderDetailById(int Id)
        {
            Messages messages = new Messages();
            messages.Message = "OrderShipmentDetail Id Is Not Found";
            var obj = _orderShipmentDetail.GetOrderShipmentDetailById(Id);
            if(obj==null)
            {
                return NotFound(messages.Message);
            }
            return Ok(obj);
        }

        [HttpPost("")]
        public IActionResult InsertOrderShipmentDetail(OrderShipmentRequest orderShipment)
        {
            Messages messages = new Messages();
            var deliveryPerson = _orderShipmentDetail.GetDeliveryPersonById(orderShipment.DeliveryPersonId);
            List<OrderShipmentList>obj = new List<OrderShipmentList>();
            obj = orderShipment.ShipmentRequest;
            foreach(OrderShipmentList item in obj)
            {
                var order=_orderDetail.GetOrderDetail(item.OrderDetailId);

                if (order == null)
                {
                    return BadRequest("The Order Detail Id Is Not Found");
                }
            }
            if (orderShipment.DeliveryPersonId==0)
            {
              return BadRequest("The DeliveryPersonId Field Is Required");
            }
            if(deliveryPerson == null)
            {
                messages.Message = "DeliveryPerson Id Is NotFound";
                return NotFound(messages.Message);
            }
            var orderShipmentDetail = _orderShipmentDetail.InsertOrderShipmentDetail(orderShipment);
            return Ok(orderShipmentDetail);
        }

        [HttpPut("")]
        public IActionResult UpdateOrderShipmentDetail(OrderShipmentDetail orderShipment)
        {
            Messages messages = new Messages();
            var ordershipmentId= _orderShipmentDetail.GetOrderShipmentDetailById(orderShipment.OrderShipmentDetailId);
            var deliveryPerson = _orderShipmentDetail.GetDeliveryPersonById(orderShipment.DeliveryPersonId);
            var orderDetail = _orderDetail.GetOrderDetail(orderShipment.OrderDetailId);
            if (ordershipmentId == null)
            {
                return NotFound("The OrderShipmentDetail Id Is NotFound");
            }
            if (orderShipment.DeliveryPersonId == 0)
            {
                return BadRequest("The DeliveryPersonId Field Is Required");
            }
            if (orderShipment.OrderShipmentDetailId == 0)
            {
                return BadRequest("The OrderShipmentDetailId Field Is Required");
            }
            if (orderShipment.OrderDetailId == 0)
            {
                return BadRequest("The OrderDetailId Field Is Required");
            }
            if (deliveryPerson == null)
            {
                messages.Message = "The DeliveryPerson Id Is NotFound";
                return NotFound(messages.Message);
            }
            if (orderDetail == null)
            {
                messages.Message = "The OrderDetail Id Is NotFound";
                return NotFound(messages.Message);
            }
            var updateOrderShipment = _orderShipmentDetail.UpdateOrderShipmentDetail(orderShipment);
            return Ok(updateOrderShipment);
        }


        [HttpDelete("{orderShipmentId}")]
        public IActionResult DeleteOrderShipmentDetail(int orderShipmentId)
        {
            Messages messages = new Messages();
            var ordershipmentId = _orderShipmentDetail.GetOrderShipmentDetailById(orderShipmentId);
            if(ordershipmentId == null)
            {
                messages.Message = "The OrderShipmentDetail Id Is NotFound";
                return NotFound(messages.Message);
            }
            var deleteOrderShipment = _orderShipmentDetail.DeleteOrderShipmentDetail(orderShipmentId);
            return Ok(deleteOrderShipment);
        }

        [HttpGet("Invoice/{CustomerId}")]
        public IActionResult GetCustomerOrderDetailsById(int CustomerId)
        {
            Messages messages = new Messages();
            var customerId = _customer.GetCustomerDetailById(CustomerId);
            if (customerId == null)
            {
                messages.Message = "The CustomerId Id Is NotFound";
                return NotFound(messages.Message);
            }
            var invoice =  _orderShipmentDetail.GetCustomerOrderDetailsById(CustomerId);
            return Ok(invoice);
        }


        [HttpGet("Invoice/GetAll")]
        public IActionResult GetAllInvoiceDetail()
        {
            Messages messages = new Messages();
            messages.Message = "The Invoice List Is Empty";
            var invoice = _orderShipmentDetail.GetAllInvoiceDetail();
            if(invoice==null)
            {
                return NotFound(messages.Message);
            }
            return Ok(invoice);
        }

        [HttpGet("Tracking/{orderId}")]
        public IActionResult TrackingStatus(int orderId)
        {
            Messages messages = new Messages();
            var customerId = _order.GetOrder(orderId);
            if (customerId == null)
            {
                messages.Message = "The Order Id Is NotFound";
                return NotFound(messages.Message);
            }
            var tracking=_orderShipmentDetail.TrackingStatus(orderId);
            return Ok(tracking);
        }

    }
}
