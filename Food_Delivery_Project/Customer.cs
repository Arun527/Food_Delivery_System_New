using Food_Delivery_System.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit.Sdk;
using Food_Delivery.RepositoryInterface;
using Food_Delivery.Controllers;
using Food_Delivery.Models;
using System.Reflection;
using System.Net;
using System;
using Microsoft.AspNetCore.Http;

namespace Food_Delivery_Project
{
    public class CustomerTest
    {
        [Fact]
        public void GetAllCustomer_OkResult()
        {
            Customer obj = (new Customer
            {
                CustomerId = 1,
                Name = "Dineshkumar",
                ContactNumber = "9876543232",
                Gender = "Male",
                Email = "dhineshkumarm1999@gmail.com",
                Address = "80/7,middle street ,Madurai"
            });
            List<Customer> Lisobj = new List<Customer>();
            Lisobj.Add(obj);
            var mockservice = new Mock<ICustomer>();
            mockservice.Setup(x => x.GetAll()).Returns(Lisobj);
            var controller = new CustomerController(mockservice.Object);

            var okresult = controller.GetAll();

            Assert.IsType<OkObjectResult>(okresult);
        }


        //[Fact]
        //public void GetAllCustomer_NotFound()
        //{

        //    Customer obj = (new Customer
        //    {
        //        CustomerId = 1,
        //        Name = "Dineshkumar",
        //        ContactNumber = "9876543232",
        //        Gender = "Male",
        //        Email = "dhineshkumarm1999@gmail.com",
        //        Address = "80/7,middle street ,Madurai"
        //    });
        //    List<Customer> Lisobj = new List<Customer>();
        //    var mockservice = new Mock<ICustomer>();
        //    mockservice.Setup(x => x.GetAll()).Returns(Lisobj);
        //    var controller = new CustomerController(mockservice.Object);

        //    var exception = controller.GetAll();

        //    Assert.IsType<NotFoundResult>(exception);
        //}



        [Fact]
        public void GetCustomerDetailById_OkResult()
        {

            Customer obj = (new Customer
            {
                CustomerId = 1,
                Name = "Dineshkumar",
                ContactNumber = "9876543232",
                Gender = "Male",
                Email = "dhineshkumarm1999@gmail.com",
                Address = "80/7,middle street ,Madurai"
            });
            List<Customer> Lisobj = new List<Customer>();
            var mockservice = new Mock<ICustomer>();
            mockservice.Setup(x => x.GetCustomerDetailById(It.IsAny<int>())).Returns(obj);
            var controller = new CustomerController(mockservice.Object);

            var exception = controller.GetCustomerDetailById(2);

            Assert.IsType<OkObjectResult>(exception);
        }


        // [Fact]
        //public void GetCustomerDetailById_NotFoundResult()
        //{

        //    Customer obj = null;
        //    List<Customer> Lisobj = new List<Customer>();
        //    var mockservice = new Mock<ICustomer>();
        //    mockservice.Setup(x => x.GetCustomerDetailById(It.IsAny<int>())).Returns(obj);
        //    var controller = new CustomerController(mockservice.Object);

        //    var exception = controller.GetCustomerDetailById(2);


        //    Assert.Equals(exception);
        //}



        [Fact]
        public void GetCustomerDetailByNumber_OkResult()
        {

            Customer obj = (new Customer
            {
                CustomerId = 1,
                Name = "Dineshkumar",
                ContactNumber = "9876543232",
                Gender = "Male",
                Email = "dhineshkumarm1999@gmail.com",
                Address = "80/7,middle street ,Madurai"
            });
            List<Customer> Lisobj = new List<Customer>();
            var mockservice = new Mock<ICustomer>();
            mockservice.Setup(x => x.GetCustomerDetailById(It.IsAny<int>())).Returns(obj);
            var controller = new CustomerController(mockservice.Object);

            var exception = controller.GetCustomerDetailById(2);

            Assert.IsType<OkObjectResult>(exception);
        }























    }


}