using Food_Delivery.Models;


namespace Food_Delivery.RepositoryInterface
{
    public interface IOrders
    {
        public IEnumerable<Orders> GetAll();
        public Orders GetOrder(int orderId);
        public Messages InsertOrder(Orders order);
        public Messages UpdateOrder(Orders order);
        public Messages DeleteOrder(int orderId);

    }
}
