//using Food_Delivery.Controllers;
//using Food_Delivery.RepositoryInterface;
//using Food_Delivery.RepositoryService;
//using Blazorise;
//using Blazorise.Extensions;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Hosting;
//using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Text;
//using System.Threading.Tasks;
//using Moq;
//using Food_Delivery.Models;
//using Xunit.Sdk;
//using ServiceStack.DataAnnotations;
//using Food_Delivery_System.Models;
//using System.Collections.Concurrent;
//using Microsoft.CodeAnalysis.CSharp.Syntax;
//using System.ComponentModel;
//using Food_Delivery_System.Controllers_Mvc;
//using Castle.Core.Resource;
//using Microsoft.VisualStudio.TestPlatform.Utilities;

//namespace Food_Delivery
//{
//    public class OrderShipmentTest
//    {
//        private OrderShipmentRequest testData => new()
//        {
//           OrderShipmentDetailId=1,
//            DeliveryPersonId=1,
//            ShipmentRequest= TestShipment()

//        };

//        private List<OrderShipmentList> TestShipment()
//        {
//            var list = new List<OrderShipmentList>();
//            list.Add(new OrderShipmentList() { OrderDetailId = 1 });
//            return list;
//        }

//        private OrderShipmentDetail test = new OrderShipmentDetail()
//        {

//            OrderShipmentDetailId = 1,
//            OrderDetailId=1,
//            DeliveryPersonId=1

//        };
//        private OrderDetail ordertest = new OrderDetail()
//        {

//            CustomerId = 1,

//            FoodId = 1,
//            HotelId = 1,
//            Quantity = 4,

//            OrderId = 1,
//            OrderDetailId = 1,
//            OrderStatus = "Order Placed"


//        };
//        private DeliveryPerson testdeliveryperson = new DeliveryPerson()
//        {
//            DeliveryPersonId = 1,
//            DeliveryPersonName = "Mohan",
//            ContactNumber = "8547124857",
//            Gender = "Male",
//            CreatedOn = DateTime.Now,
//            IsActive = true

//        };
//        private Mock<IOrderShipmentDetail> Mock()
//        {
//            var mockservice = new Mock<IOrderShipmentDetail>();
//            return mockservice;
//        }
//        private Mock<IOrderDetail> orderdetailMock()
//        {
//            var mockservice = new Mock<IOrderDetail>();
//            return mockservice;
//        }
//        private Mock<IDeliveryPerson> deliveryMock()
//        {
//            var mockservice = new Mock<IDeliveryPerson>();
//            return mockservice;
//        }
//        private Mock<ICustomer> customerMock()
//        {
//            var mockservice = new Mock<ICustomer>();
//            return mockservice;
//        }
//        private Mock<IOrders> orderMock()
//        {
//            var mockservice = new Mock<IOrders>();
//            return mockservice;
//        }

//        private Mock<IOrderShipmentDetail> GetAllMock(List<OrderShipmentDetail> order)
//        {
//            var mockservice = Mock();
//            mockservice.Setup(x => x.GetAllOrderShipmentDetail()).Returns(order);
//            return mockservice;
//        }

//        private Mock<IOrderShipmentDetail> GetByIdMock(OrderShipmentDetail order)
//        {
//            var mockservice = Mock();
//            mockservice.Setup(x => x.GetOrderShipmentDetailById(It.IsAny<int>())).Returns(order);
//            return mockservice;
//        }
//        private Mock<IOrderShipmentDetail> AddOrderShipmentMock(Messages message)
//        {
//            var mockservice = Mock();
//            mockservice.Setup(x => x.InsertOrderShipmentDetail(It.IsAny<OrderShipmentRequest>())).Returns(message);
//            return mockservice;
//        }

//        private Mock<IOrderShipmentDetail> UpdateOrderShipmentMock(Messages message)
//        {
//            var mockservice = Mock();
//            mockservice.Setup(x => x.UpdateOrderShipmentDetail(It.IsAny<OrderShipmentDetail>())).Returns(message);
//            return mockservice;
//        }

//        private Mock<IOrderShipmentDetail> DeleteOrerShipmentMock(Messages message)
//        {
//            var mockservice = Mock();
//            mockservice.Setup(x => x.DeleteOrderShipmentDetail(It.IsAny<int>())).Returns(message);
//            return mockservice;
//        }

//        [Fact]
//        public void GetAll_OkResult()
//        {
//            List<OrderShipmentDetail> list = new List<OrderShipmentDetail>();
//            list.Add(test);
//            var controller = new OrderShipmentDetailController(GetAllMock(list).Object,orderMock().Object, orderdetailMock().Object,deliveryMock().Object,customerMock().Object);

//            var okresult = controller.GetAllOrderShipmentDetail();
//            var output = okresult as OkObjectResult;
//            Assert.IsType<OkObjectResult>(okresult);
//            Assert.StrictEqual(200, output.StatusCode);
//        }

//        [Fact]
//        public void GetAll_NotFoundResult()
//        {
//            Messages obj = new Messages();
//            obj.Success = false;
//            List<OrderShipmentDetail> list = null;
      
//            var controller = new OrderShipmentDetailController(GetAllMock(list).Object, orderMock().Object, orderdetailMock().Object, deliveryMock().Object, customerMock().Object);

//            var NotFoundResult = controller.GetAllOrderShipmentDetail();
//            var output = NotFoundResult as NotFoundObjectResult;
//            Assert.Equal("OrderShipmentDetail list is empty", output.Value);
//            Assert.StrictEqual(404, output.StatusCode);

//        }


//        [Fact]
//        public void InsertOk()
//        {
//            Messages messages = new Messages();
//            messages.Success = true;
//            messages.Message = "Your Order Is Placed!!";
//            var orderdetailMock = new Mock<IOrderDetail>();
//            orderdetailMock.Setup(x => x.GetOrderDetail(It.IsAny<int>())).Returns(ordertest);

//            var mockservicedelivery = new Mock<IDeliveryPerson>();
//            mockservicedelivery.Setup(x => x.GetDeliveryPerson(It.IsAny<int>())).Returns(testdeliveryperson);

//            var controller = new OrderShipmentDetailController(AddOrderShipmentMock(messages).Object, orderMock().Object, orderdetailMock.Object, mockservicedelivery.Object, customerMock().Object);
//            var result = controller.InsertOrderShipmentDetail(testData);
//            var output = result as CreatedResult;
//            Assert.IsType<CreatedResult>(output);

//        }


//        //[Fact]
//        //public void DeleteOk()
//        //{
//        //    Messages messages = new Messages();
//        //    messages.Success = true;
//        //    messages.Message = "OrderShipmentDetail Deleted Succesfully";
//        //    var mockservice = new Mock<IOrderShipmentDetail>();
//        //    mockservice.Setup(x => x.GetOrderShipmentDetailById(It.IsAny<int>())).Returns(test);
//        //    mockservice.Setup(x => x.DeleteOrderShipmentDetail(It.IsAny<int>())).Returns(messages);
//        //    var controller = new OrderShipmentDetailController(mockservice.Object);
//        //    var output = controller.DeleteOrderShipmentDetail(5);
//        //    var result = output as OkObjectResult;
//        //    Assert.IsType<OkObjectResult>(output);
//        //    Assert.StrictEqual(messages, result.Value);
//        //    Assert.StrictEqual(200, result.StatusCode);

//        //}


//        //[Fact]
//        //public void Delete_NotFound()
//        //{
//        //    Messages messages = new Messages();
//        //    messages.Success = true;
//        //    messages.Message = "The orderShipmentDetail id is not found";
//        //    OrderShipmentDetail order = null;
//        //    var mockservice = new Mock<IOrderShipmentDetail>();
//        //    mockservice.Setup(x => x.GetOrderShipmentDetailById(It.IsAny<int>())).Returns(order);
//        //    mockservice.Setup(x => x.DeleteOrderShipmentDetail(It.IsAny<int>())).Returns(messages);
//        //    var controller = new OrderShipmentDetailController(mockservice.Object);
//        //    var output = controller.DeleteOrderShipmentDetail(5);
//        //    var result = output as NotFoundObjectResult;
//        //    Assert.IsType<NotFoundObjectResult>(output);
//        //    Assert.StrictEqual(messages.Message, result.Value);
//        //    Assert.StrictEqual(404, result.StatusCode);
//        //}


//        //[Fact]
//        //public void GetAllInvoice_OkResult()
//        //{
//        //    List<InvoiceDetail> Lisobj = new List<InvoiceDetail>();
//        //    var mockservice = new Mock<IOrderShipmentDetail>();
//        //    mockservice.Setup(x => x.GetAllInvoiceDetail()).Returns(Lisobj);
//        //    var controller = new OrderShipmentDetailController(mockservice.Object);
//        //    var output = controller.GetAllInvoiceDetail();
//        //    var result = output as OkObjectResult;
//        //    Assert.IsType<OkObjectResult>(output);
//        //    Assert.StrictEqual(200, result.StatusCode);
//        //}


//        //[Fact]
//        //public void GetAllInvoice_NotFound()
//        //{
//        //    Messages messages = new Messages();
//        //    messages.Success = true;
//        //    messages.Message = "The invoice list is empty";
//        //    List<InvoiceDetail> Lisobj = null;
//        //    var mockservice = new Mock<IOrderShipmentDetail>();
//        //    mockservice.Setup(x => x.GetAllInvoiceDetail()).Returns(Lisobj);
//        //    var controller = new OrderShipmentDetailController(mockservice.Object);
//        //    var output = controller.GetAllInvoiceDetail();
//        //    var result = output as NotFoundObjectResult;
//        //    Assert.IsType<NotFoundObjectResult>(output);
//        //    Assert.StrictEqual(messages.Message, result.Value);
//        //    Assert.StrictEqual(404, result.StatusCode);

//        //}
//    }
//}
