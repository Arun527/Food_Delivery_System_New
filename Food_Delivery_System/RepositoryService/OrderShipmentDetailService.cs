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

        public OrderShipmentDetail GetOrderShipmentDetailById(int Id)
        {
            var getId = db.OrderShipmentDetail.FirstOrDefault(x => x.OrderShipmentDetailId == Id);
            return getId;
        }

        public Messages InsertOrderShipmentDetail(OrderShipmentRequest orderShipment)
        {
            Messages msg = new Messages();

            OrderDetail orderDetail = new OrderDetail();
            orderDetail.OrderStatus = "Your Order Is Out For Delivery!!";


            foreach (var request in orderShipment.ShipmentRequest)
            {
                var ord = db.OrderDetail.FirstOrDefault(x => x.OrderDetailId == request.OrderDetailId);

                ord.OrderStatus = "Your Order Is Out For Delivery!!";
                db.OrderDetail.Update(ord);
                db.SaveChanges();

                var shipment = new OrderShipmentDetail()
                {
                    DeliveryPersonId = orderShipment.DeliveryPersonId,
                    OrderDetailId = request.OrderDetailId 
                };
                
                db.Add(shipment);
            }

           
            db.SaveChanges();
            msg.Success = true;
            msg.Message = " Your Order Is Out For Delivery!!";
            return msg;
        }

        public Messages UpdateOrderShipmentDetail(OrderShipmentDetail orderShipment)
        {
            Messages msg = new Messages();
            try
            { 
                var id = db.OrderShipmentDetail.FirstOrDefault(x=>x.OrderShipmentDetailId==orderShipment.OrderShipmentDetailId);
                if (id!= null)
                {
                    id.DeliveryPersonId = orderShipment.DeliveryPersonId;
                    id.OrderDetailId = orderShipment.OrderDetailId;

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


        public IEnumerable<InvoiceDeliveryPerson> GetdeliveryPersonById(int deliveryPersonId)
        {

            var orderDetails = (from ordershipment in db.OrderShipmentDetail 
                                join deliveryPerson in db.DeliveryPerson on ordershipment.DeliveryPersonId equals deliveryPerson.DeliveryPersonId
                                join OrderDetail in db.OrderDetail on ordershipment.OrderDetailId equals OrderDetail.OrderDetailId 
                                join Customer in db.Customer on OrderDetail.CustomerId equals Customer.CustomerId
                                where ordershipment.DeliveryPersonId == deliveryPersonId
                                select new InvoiceDeliveryPerson
                                {

                                 DeliveryPersonId = ordershipment.DeliveryPersonId,
                                 DeliveryPersonName= deliveryPerson.DeliveryPersonName,
                                 CustomerName = Customer.Name,
                                 Address=Customer.Address,
                                  OrderDetailId = ordershipment.OrderDetailId,
                                 OrderId= OrderDetail.OrderId,
                                 OrderShipmentdateTime=ordershipment.OrderShipmentdateTime,
                                 Quantity =OrderDetail.Quantity,
                                 Contactnumber=deliveryPerson.ContactNumber,
                                 OrderShipmentDetailId = ordershipment.OrderShipmentDetailId,
                                   
                                }).ToList();
            return orderDetails;
        }

        public IEnumerable<InvoiceDetail> GetCustomerOrderDetailsById(int CustomerId)
        {

            var orderDetails = (from orderDetail in db.OrderDetail
                                join customer in db.Customer on orderDetail.CustomerId equals customer.CustomerId
                                join Hotel in db.Hotel on orderDetail.HotelId equals Hotel.HotelId
                                join food in db.Food on orderDetail.FoodId equals food.FoodId 
                                join order in db.orders on orderDetail.OrderId equals order.OrderId
                                where orderDetail.CustomerId == CustomerId
                                select new InvoiceDetail
                                {
                                    InvoiceNumber = orderDetail.OrderDetailId,
                                    OrderId = orderDetail.OrderId,
                                    CustomerName = customer.Name,
                                    HotelName= orderDetail.Hotel.HotelName,
                                    FoodName = food.FoodName,
                                    Orderdate = order.OrderdateTime,
                                    Price = food.Price,
                                    Quantity = orderDetail.Quantity,
                                    TotalPrice = food.Price * orderDetail.Quantity,
                                    OrderStatus= orderDetail.OrderStatus
                                    
                                }).ToList();
            return orderDetails;
        }

        public IEnumerable<InvoiceDetail> GetAllInvoiceDetail()
        {
        
            var orderDetails = (from orderDetail in db.OrderDetail
                                join customer in db.Customer on orderDetail.CustomerId equals customer.CustomerId 
                                join Hotel in db.Hotel on orderDetail.HotelId equals Hotel.HotelId
                                join food in db.Food on orderDetail.FoodId equals food.FoodId 
                                join order in db.orders on orderDetail.OrderId equals order.OrderId
                                select new InvoiceDetail
                                {
                                    InvoiceNumber = orderDetail.OrderDetailId,
                                    OrderId = orderDetail.OrderId,
                                    CustomerName = customer.Name,
                                    HotelName = Hotel.HotelName,
                                    FoodName = food.FoodName,
                                    Orderdate = order.OrderdateTime,
                                    Price = food.Price,
                                    Quantity = orderDetail.Quantity,
                                    TotalPrice = food.Price * orderDetail.Quantity
                                }).ToList();
            return orderDetails;

        }

        public IEnumerable<TrackingDetail> TrackingStatus(int orderId)
        {

            var orderDetails = (from Orders in db.orders
                                join orderdetail in db.OrderDetail on Orders.OrderId equals orderdetail.OrderId
                                where orderdetail.OrderId == orderId
                                select new TrackingDetail
                                {
                                    OrderId = orderdetail.OrderId,
                                    OrderDetailId=orderdetail.OrderDetailId,
                                    OrderStatus = orderdetail.OrderStatus,

                                }).ToList();
            return orderDetails;

        }

      
    }
}


