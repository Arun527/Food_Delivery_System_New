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

        public OrderShipmentDetailController(IOrders order, IOrderShipmentDetail orderShipmentDetail, IOrderDetail orderDetail)
        {
            _order = order;
            _orderShipmentDetail=orderShipmentDetail;
            _orderDetail=orderDetail;
        }


        [HttpGet("/api/OrderShipmentDetail/getall")]
        public IActionResult GetAllOrderShipmentDetail()
        {
            var ShipmentDetail = _orderShipmentDetail.GetAllOrderShipmentDetail();
            return Ok(ShipmentDetail);
        }


        [HttpGet("/api/OrderShipmentDetail/GetDeliveryPerson/{Id}")]
        public OrderShipmentDetail GetDeliveryPersonById(int Id)
        {
            var obj = _orderShipmentDetail.GetDeliveryPersonById(Id);
            return obj;
        }

        [HttpGet("/api/OrderShipmentDetail/GetOrderDetail/{Id}")]
        public OrderShipmentDetail GetOrderDetailById(int Id)
        {
            var obj = _orderShipmentDetail.GetOrderDetailById(Id);
            return obj;
        }

        [HttpPost("/api/OrderShipmentDetail/Add")]
        public Messages InsertOrderShipmentDetail(OrderShipmentRequest orderShipment)
        {
            var orderShipmentDetail = _orderShipmentDetail.InsertOrderShipmentDetail(orderShipment);
            return orderShipmentDetail;
        }

        [HttpPut("/api/OrderShipmentDetail/Update")]
        public Messages UpdateOrderShipmentDetail(OrderShipmentDetail orderShipment)
        {
            var updateOrderShipment = _orderShipmentDetail.UpdateOrderShipmentDetail(orderShipment);
            return updateOrderShipment;
        }


        [HttpDelete("/api/OrderShipmentDetail/Delete/{orderShipmentId}")]
        public Messages DeleteOrderShipmentDetail(int orderShipmentId)
        {
            var deleteOrderShipment = _orderShipmentDetail.DeleteOrderShipmentDetail(orderShipmentId);
            return deleteOrderShipment;
        }

        //[HttpGet("/api/OrderShipmentDetail/Invoice/{CustomerId}")]
        //public IEnumerable<InvoiceDetail> GetCustomerOrderDetailsById(int CustomerId)
        //{
        //    return _orderShipmentDetail.GetCustomerOrderDetailsById(CustomerId);
        //}


        //[HttpGet("/api/OrderShipmentDetail/Invoice/Getall")]
        //public IEnumerable<InvoiceDetail> GetAllInvoiceDetail()
        //{
        //    return _orderShipmentDetail.GetAllInvoiceDetail();
        //}


        [HttpGet("/api/OrderShipmentDetail/Tracking/{orderId}")]
        public IEnumerable<TrackingDetail> TrackingStatus(int orderId)
        {
            return _orderShipmentDetail.TrackingStatus(orderId);
        }

        [HttpGet("/api/OrderShipmentDetail/Tracking/{orderId}")]
        public IEnumerable<TrackingDetail> TrackingStatus(int orderId)
        {
            return _orderShipmentDetail.TrackingStatus(orderId);
        }

    }
}
