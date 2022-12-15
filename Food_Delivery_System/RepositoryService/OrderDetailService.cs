using Food_Delivery.Models;
using Food_Delivery.RepositoryInterface;
using Food_Delivery_System.Models;
using ServiceStack.Messaging;
using static Food_Delivery.Models.Messages;

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
            var order = db.OrderDetail.ToList();
            return order;
        }

        public IEnumerable<OrderDetail> GetAllDto()
        {
            var orderDetails = (from ordershipment in db.OrderShipmentDetail
                               
                                join OrderDetail in db.OrderDetail on ordershipment.OrderDetailId equals OrderDetail.OrderDetailId
                                where OrderDetail.OrderStatus == "Order placed"
                                select new OrderDetail
                                {
                                    
                                    OrderDetailId = ordershipment.OrderDetailId,
                                 
                                

                                    

                                }).Distinct().ToList();
            return orderDetails;
          
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
                var cusid= db.Customer.FirstOrDefault(x => x.CustomerId == orderDetail.CustomerId);
                if (cusid == null)
                {
                    msg.Success = false;
                    msg.Message = "The customer id is not found";
                    msg.Status = Statuses.NotFound;
                    return msg;
                }
                db.orders.Add(order);
                db.SaveChanges();
                var orderid = order.OrderId;

                foreach (var FoodType in orderDetail.Food)
                {
                    var foods = db.Food.Where(x => x.FoodId == FoodType.FoodId);
                    var hotel = db.Hotel.Where(x => x.HotelId == FoodType.HotelId);
                    var quantity = FoodType.Quantity;
                    if (foods == null)
                    {
                        msg.Success = false;
                        msg.Message = "The food id is not found";
                        msg.Status = Statuses.NotFound;
                        return msg;
                    }

                    if (hotel.Count() == 0)
                    {
                        msg.Success = false;
                        msg.Message = "The hotel id is not found";
                        msg.Status = Statuses.NotFound;
                        return msg;
                    }

                    if (quantity == 0)
                    {
                        msg.Success = false;
                        msg.Message = "The Quantity is Minimumof  1  ..!!";
                        msg.Status = Statuses.BadRequest;
                        return msg;
                    }
                    else
                    {
                        var foodtype = new OrderDetail()
                        {
                            CustomerId = orderDetail.CustomerId,
                            OrderId = orderid,
                            Quantity = FoodType.Quantity,
                            HotelId = FoodType.HotelId,
                            FoodId = FoodType.FoodId,
                            OrderStatus = orderDetail.OrderStatus

                        };
                        db.Add(foodtype);
                    }
                    db.SaveChanges();
                    msg.Success = true;
                    msg.Message = "Your order is placed!!";
                    msg.Status = Statuses.Created;
                   
                }
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
                var customerid = db.Customer.FirstOrDefault(x => x.CustomerId == orderDetail.CustomerId);
                var orderid=db.orders.FirstOrDefault(x=>x.OrderId == orderDetail.OrderId);  
                var hotelId=db.Hotel.FirstOrDefault(x => x.HotelId == orderDetail.HotelId);
                var foodId=db.Food.FirstOrDefault(x => x.FoodId == orderDetail.FoodId);
                var quantity = orderDetail.Quantity;
                var orderDetaileId = db.OrderDetail.FirstOrDefault(x => x.OrderDetailId == orderDetail.OrderDetailId);
                var update = db.OrderDetail.FirstOrDefault(x => x.OrderStatus == orderDetail.OrderStatus);
                if (customerid == null)
                {
                    msg.Success = false;
                    msg.Message = "This customer id not registered";
                    msg.Status = Statuses.NotFound;
                    return msg;
                }
                if (orderDetaileId == null)
                {
                    msg.Success = false;
                    msg.Message = "This orderdetail id not registered";
                    msg.Status = Statuses.NotFound;
                    return msg;
                }
                if(orderid == null)
                {
                    msg.Success = false;
                    msg.Message = "This order id not registered";
                    msg.Status = Statuses.NotFound;
                    return msg;
                }
                if (hotelId == null)
                {
                    msg.Success = false;
                    msg.Message = "The hotel id is not found";
                    msg.Status = Statuses.NotFound;
                    return msg;
                }
                if(foodId == null)
                {
                    msg.Success = false;
                    msg.Message = "The food id is not found";
                    msg.Status = Statuses.NotFound;
                    return msg;
                }
                if (quantity == 0)
                {
                    msg.Success = false;
                    msg.Message = "The Quantity is Minimumof  1  ..!!";
                    msg.Status = Statuses.BadRequest;
                    return msg;
                }
                else
                {
                    orderDetaileId.CustomerId=orderDetail.CustomerId;
                    orderDetaileId.OrderId = orderDetail.OrderId;
                    orderDetaileId.HotelId= orderDetail.HotelId;
                    orderDetaileId.FoodId = orderDetail.FoodId;
                    orderDetaileId.Quantity = orderDetail.Quantity;
                    db.Update(orderDetaileId);
                    db.SaveChanges();
                    msg.Success = true;
                    msg.Message = "Order updated succesfully!!";
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
                var order=db.OrderShipmentDetail.FirstOrDefault(x=>x.OrderDetailId==orderDetailId);
                if (order != null)
                {
                    msg.Success = false;
                    msg.Status = Statuses.BadRequest;
                    msg.Message = "Order cancellation is failed..because food is delivered..!! ";
                }
                else if (deleteOrder != null)
                {
                    db.Remove(deleteOrder);
                    db.SaveChanges();
                    msg.Success = true;
                    msg.Status=Statuses.Success;
                    msg.Message = "Order deleted succesfully";
                }
               
                else
                {
                    msg.Success = false;
                    msg.Status = Statuses.NotFound;
                    msg.Message = "Order id  is not found";
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
