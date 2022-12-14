using Food_Delivery.Controllers;
using Food_Delivery.Models;
using Food_Delivery.RepositoryInterface;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food_Delivery
{
    public class OrderDetailTest
    {
        private Mock <IOrderDetail> Mock()
        {
           var mockservice= new Mock<IOrderDetail>();
           return mockservice;
        }

        private Mock <IOrderDetail> GetAllMock(List<OrderDetail> listobj)
        {
            var mockservice=new Mock<IOrderDetail>();
            mockservice.Setup(x => x.GetAll()).Returns(listobj);
            return mockservice;
        }

        public Mock <IOrderDetail>GetByIdMock(OrderDetail obj)
        {
            var mockservice = new Mock<IOrderDetail>();
            mockservice.Setup(x => x.GetOrderDetail(It.IsAny<int>())).Returns(obj);
            return mockservice;
        }

        public Mock<IOrderDetail>InsertOkMock(Messages messages)
        {
            var mockservice = new Mock<IOrderDetail>();
            mockservice.Setup(x => x.InsertOrderDetail(It.IsAny<OrderRequest>())).Returns(messages);
            return mockservice;
        }

        private OrderRequest testData => new ()
        {
            OrderDetailId = 1,
            CustomerId= 1,
            OrderId=1,
            OrderStatus = "Order Placed",
            Food= testDataList()

        };
        private List<FoodDetaile> testDataList()
        {
           var list=new List<FoodDetaile>();
            list.Add(new FoodDetaile()
            {
                FoodId = 1,
                HotelId = 1,
                Quantity = 4
            });
            return list;
        }

        private OrderDetail test = new OrderDetail()
        {

            CustomerId = 1,

            FoodId = 1,
            HotelId = 1,
            Quantity = 4,

            OrderId = 1,
            OrderDetailId = 1,
            OrderStatus = "Order Placed"


        };

        [Fact]
        public void GetAllOrderDetail()
        {
            List<OrderDetail> list = new List<OrderDetail>();
            list.Add(test);
            var controller = new OrderDetailController(GetAllMock(list).Object);
            var output = controller.GetAll();
            Assert.IsType<OkObjectResult>(output);

        }

        [Fact]
        public void GetAllOrderDetailNotFound()
        {
            List<OrderDetail> list =null;
          
            var controller = new OrderDetailController(GetAllMock(list).Object);
            var output = controller.GetAll();
            Assert.IsType<NotFoundObjectResult>(output);

        }

        [Fact]
        public void GetOrderById()
        {
            var controller = new OrderDetailController(GetByIdMock(test).Object);
            var output = controller.GetAllById(5);
            Assert.IsType<OkObjectResult>(output);

        }

        [Fact]
        public void GetOrderByIdNotFound()
        {
            OrderDetail order=null;
            var controller = new OrderDetailController(GetByIdMock(order).Object);
            var output = controller.GetAllById(5);
            Assert.IsType<NotFoundObjectResult>(output);

        }

        [Fact]
        public void InsertOk()
        {
            Messages messages = new Messages();
            messages.Success = false;
            messages.Message = "Your Order Is Placed!!";
            var mockservice = new Mock<IOrderDetail>();
            mockservice.Setup(x=>x.GetOrderDetail(It.IsAny<int>())).Returns(test);
            mockservice.Setup(x=>x.InsertOrderDetail(It.IsAny<OrderRequest>())).Returns(messages);
            var controller=mockservice.Object;
            var output = controller.InsertOrderDetail(testData);
            Assert.IsType<OkObjectResult>(output);

        }


        [Fact]
        public void UpdateOk()
        {

        }




        [Fact]
        public void DeleteOk()
        {
            Messages messages = new Messages();
            messages.Success = true;
            messages.Message = "Your Order Is Placed!!";
            var mockservice = new Mock<IOrderDetail>();
            mockservice.Setup(x=>x.GetOrderDetail(It.IsAny<int>())).Returns(test);
            mockservice.Setup(x=>x.DeleteOrderDetail(It.IsAny<int>())).Returns(messages);
            var controller = new OrderDetailController(mockservice.Object);
            var output = controller.DeleteOrderDetail(5);
            Assert.IsType<OkObjectResult>(output);

        }

        [Fact]
        public void DeleteNotOk()
        {
            Messages messages = new Messages();
            messages.Success = false;
            messages.Message = "Your Order Is Placed!!";
            OrderDetail ord = null;
            var mockservice = new Mock<IOrderDetail>();
            mockservice.Setup(x => x.GetOrderDetail(It.IsAny<int>())).Returns(ord);
            mockservice.Setup(x => x.DeleteOrderDetail(It.IsAny<int>())).Returns(messages);
            var controller = new OrderDetailController(mockservice.Object);
            var output = controller.DeleteOrderDetail(5);
            Assert.IsType<NotFoundObjectResult>(output);

        }

    }
}
