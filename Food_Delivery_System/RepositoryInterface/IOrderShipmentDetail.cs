using Food_Delivery.Models;
using Food_Delivery_System.Models;

namespace Food_Delivery.RepositoryInterface
{
    public interface IOrderShipmentDetail
    {
        public IEnumerable<OrderShipmentDetail> GetAllOrderShipmentDetail();
        public OrderShipmentDetail GetDeliveryPersonById(int Id);
        public OrderShipmentDetail GetOrderShipmentDetailById(int Id);
        public Messages InsertOrderShipmentDetail(OrderShipmentRequest orderShipment);
        public Messages UpdateOrderShipmentDetail(OrderShipmentDetail orderShipment);
        public Messages DeleteOrderShipmentDetail(int orderShipmentId);
        public IEnumerable<InvoiceDetail> GetCustomerOrderDetailsById(int CustomerId);
        public IEnumerable<InvoiceDeliveryPerson> GetdeliveryPersonById(int deliveryPersonId);
        public IEnumerable<TrackingDetail> TrackingStatus(int orderId);
           public IEnumerable<InvoiceDetail> GetAllInvoiceDetail();

       
    }
}
