﻿using Food_Delivery.Models;
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
                var orderid = orderDetail.OrderId;

                foreach (var FoodType in orderDetail.Food)
                {
                    var foodtype = new OrderDetail()
                    {
                        CustomerId = orderDetail.CustomerId,
                        OrderId = orderid,
                        Quantity = FoodType.Quantity,
                        FoodId = FoodType.FoodId,

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
                if (updateOrder != null)
                {
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


    }
}