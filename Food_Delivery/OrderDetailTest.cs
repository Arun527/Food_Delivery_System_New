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

        private OrderDetail testData = new OrderDetail()
        {
            OrderDetailId = 1,
            CustomerId= 1,
            OrderId = 1,
            FoodId = 1,
            HotelId = 1,
            Quantity =4,
            OrderStatus = "Order Placed",
           
        };

        [Fact]
        public void GetAllOrderDetail()
        {
            List<OrderDetail> list = new List<OrderDetail>();
            list.Add(testData);
            var mockservice = new Mock<IOrderDetail>();
            mockservice.Setup(x => x.GetAll()).Returns(list);
            var controller = new OrderDetailController(mockservice.Object);
            var output = controller.GetAll();
            Assert.IsType<OkObjectResult>(output);

        }


    }
}
