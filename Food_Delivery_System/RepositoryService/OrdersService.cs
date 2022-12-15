using Food_Delivery.Models;
using Food_Delivery.RepositoryInterface;

namespace Food_Delivery.RepositoryService
{
    public class OrdersService : IOrders
    {
        private readonly FoodDeliveryDbContext db;
        public OrdersService(FoodDeliveryDbContext foodDeliveryDbContext)
        {
            this.db = foodDeliveryDbContext;
        }

        public IEnumerable<Orders> GetAll()
        {
            return db.orders.ToList();
        }

        public Orders GetOrder(int orderId)
        {
            var getId = db.orders.FirstOrDefault(x => x.OrderId == orderId);
            return getId;
        }

        public Messages InsertOrder(Orders order)
        {
            Messages msg = new Messages();
            try
            {
                msg.Success = false;
                msg.Message = "This customer id not registered";
                var customerId = db.Customer.Where(x => x.CustomerId == order.CustomerId).ToList();
                if (customerId != null)
                {
                    db.Add(order);
                    db.SaveChanges();
                    msg.Success = true;
                    msg.Message = "Order added succesfully";
                    return msg;
                }
                return msg;
            }
            catch (Exception ex)
            {
                msg.Message = ex.Message;
                return msg;
            }
        }
        public Messages UpdateOrder(Orders order)
        {
            Messages msg = new Messages();
            try
            {
                msg.Success = false;
                msg.Message = "This order id not registered";
                var updateOrder = db.orders.FirstOrDefault(x => x.OrderId == order.OrderId);
                if (updateOrder != null)
                {
                    updateOrder.CustomerId = order.CustomerId;
                    db.Update(updateOrder);
                    db.SaveChanges();
                    msg.Success = true;
                    msg.Message = "Order updated succesfully!!";
                    return msg;
                }
                return msg;
            }

            catch (Exception ex)
            {
                msg.Message = ex.Message;
                return msg;
            }
        }
        public Messages DeleteOrder(int orderId)
        {
            Messages msg = new Messages();
            var deleteOrder = db.orders.FirstOrDefault(x => x.OrderId == orderId);
            if (deleteOrder != null)
            {
                db.Remove(deleteOrder);
                db.SaveChanges();
                msg.Success = true;
                msg.Message = "Order deleted succesfully";
            }
            return msg;
        }

    }
}

