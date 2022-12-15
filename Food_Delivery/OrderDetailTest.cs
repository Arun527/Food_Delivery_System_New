using Blazorise;
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
        private Mock <IOrderDetail> OrderDetailMock()
        {
           var mockservice= new Mock<IOrderDetail>();
           
           return mockservice;
        }
        private Mock<ICustomer> CustomerMock()
        {
            var mockservice = new Mock<ICustomer>();

            return mockservice;
        }

        private Mock<IHotel> HotelMock()
        {
            var mockservice = new Mock<IHotel>();

            return mockservice;
        }

        private Mock<IFood> FoodMock()
        {
            var mockservice = new Mock<IFood>();

            return mockservice;
        }

        private Mock<IOrders> OrdersMock()
        {
            var mockservice = new Mock<IOrders>();

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

        public Mock<IOrderDetail> UpdateOkMock(Messages messages)
        {
            var mockservice = new Mock<IOrderDetail>();
            mockservice.Setup(x => x.UpdateOrderDetail(It.IsAny<OrderDetail>())).Returns(messages);
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

        private Food foodData => new()
        {
            FoodId = 1,
            FoodName = "idli",
            Price = 12,
            ImageId = "d2f5",
            Type = "veg"
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

        private Customer TestCustomer = new Customer()
        {
            CustomerId = 1,
            Name = "Balaji",
            ContactNumber = "9791225793",
            Gender = "male",
            Email = "dhineshkumarm1999@gmail.com",
            Address = "Trichy",
            CreatedOn = DateTime.Now,
            IsActive = true
        };

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
        private Hotel hotelData => new()
        {
            HotelId = 1,
            HotelName = "saravanabhavan",
            Email = "Saravanabhavan123@gmail.com",
            ImageId = "d2f5",
            Type = "Veg",
            Address = "Madurai",
            ContactNumber = "9874563214"
        };
        [Fact]
        public void GetAllOrderDetail()
        {
            List<OrderDetail> list = new List<OrderDetail>();
            list.Add(test);
            var controller = new OrderDetailController(GetAllMock(list).Object,CustomerMock().Object,HotelMock().Object,FoodMock().Object,OrdersMock().Object);
            var output = controller.GetAll();
            Assert.IsType<OkObjectResult>(output);

        }

        [Fact]
        public void GetAllOrderDetailNotFound()
        {
            List<OrderDetail> list = null;
            var controller = new OrderDetailController(GetAllMock(list).Object, CustomerMock().Object, HotelMock().Object, FoodMock().Object, OrdersMock().Object);
            var output = controller.GetAll();
            Assert.IsType<NotFoundObjectResult>(output);

        }

        [Fact]
        public void GetOrderById()
        {
            var controller = new OrderDetailController(GetByIdMock(test).Object, CustomerMock().Object, HotelMock().Object, FoodMock().Object, OrdersMock().Object);
            var output = controller.GetAllById(5);
            Assert.IsType<OkObjectResult>(output);

        }

        [Fact]
        public void GetOrderByIdNotFound()
        {
            OrderDetail order = null;
            var controller = new OrderDetailController(GetByIdMock(order).Object, CustomerMock().Object, HotelMock().Object, FoodMock().Object, OrdersMock().Object);
            var output = controller.GetAllById(5);
            Assert.IsType<NotFoundObjectResult>(output);

        }

        [Fact]
        public void InsertOk()
        {
            Messages messages = new Messages();
            messages.Success = true;
            messages.Message = "Your Order Is Placed!!";
            var customerMock = new Mock<ICustomer>();
            customerMock.Setup(x => x.GetCustomerDetailById(It.IsAny<int>())).Returns(TestCustomer);

            var mockservicehotel = new Mock<IHotel>();
            mockservicehotel.Setup(x => x.GetHotelById(It.IsAny<int>())).Returns(hotelData);
            var mockserviceFood = new Mock<IFood>();
            mockserviceFood.Setup(x => x.GetFoodTypeById(It.IsAny<int>())).Returns(foodData);

            var controller = new OrderDetailController(InsertOkMock(messages).Object, customerMock.Object, mockservicehotel.Object, mockserviceFood.Object, OrdersMock().Object);
            var result = controller.InsertFoodType(testData);
            var output=result as CreatedResult;
            Assert.IsType<CreatedResult>(output);

        }

        [Fact]
        public void InsertCustomerNotOk()
        {
            Messages messages = new Messages();
            messages.Success = false;
            messages.Message = "Your Order Is Placed!!";
            var customerMock = new Mock<ICustomer>();
            Customer customer = null;
            customerMock.Setup(x => x.GetCustomerDetailById(It.IsAny<int>())).Returns(customer);

            var mockservicehotel = new Mock<IHotel>();
            mockservicehotel.Setup(x => x.GetHotelById(It.IsAny<int>())).Returns(hotelData);
            var mockserviceFood = new Mock<IFood>();
            mockserviceFood.Setup(x => x.GetFoodTypeById(It.IsAny<int>())).Returns(foodData);

            var controller = new OrderDetailController(InsertOkMock(messages).Object, customerMock.Object, mockservicehotel.Object, mockserviceFood.Object, OrdersMock().Object);
            var result = controller.InsertFoodType(testData);
            var output = result as NotFoundObjectResult;
            Assert.IsType<NotFoundObjectResult>(output);

        }
        [Fact]
        public void UpdateOk()
        {
            Messages messages = new Messages();
            messages.Success = true;
            messages.Message = "Order updated succesfully!!";

            var customerMock = new Mock<ICustomer>();
            Customer customer = TestCustomer;
            customerMock.Setup(x => x.GetCustomerDetailById(It.IsAny<int>())).Returns(customer);

            var mockservicehotel = new Mock<IHotel>();
            mockservicehotel.Setup(x => x.GetHotelById(It.IsAny<int>())).Returns(hotelData);
            var mockserviceFood = new Mock<IFood>();
            mockserviceFood.Setup(x => x.GetFoodTypeById(It.IsAny<int>())).Returns(foodData);



            var orderDetailMock = new Mock<IOrderDetail>();

            orderDetailMock.Setup(x => x.GetOrderDetail(It.IsAny<int>())).Returns(test);
            orderDetailMock.Setup(x => x.UpdateOrderDetail(It.IsAny<OrderDetail>())).Returns(messages);

            var controller = new OrderDetailController(orderDetailMock.Object, customerMock.Object, mockservicehotel.Object, mockserviceFood.Object, OrdersMock().Object);
            var result = controller.UpdateOrderDetail(test);
            var output = result as OkObjectResult;
            Assert.IsType<OkObjectResult>(output);  

        }

        [Fact]
        public void UpdateCustomerNotOKOk()
        {
            Messages messages = new Messages();
            messages.Success = true;
            messages.Message = "The customer id is not found";

            var customerMock = new Mock<ICustomer>();
            Customer customer = null;
            customerMock.Setup(x => x.GetCustomerDetailById(It.IsAny<int>())).Returns(customer);

            var mockservicehotel = new Mock<IHotel>();
            mockservicehotel.Setup(x => x.GetHotelById(It.IsAny<int>())).Returns(hotelData);
            var mockserviceFood = new Mock<IFood>();
            mockserviceFood.Setup(x => x.GetFoodTypeById(It.IsAny<int>())).Returns(foodData);



            var orderDetailMock = new Mock<IOrderDetail>();

            orderDetailMock.Setup(x => x.GetOrderDetail(It.IsAny<int>())).Returns(test);
            orderDetailMock.Setup(x => x.UpdateOrderDetail(It.IsAny<OrderDetail>())).Returns(messages);

            var controller = new OrderDetailController(orderDetailMock.Object, customerMock.Object, mockservicehotel.Object, mockserviceFood.Object, OrdersMock().Object);
            var result = controller.UpdateOrderDetail(test);
            var output = result as NotFoundObjectResult;
            Assert.IsType<NotFoundObjectResult>(output);

        }
        [Fact]
        public void UpdateOrderIdNotOk()
        {
            Messages messages = new Messages();
            messages.Success = true;
            messages.Message = "Order updated succesfully!!";

            var customerMock = new Mock<ICustomer>();
            Customer customer = TestCustomer;
            customerMock.Setup(x => x.GetCustomerDetailById(It.IsAny<int>())).Returns(customer);

            var mockservicehotel = new Mock<IHotel>();
            mockservicehotel.Setup(x => x.GetHotelById(It.IsAny<int>())).Returns(hotelData);
            var mockserviceFood = new Mock<IFood>();
            mockserviceFood.Setup(x => x.GetFoodTypeById(It.IsAny<int>())).Returns(foodData);



            var orderDetailMock = new Mock<IOrderDetail>();

            orderDetailMock.Setup(x => x.GetOrderDetail(It.IsAny<int>())).Returns(test);
            orderDetailMock.Setup(x => x.UpdateOrderDetail(It.IsAny<OrderDetail>())).Returns(messages);

            var controller = new OrderDetailController(orderDetailMock.Object, customerMock.Object, mockservicehotel.Object, mockserviceFood.Object, OrdersMock().Object);
            var result = controller.UpdateOrderDetail(test);
            var output = result as OkObjectResult;
            Assert.IsType<OkObjectResult>(output);

        }

        [Fact]
        public void DeleteOk()
        {
            Messages messages = new Messages();
            messages.Success = true;
            messages.Message = "Your Order Is Placed!!";
            var controller = new OrderDetailController(GetByIdMock(test).Object, CustomerMock().Object, HotelMock().Object, FoodMock().Object, OrdersMock().Object);

        
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
            OrderDetail order = null;
            var controller = new OrderDetailController(GetByIdMock(order).Object, CustomerMock().Object, HotelMock().Object, FoodMock().Object, OrdersMock().Object);
            var output = controller.DeleteOrderDetail(5);
            Assert.IsType<NotFoundObjectResult>(output);

        }

    }
}
