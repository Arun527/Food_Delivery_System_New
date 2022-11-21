using Food_Delivery.Models;

namespace Food_Delivery.RepositoryInterface
{
    public interface IOrderShipmentDetail
    {
        public IEnumerable<OrderShipmentDetail> GetAllOrderShipmentDetail();
        public OrderShipmentDetail GetDeliveryPersonById(int Id);
        public OrderShipmentDetail GetOrderDetailById(int Id);
        public Messages InsertOrderShipmentDetail(OrderShipmentDetail orderShipment);
        public Messages UpdateOrderShipmentDetail(OrderShipmentDetail orderShipment);
        public Messages DeleteOrderShipmentDetail(int orderShipmentId);
        //public IEnumerable<InvoiceDetail> GetCustomerOrderDetailsById(int CustomerId);
        //public IEnumerable<InvoiceDetail> GetAllInvoiceDetail();
    }
}
