using Food_Delivery.Models;
using Food_Delivery.RepositoryInterface;
using ServiceStack.Messaging;

namespace Food_Delivery.RepositoryService
{
    public class OrderDetailService : IOrderDetail
    {
        private readonly FoodDeliveryDbContext db;
        public OrderDetailService(FoodDeliveryDbContext foodDeliveryDbContext)
        {
            this.db = foodDeliveryDbContext;
        }

        public IEnumerable<OrderDetail> GetAll()
        {
            return db.OrderDetail.ToList();
        }

        public OrderDetail GetOrderDetail(int orderDetailId)
        {
            var getId = db.OrderDetail.FirstOrDefault(x => x.OrderDetailId == orderDetailId);
            return getId;
        }

        public Messages InsertOrderDetail(OrderRequest orderDetail)
        {   
            Messages msg = new Messages();
            try
            {
                Orders order = new Orders();
                order.CustomerId = orderDetail.CustomerId;
                db.orders.Add(order);
                db.SaveChanges();
                var orderid = order.OrderId;

                foreach (var FoodType in orderDetail.Food)
                {
                    var foodtype = new OrderDetail()
                    {
                        CustomerId = orderDetail.CustomerId,
                        OrderId = orderid,
                        Quantity = FoodType.Quantity,
                        HotelId = FoodType.HotelId,
                        FoodId = FoodType.FoodId,
                        OrderStatus=orderDetail.OrderStatus

                    };
                    db.Add(foodtype);
                }
                db.SaveChanges();
                msg.Success = true;
                msg.Message = "Your Order Is Placed!!";
                return msg;

            }
            catch (Exception ex)
            {
                msg.Message = ex.Message;
                return msg;
            }
        }




        public Messages UpdateOrderDetail(OrderDetail orderDetail)
        {
            Messages msg = new Messages();
            try
            {
                msg.Success = false;
                msg.Message = "This OrderId not registered";
                var updateOrder = db.OrderDetail.FirstOrDefault(x => x.OrderDetailId == orderDetail.OrderDetailId);
                var update = db.OrderDetail.FirstOrDefault(x => x.OrderStatus == orderDetail.OrderStatus);
                if (updateOrder != null && update.OrderStatus=="Order Placed")
                {
                    updateOrder.HotelId= orderDetail.HotelId;
                    updateOrder.FoodId = orderDetail.FoodId;
                    updateOrder.Quantity = orderDetail.Quantity;
                    db.Update(updateOrder);
                    db.SaveChanges();
                    msg.Success = true;
                    msg.Message = "Order Updated Succesfully!!";
                }
                return msg;
            }

            catch (Exception ex)
            {
                msg.Message = ex.Message;
                return msg;
            }
        }


        public Messages DeleteOrderDetail(int orderDetailId)
        {
            Messages msg = new Messages();
            try
            {
                var deleteOrder = db.OrderDetail.FirstOrDefault(x => x.OrderDetailId == orderDetailId);
                if (deleteOrder != null)
                {
                    db.Remove(deleteOrder);
                    db.SaveChanges();
                    msg.Success = true;
                    msg.Message = "Order Deleted Succesfully";
                }
                return msg;
            }
            catch (Exception ex)
            {
                msg.Message = ex.Message;
                return msg;
            }

        }

    }
}
