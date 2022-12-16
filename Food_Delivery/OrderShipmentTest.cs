using Food_Delivery.Controllers;
using Food_Delivery.RepositoryInterface;
using Food_Delivery.RepositoryService;
using Blazorise;
using Blazorise.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Food_Delivery.Models;
using Xunit.Sdk;
using ServiceStack.DataAnnotations;
using Food_Delivery_System.Models;
using System.Collections.Concurrent;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.ComponentModel;
using Food_Delivery_System.Controllers_Mvc;
using Castle.Core.Resource;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using static Food_Delivery.Models.Messages;

namespace Food_Delivery
{
    public class OrderShipmentTest
    {
        private OrderShipmentRequest testData => new()
        {
            OrderShipmentDetailId = 1,
            DeliveryPersonId = 1,
            ShipmentRequest = TestShipment()

        };

        private List<OrderShipmentList> TestShipment()
        {
            var list = new List<OrderShipmentList>();
            list.Add(new OrderShipmentList() { OrderDetailId = 0 });
            return list;
        }
        private List<OrderShipmentList> Shipment()
        {
            var list = new List<OrderShipmentList>();
            list.Add(new OrderShipmentList() { OrderDetailId = 1 });
            return list;
        }

        private OrderShipmentDetail test = new OrderShipmentDetail()
        {

            OrderShipmentDetailId = 1,
            OrderDetailId = 1,
            DeliveryPersonId = 1

        };
        private InvoiceDeliveryPerson delivery = new InvoiceDeliveryPerson()
        {
            DeliveryPersonId = 1,
            DeliveryPersonName = "suresh",
            CustomerName = "dinesh",
            Address = "madurai",
            OrderDetailId = 1,
            OrderId = 1,
            Quantity = 5,
            Contactnumber = "9874563214",
            OrderShipmentdateTime = DateTime.Now,
            OrderShipmentDetailId = 1

        };
        private OrderDetail ordertest = new OrderDetail()
        {
            CustomerId = 1,
            FoodId = 1,
            HotelId = 1,
            Quantity = 4,
            OrderId = 1,
            OrderDetailId = 1,
            OrderStatus = "Order Placed"
        };
        private DeliveryPerson testdeliveryperson = new DeliveryPerson()
        {
            DeliveryPersonId = 1,
            DeliveryPersonName = "Mohan",
            ContactNumber = "8547124857",
            Gender = "Male",
            CreatedOn = DateTime.Now,
            IsActive = true

        };
        private Mock<IOrderShipmentDetail> Mock()
        {
            var mockservice = new Mock<IOrderShipmentDetail>();
            return mockservice;
        }
        private Mock<IOrderDetail> orderdetailMock()
        {
            var mockservice = new Mock<IOrderDetail>();
            return mockservice;
        }
        private Mock<IDeliveryPerson> deliveryMock()
        {
            var mockservice = new Mock<IDeliveryPerson>();
            return mockservice;
        }
        private Mock<ICustomer> customerMock()
        {
            var mockservice = new Mock<ICustomer>();
            return mockservice;
        }
        private Mock<IOrders> orderMock()
        {
            var mockservice = new Mock<IOrders>();
            return mockservice;
        }
        private Mock<InvoiceDeliveryPerson> InvoiceMock()
        {
            var mockservice = new Mock<InvoiceDeliveryPerson>();
            return mockservice;
        }

        private Mock<IOrderShipmentDetail> GetAllMock(List<OrderShipmentDetail> order)
        {
            var mockservice = Mock();
            mockservice.Setup(x => x.GetAllOrderShipmentDetail()).Returns(order);
            return mockservice;
        }

        private Mock<IOrderShipmentDetail> GetByIdMock(OrderShipmentDetail order)
        {
            var mockservice = Mock();
            mockservice.Setup(x => x.GetOrderShipmentDetailById(It.IsAny<int>())).Returns(order);
            return mockservice;
        }
        private Mock<IOrderShipmentDetail> AddOrderShipmentMock(Messages message)
        {
            var mockservice = Mock();
            mockservice.Setup(x => x.InsertOrderShipmentDetail(It.IsAny<OrderShipmentRequest>())).Returns(message);
            return mockservice;
        }

        private Mock<IOrderShipmentDetail> UpdateOrderShipmentMock(Messages message)
        {
            var mockservice = Mock();
            mockservice.Setup(x => x.UpdateOrderShipmentDetail(It.IsAny<OrderShipmentDetail>())).Returns(message);
            return mockservice;
        }

        private Mock<IOrderShipmentDetail> DeleteOrerShipmentMock(Messages message)
        {
            var mockservice = Mock();
            mockservice.Setup(x => x.DeleteOrderShipmentDetail(It.IsAny<int>())).Returns(message);
            return mockservice;
        }



        [Fact]
        public void GetAll_OkResult()
        {
            List<OrderShipmentDetail> list = new List<OrderShipmentDetail>();
            list.Add(test);
            var controller = new OrderShipmentDetailController(GetAllMock(list).Object, orderMock().Object, orderdetailMock().Object, deliveryMock().Object, customerMock().Object);

            var okresult = controller.GetAllOrderShipmentDetail();
            var output = okresult as OkObjectResult;
            Assert.IsType<OkObjectResult>(okresult);
            Assert.StrictEqual(200, output.StatusCode);
        }

        [Fact]
        public void GetAll_NotFoundResult()
        {
            Messages obj = new Messages();
            obj.Success = false;
            List<OrderShipmentDetail> list = null;

            var controller = new OrderShipmentDetailController(GetAllMock(list).Object, orderMock().Object, orderdetailMock().Object, deliveryMock().Object, customerMock().Object);

            var NotFoundResult = controller.GetAllOrderShipmentDetail();
            var output = NotFoundResult as NotFoundObjectResult;
            Assert.Equal("OrderShipmentDetail list is empty", output.Value);
            Assert.StrictEqual(404, output.StatusCode);

        }

        //[Fact]
        //public void GetDeliveryPersonById_Ok()
        //{
        //    Messages obj = new Messages();
        //    obj.Success = true;

        //    DeliveryPerson deliveryid = new DeliveryPerson();
        //    var service = new Mock<IDeliveryPerson>();
        //    service.Setup(x => x.GetDeliveryPerson(It.IsAny<int>())).Returns(testdeliveryperson);
        //    var mockservice = new Mock<IOrderShipmentDetail>();
        //    var controller = new OrderShipmentDetailController(mockservice(delivery).Object, orderMock().Object, orderdetailMock().Object, deliveryMock().Object, customerMock().Object);

        //    var okResulut = controller.GetDeliveryPersonById(2);
        //    var output = okResulut as OkObjectResult;
        //    Assert.StrictEqual(200, output.StatusCode);
        //    Assert.IsType<OkObjectResult>(output);
        //}

        //[Fact]
        //public void GetDeliveryPersonById_NotFound()
        //{
        //    Messages obj = new Messages();
        //    obj.Success = false;
        //    List<OrderShipmentDetail> list = null;

        //    var controller = new OrderShipmentDetailController(GetAllMock(list).Object, orderMock().Object, orderdetailMock().Object, deliveryMock().Object, customerMock().Object);

        //    var NotFoundResult = controller.GetAllOrderShipmentDetail();
        //    var output = NotFoundResult as NotFoundObjectResult;
        //    Assert.Equal("OrderShipmentDetail list is empty", output.Value);
        //    Assert.StrictEqual(404, output.StatusCode);

        //}

        [Fact]
        public void GetById_OkResult()
        {
            var controller = new OrderShipmentDetailController(GetByIdMock(test).Object, orderMock().Object, orderdetailMock().Object, deliveryMock().Object, customerMock().Object);
            var okresult = controller.GetOrderShipmentDetailById(2);
            var list = okresult as OkObjectResult;
            var result = list.Value as OrderShipmentDetail;
            Assert.IsType<OkObjectResult>(okresult);
            Assert.NotNull(result);
            Assert.StrictEqual(1, result.OrderShipmentDetailId);
            Assert.StrictEqual(200, list.StatusCode);
        }

        [Fact]
        public void GetById_NotFoundResult()
        {
            Messages msg = new Messages();
            msg.Success = true;
            msg.Message = "OrderShipmentDetail id is not found";
            msg.Status = Statuses.NotFound;
            OrderShipmentDetail order = null;
            var mockservice = new Mock<IOrderShipmentDetail>();
            var controller = new OrderShipmentDetailController(GetByIdMock(order).Object, orderMock().Object, orderdetailMock().Object, deliveryMock().Object, customerMock().Object);
            var NotFoundResult = controller.GetOrderShipmentDetailById(2);
            var output = NotFoundResult as NotFoundObjectResult;
            Assert.IsType<NotFoundObjectResult>(NotFoundResult);
            Assert.StrictEqual(msg.Message, output.Value);
            Assert.StrictEqual(404, output.StatusCode);
        }

        [Fact]
        public void InsertOk()
        {
            Messages messages = new Messages();
            messages.Success = true;
            messages.Message = "Your Order Is Placed!!";
            messages.Status = Statuses.Created;
            var controller = new OrderShipmentDetailController(AddOrderShipmentMock(messages).Object, orderMock().Object, orderdetailMock().Object, deliveryMock().Object, customerMock().Object);
            var result = controller.InsertOrderShipmentDetail(testData);
            var output = result as CreatedResult;
            Assert.IsType<CreatedResult>(output);

        }

        [Fact]
        public void Insert_DeliveryPersonIdBadRequest()
        {
            OrderShipmentRequest shipmentRequest = new OrderShipmentRequest { OrderShipmentDetailId = 1, DeliveryPersonId = 0 };
            Messages msg = new Messages();
            msg.Success = false;
            msg.Message = "The delivery person id field is required";
            msg.Status = Statuses.BadRequest;
            var controller = new OrderShipmentDetailController(AddOrderShipmentMock(msg).Object, orderMock().Object, orderdetailMock().Object, deliveryMock().Object, customerMock().Object);
            var output = controller.InsertOrderShipmentDetail(shipmentRequest);
            var result = output as BadRequestObjectResult;
            Assert.IsType<BadRequestObjectResult>(output);
            Assert.StrictEqual(msg.Message, result.Value);
            Assert.StrictEqual(400, result.StatusCode);
        }

        [Fact]
        public void Insert_OrderDetailIdBadRequest()
        {
            OrderShipmentRequest shipmentRequest = new OrderShipmentRequest { ShipmentRequest = TestShipment() };
            Messages msg = new Messages();
            msg.Success = false;
            msg.Message = "The ordershipment detail id field is required";
            msg.Status = Statuses.BadRequest;
            var controller = new OrderShipmentDetailController(AddOrderShipmentMock(msg).Object, orderMock().Object, orderdetailMock().Object, deliveryMock().Object, customerMock().Object);
            var output = controller.InsertOrderShipmentDetail(shipmentRequest);
            var result = output as BadRequestObjectResult;
            Assert.IsType<BadRequestObjectResult>(output);
            Assert.StrictEqual(msg.Message, result.Value);
            Assert.StrictEqual(400, result.StatusCode);
        }


        [Fact]
        public void Insert_NotFound()
        {
            Messages msg = new Messages();
            msg.Success = false;
            msg.Message = "The orderdetail id is not found";
            msg.Status = Statuses.NotFound;
            var controller = new OrderShipmentDetailController(AddOrderShipmentMock(msg).Object, orderMock().Object, orderdetailMock().Object, deliveryMock().Object, customerMock().Object);
            var output = controller.InsertOrderShipmentDetail(testData);
            var result = output as NotFoundObjectResult;
            Assert.IsType<NotFoundObjectResult>(output);
            Assert.StrictEqual(msg.Message, result.Value);
            Assert.StrictEqual(404, result.StatusCode);
        }

        [Fact]
        public void Update_OKResult()
        {
            Messages obj = new Messages();
            obj.Success = true;
            obj.Message = "OrderShipmentDetail updated succesfully";
            OrderShipmentRequest obj1 = (new OrderShipmentRequest
            {
                OrderShipmentDetailId = 1,
                DeliveryPersonId = 1,
                ShipmentRequest = Shipment()
            });
            List<OrderShipmentRequest> Obj = new List<OrderShipmentRequest>();
            Obj.Add(obj1);
            var mockservice = new Mock<IFood>();
            var controller = new OrderShipmentDetailController(UpdateOrderShipmentMock(obj).Object, orderMock().Object, orderdetailMock().Object, deliveryMock().Object, customerMock().Object);
            var output = controller.UpdateOrderShipmentDetail(test);
            var result = output as OkObjectResult;
            Assert.IsType<OkObjectResult>(output);
            Assert.StrictEqual(obj.Message, result.Value);
            Assert.StrictEqual(200, result.StatusCode);
        }

        [Fact]
        public void Update_NotFoundResult()
        {
            Messages msg = new Messages();
            msg.Success = true;
            msg.Message = "The ordershipment id is not found";
            msg.Status = Statuses.NotFound;
            var controller = new OrderShipmentDetailController(UpdateOrderShipmentMock(msg).Object, orderMock().Object, orderdetailMock().Object, deliveryMock().Object, customerMock().Object);
            var output = controller.UpdateOrderShipmentDetail(test);
            var result = output as NotFoundObjectResult;
            Assert.IsType<NotFoundObjectResult>(output);
            Assert.StrictEqual(msg.Message, result.Value);
            Assert.StrictEqual(404, result.StatusCode);
        }

        [Fact]
        public void Update_DeliveryPersonIdBadRequest()
        {
            OrderShipmentDetail shipmentRequest = new OrderShipmentDetail { OrderDetailId = 1, DeliveryPersonId = 0, OrderShipmentDetailId = 1 };
            Messages msg = new Messages();
            msg.Success = false;
            msg.Message = "The delivery person id field is required";
            msg.Status = Statuses.BadRequest;
            var controller = new OrderShipmentDetailController(UpdateOrderShipmentMock(msg).Object, orderMock().Object, orderdetailMock().Object, deliveryMock().Object, customerMock().Object);
            var output = controller.UpdateOrderShipmentDetail(test);
            var result = output as BadRequestObjectResult;
            Assert.IsType<BadRequestObjectResult>(output);
            Assert.StrictEqual(msg.Message, result.Value);
            Assert.StrictEqual(400, result.StatusCode);
        }

        [Fact]
        public void Update_OrdershipmentIdBadRequest()
        {
            OrderShipmentDetail shipmentRequest = new OrderShipmentDetail { OrderDetailId = 1, DeliveryPersonId = 1, OrderShipmentDetailId = 0 };
            Messages msg = new Messages();
            msg.Success = false;
            msg.Message = "The ordershipment detail id field is required";
            msg.Status = Statuses.BadRequest;
            var controller = new OrderShipmentDetailController(UpdateOrderShipmentMock(msg).Object, orderMock().Object, orderdetailMock().Object, deliveryMock().Object, customerMock().Object);
            var output = controller.UpdateOrderShipmentDetail(test);
            var result = output as BadRequestObjectResult;
            Assert.IsType<BadRequestObjectResult>(output);
            Assert.StrictEqual(msg.Message, result.Value);
            Assert.StrictEqual(400, result.StatusCode);
        }

        [Fact]
        public void Update_OrderdetailIdBadRequest()
        {
            OrderShipmentDetail shipmentRequest = new OrderShipmentDetail { OrderDetailId = 0, DeliveryPersonId = 1, OrderShipmentDetailId = 1 };
            Messages msg = new Messages();
            msg.Success = false;
            msg.Message = "The orderdetail id field is required";
            msg.Status = Statuses.BadRequest;
            var controller = new OrderShipmentDetailController(UpdateOrderShipmentMock(msg).Object, orderMock().Object, orderdetailMock().Object, deliveryMock().Object, customerMock().Object);
            var output = controller.UpdateOrderShipmentDetail(shipmentRequest);
            var result = output as BadRequestObjectResult;
            Assert.IsType<BadRequestObjectResult>(output);
            Assert.StrictEqual(msg.Message, result.Value);
            Assert.StrictEqual(400, result.StatusCode);
        }

        [Fact]
        public void DeleteOk()
        {
            Messages messages = new Messages();
            messages.Success = true;
            messages.Message = "OrderShipmentDetail Deleted Succesfully";
            var mockservice = new Mock<IOrderShipmentDetail>();
            mockservice.Setup(x => x.DeleteOrderShipmentDetail(It.IsAny<int>())).Returns(messages);
            var controller = new OrderShipmentDetailController(DeleteOrerShipmentMock(messages).Object, orderMock().Object, orderdetailMock().Object, deliveryMock().Object, customerMock().Object);
            var output = controller.DeleteOrderShipmentDetail(5);
            var result = output as OkObjectResult;
            Assert.IsType<OkObjectResult>(output);
            Assert.StrictEqual(messages.Message, result.Value);
            Assert.StrictEqual(200, result.StatusCode);

        }


        [Fact]
        public void Delete_NotFound()
        {
            Messages messages = new Messages();
            messages.Success = true;
            messages.Message = "The orderShipmentDetail id is not found";
            messages.Status = Statuses.NotFound;
            OrderShipmentDetail order = null;
            var mockservice = new Mock<IOrderShipmentDetail>();
            mockservice.Setup(x => x.DeleteOrderShipmentDetail(It.IsAny<int>())).Returns(messages);
            var controller = new OrderShipmentDetailController(mockservice.Object, orderMock().Object, orderdetailMock().Object, deliveryMock().Object, customerMock().Object);
            var output = controller.DeleteOrderShipmentDetail(5);
            var result = output as NotFoundObjectResult;
            Assert.IsType<NotFoundObjectResult>(output);
            Assert.StrictEqual(messages.Message, result.Value);
            Assert.StrictEqual(404, result.StatusCode);
        }


        [Fact]
        public void GetAllInvoice_OkResult()
        {
            List<InvoiceDetail> Lisobj = new List<InvoiceDetail>();
            var mockservice = new Mock<IOrderShipmentDetail>();
            mockservice.Setup(x => x.GetAllInvoiceDetail()).Returns(Lisobj);
            var controller = new OrderShipmentDetailController(mockservice.Object, orderMock().Object, orderdetailMock().Object, deliveryMock().Object, customerMock().Object);
            var output = controller.GetAllInvoiceDetail();
            var result = output as OkObjectResult;
            Assert.IsType<OkObjectResult>(output);
            Assert.StrictEqual(200, result.StatusCode);
        }


        [Fact]
        public void GetAllInvoice_NotFound()
        {
            Messages messages = new Messages();
            messages.Success = true;
            messages.Message = "The invoice list is empty";
            List<InvoiceDetail> Lisobj = null;
            var mockservice = new Mock<IOrderShipmentDetail>();
            mockservice.Setup(x => x.GetAllInvoiceDetail()).Returns(Lisobj);
            var controller = new OrderShipmentDetailController(mockservice.Object, orderMock().Object, orderdetailMock().Object, deliveryMock().Object, customerMock().Object);
            var output = controller.GetAllInvoiceDetail();
            var result = output as NotFoundObjectResult;
            Assert.IsType<NotFoundObjectResult>(output);
            Assert.StrictEqual(messages.Message, result.Value);
            Assert.StrictEqual(404, result.StatusCode);

        }
    }
}
