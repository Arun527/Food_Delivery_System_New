using Food_Delivery.Models;
using Food_Delivery.RepositoryInterface;
using Food_Delivery_System.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Food_Delivery.Models.Messages;

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
            return (obj == null) ? NotFound("DeliveryPerson id is not found") : Ok(_orderShipmentDetail.GetdeliveryPersonById(Id));
        }

        [HttpGet("{Id}")]
        public IActionResult GetOrderDetailById(int Id)
        {
            var obj = _orderShipmentDetail.GetOrderShipmentDetailById(Id);
            return (obj==null) ? NotFound("OrderShipmentDetail id is not found") :Ok(obj);
        }

        [HttpPost]
        public IActionResult InsertOrderShipmentDetail(OrderShipmentRequest orderShipment)
        {
            var orderShipmentDetail = _orderShipmentDetail.InsertOrderShipmentDetail(orderShipment);
                return (orderShipment.OrderShipmentDetailId==0)? BadRequest("The ordershipment detail id field is required") :
                       (orderShipment.DeliveryPersonId==0)? BadRequest("The delivery person id field is required") : Output(orderShipmentDetail);
        }

        [HttpPut]
        public IActionResult UpdateOrderShipmentDetail(OrderShipmentDetail orderShipment)
        {
            var updateOrderShipment = _orderShipmentDetail.UpdateOrderShipmentDetail(orderShipment);
            return (orderShipment.OrderShipmentDetailId == 0) ? BadRequest("The ordershipment detail id field is required") :
                   (orderShipment.OrderDetailId == 0) ? BadRequest("The orderdetail id field is required") :
                   (orderShipment.DeliveryPersonId == 0) ? BadRequest("The delivery person id field is required") : Output(updateOrderShipment);
        }

        [HttpDelete("{orderShipmentId}")]
        public IActionResult DeleteOrderShipmentDetail(int orderShipmentId)
        {
            var deleteOrderShipment = _orderShipmentDetail.DeleteOrderShipmentDetail(orderShipmentId);
            return Output(deleteOrderShipment);
        }

        [HttpGet("Invoice/{CustomerId}")]
        public IActionResult GetCustomerOrderDetailsById(int CustomerId)
        {
            var customerId = _customer.GetCustomerDetailById(CustomerId);
            return (customerId == null) ? NotFound("The customer id is not found") : Ok(_orderShipmentDetail.GetCustomerOrderDetailsById(CustomerId));
        }

        [HttpGet("Invoice/GetAll")]
        public IActionResult GetAllInvoiceDetail()
        {
            var invoice = _orderShipmentDetail.GetAllInvoiceDetail();
            return (invoice==null) ? NotFound("The invoice list is empty") : Ok(invoice);
        }

        [HttpGet("Tracking/{orderId}")]
        public IActionResult TrackingStatus(int orderId)
        {
            Messages messages = new Messages();
            var customerId = _order.GetOrder(orderId);
            return (customerId == null) ? NotFound("The order id is not found") : Ok(_orderShipmentDetail.TrackingStatus(orderId));
        }
        public IActionResult Output(Messages result)
        {
            switch (result.Status)
            {
                case Statuses.BadRequest:
                    return BadRequest(result.Message);
                case Statuses.NotFound:
                    return NotFound(result.Message);
                case Statuses.Conflict:
                    return Conflict(result.Message);
                case Statuses.Created:
                    return Created("", result.Message);
            }
            return Ok(result.Message);
        }

    }
}
