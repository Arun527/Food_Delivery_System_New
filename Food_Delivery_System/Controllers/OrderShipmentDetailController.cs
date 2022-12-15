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
        IOrderShipmentDetail _orderShipmentDetail;
        IOrders _order;
        IOrderDetail _orderDetail;
        IDeliveryPerson _deliveryPerson;
        ICustomer _customer;
        public OrderShipmentDetailController(IOrderShipmentDetail orderShipmentDetail, IOrders order,  IOrderDetail orderDetail,IDeliveryPerson deliveryPerson,ICustomer customer)
        {
            _orderShipmentDetail = orderShipmentDetail;
            _order = order;
            _orderDetail=orderDetail;
            _deliveryPerson=deliveryPerson;
            _customer=customer;
            
        }
        [HttpGet("getall")]
        public IActionResult GetAllOrderShipmentDetail()
        {
            var ShipmentDetail = _orderShipmentDetail.GetAllOrderShipmentDetail();
            return (ShipmentDetail == null) ? NotFound("OrderShipmentDetail list is empty") : Ok(ShipmentDetail);
        }
        [HttpGet("GetDeliveryPerson/{Id}")]
        public IActionResult GetDeliveryPersonById(int Id)
        {
            
            var obj = _deliveryPerson.GetDeliveryPerson(Id);
            if(obj == null)
            {
                return NotFound("DeliveryPerson id is not found");
            }

            var delivery = _orderShipmentDetail.GetdeliveryPersonById(Id);
            if (delivery.Count() == 0)
            {
                return NotFound("This delivery person don't delivery any order");
            }
            return Ok(delivery);
        }

        [HttpGet("{Id}")]
        public IActionResult GetOrderDetailById(int Id)
        {
            Messages messages = new Messages();
            messages.Message = "OrderShipmentDetail id is not found";
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
         
            List<OrderShipmentList>obj = new List<OrderShipmentList>();
            obj = orderShipment.ShipmentRequest;
            foreach(OrderShipmentList item in obj)
            {
                var order=_orderDetail.GetOrderDetail(item.OrderDetailId);

                if (order == null)
                {
                    return BadRequest("The orderDetail id is not found");
                }
            }
            if (orderShipment.DeliveryPersonId ==null)
            {
              return BadRequest("The deliveryPerson id field is required");
            }
            var deliveryPerson = _deliveryPerson.GetDeliveryPerson(orderShipment.DeliveryPersonId);
            if (deliveryPerson == null)
            {
                messages.Message = "DeliveryPerson id is not found";
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
            var deliveryPerson = _deliveryPerson.GetDeliveryPerson(orderShipment.DeliveryPersonId);
            var orderDetail = _orderDetail.GetOrderDetail(orderShipment.OrderDetailId);
            if (ordershipmentId == null)
            {
                return NotFound("The orderShipmentDetail id is not found");
            }
            if (orderShipment.DeliveryPersonId == 0)
            {
                return BadRequest("The deliveryPerson Id field is required");
            }
            if (orderShipment.OrderShipmentDetailId == 0)
            {
                return BadRequest("The orderShipmentDetail id field is required");
            }
            if (orderShipment.OrderDetailId == 0)
            {
                return BadRequest("The orderDetail id field is required");
            }
            if (deliveryPerson == null)
            {
                messages.Message = "The deliveryPerson id is not found";
                return NotFound(messages.Message);
            }
            if (orderDetail == null)
            {
                messages.Message = "The orderDetail id is not found";
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
                messages.Message = "The orderShipmentDetail id is not found";
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
                messages.Message = "The customer id is not found";
                return NotFound(messages.Message);
            }
            var invoice =  _orderShipmentDetail.GetCustomerOrderDetailsById(CustomerId);
            return Ok(invoice);
        }


        [HttpGet("Invoice/GetAll")]
        public IActionResult GetAllInvoiceDetail()
        {
            Messages messages = new Messages();
            messages.Message = "The invoice list is empty";
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
                messages.Message = "The order id is not found";
                return NotFound(messages.Message);
            }
            var tracking=_orderShipmentDetail.TrackingStatus(orderId);
            return Ok(tracking);
        }

    }
}
