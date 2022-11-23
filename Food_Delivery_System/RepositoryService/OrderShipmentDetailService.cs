using Food_Delivery.Models;
using Food_Delivery.RepositoryInterface;
using Food_Delivery_System.Models;
using System.Security.Cryptography;

namespace Food_Delivery.RepositoryService
{
    public class OrderShipmentDetailService : IOrderShipmentDetail
    {
        private readonly FoodDeliveryDbContext db;


        public OrderShipmentDetailService(FoodDeliveryDbContext foodDeliveryDbContext)
        {
            this.db = foodDeliveryDbContext;
        }

        public IEnumerable<OrderShipmentDetail> GetAllOrderShipmentDetail()
        {
            return db.OrderShipmentDetail.ToList();
        }

        public OrderShipmentDetail GetDeliveryPersonById(int Id)
        {
            var getId = db.OrderShipmentDetail.FirstOrDefault(x => x.DeliveryPersonId == Id);
            return getId;
        }

        public OrderShipmentDetail GetOrderDetailById(int Id)
        {
            var getId = db.OrderShipmentDetail.FirstOrDefault(x => x.OrderDetailId == Id);
            return getId;
        }

        public Messages InsertOrderShipmentDetail(OrderShipmentRequest orderShipment)
        {
            Messages msg = new Messages();
            foreach(var request in orderShipment.ShipmentRequest)
            {
                var shipment = new OrderShipmentDetail()
                {
                    DeliveryPersonId=orderShipment.DeliveryPersonId,
                    OrderDetailId = request.OrderDetailId
                };
              
                var status = db.OrderDetail.FirstOrDefault(x => x.OrderDetailId == shipment.OrderDetailId);
                status.OrderStatus = "Thankyou For Your Valuble Order";
                
                db.Add(shipment);
            }
                db.SaveChanges();
                msg.Success = true;
                msg.Message = " Your Order Is Out Of Delivery!!";
                return msg;
        }

        public Messages UpdateOrderShipmentDetail(OrderShipmentDetail orderShipment)
        {
            Messages msg = new Messages();
            try
            {
                var deliveryPersonId = db.DeliveryPerson.FirstOrDefault(x => x.DeliveryPersonId == orderShipment.DeliveryPersonId);
                var orderDetailId = db.OrderDetail.FirstOrDefault(x => x.OrderDetailId == orderShipment.OrderDetailId);
                var orderShipmentId = db.OrderShipmentDetail.FirstOrDefault(x => x.OrderShipmentDetailId == orderShipment.OrderShipmentDetailId);

                if (deliveryPersonId == null)
                {
                    msg.Success = false;
                    msg.Message = "This DeliveryPersonId id not registered";
                }
                if (orderDetailId == null)
                {
                    msg.Success = false;
                    msg.Message = "This orderDetailId id not registered";
                }
                if (orderShipmentId != null)
                {
                    orderShipmentId.DeliveryPersonId = orderShipment.DeliveryPersonId;
                    orderShipmentId.OrderDetailId = orderShipment.OrderDetailId;
                    db.Update(orderShipment);
                    db.SaveChanges();
                    msg.Success = true;
                    msg.Message = "OrderShipmentDetail Updated Succesfully";
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


        public Messages DeleteOrderShipmentDetail(int orderShipmentId)
        {
            Messages msg = new Messages();
            var orderShipmentDetailId = db.OrderShipmentDetail.FirstOrDefault(x => x.OrderShipmentDetailId == orderShipmentId);
            if (orderShipmentDetailId != null)
            {
                db.Remove(orderShipmentDetailId);
                db.SaveChanges();
                msg.Success = true;
                msg.Message = "OrderShipmentDetail Deleted Succesfully";
            }
            return msg;
        }

        public IEnumerable<InvoiceDetail> GetCustomerOrderDetailsById(int CustomerId)
        { 
                    var orderDetails = (from orderDetail in db.OrderDetail
                                        join customer in db.Customer on orderDetail.CustomerId equals customer.CustomerId
                                        join food in db.Food on orderDetail.FoodId equals food.FoodId
                                        join order in db.orders on orderDetail.OrderId equals order.OrderId
                                        where orderDetail.CustomerId == CustomerId
                                        select new InvoiceDetail
                                        {
                                            InvoiceNumber = orderDetail.OrderDetailId,
                                            OrderId = orderDetail.OrderId,
                                            CustomerName = customer.Name,
                                            FoodName = food.FoodName,
                                            Orderdate = order.OrderdateTime,
                                            Price = food.Price,
                                            Quantity = orderDetail.Quantity,
                                            TotalPrice = food.Price * orderDetail.Quantity
                                        }).ToList();
                    return orderDetails;
        }

        public IEnumerable<InvoiceDetail> GetAllInvoiceDetail()
        {
            try
            {
                
                var orderDetails = (from orderDetail in db.OrderDetail
                                    join customer in db.Customer on orderDetail.CustomerId equals customer.CustomerId
                                    join food in db.Food on orderDetail.FoodId equals food.FoodId
                                    join order in db.orders on orderDetail.OrderId equals order.OrderId
                                    select new InvoiceDetail
                                    {
                                        InvoiceNumber = orderDetail.OrderDetailId,
                                        OrderId = orderDetail.OrderId,
                                        CustomerName = customer.Name,
                                        FoodName = food.FoodName,
                                        Orderdate = order.OrderdateTime,
                                        Price = food.Price,
                                        Quantity = orderDetail.Quantity,
                                        TotalPrice = food.Price * orderDetail.Quantity
                                    }).ToList();
                return orderDetails;
            }
            catch(Exception ex)
            {
                throw;
            }
           

        }



    }
}


