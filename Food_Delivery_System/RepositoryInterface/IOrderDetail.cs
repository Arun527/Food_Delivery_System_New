using Food_Delivery.Models;

namespace Food_Delivery.RepositoryInterface
{
    public interface IOrderDetail
    {
        public IEnumerable<OrderDetail> GetAll();
        public OrderDetail GetOrderDetail(int orderDetailId);
        public Messages InsertOrderDetail(OrderRequest orderDetail);
        public Messages UpdateOrderDetail(OrderDetail orderDetail);
        public Messages DeleteOrderDetail(int orderDetailId);

        public IEnumerable<OrderDetail> GetAllDto();



    }
}
