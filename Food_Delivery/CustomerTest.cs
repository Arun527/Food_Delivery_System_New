using Azure;
using Food_Delivery.Controllers;
using Food_Delivery.Models;
using Food_Delivery.RepositoryInterface;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Food_Delivery
{
    public class CustomerTest
    {

        private Mock <ICustomer> Mock()
        {
            var mockservice = new Mock<ICustomer>();
            return mockservice;
        }

        private Mock <ICustomer> GetallMock(List<Customer> Lisobj)
        {

            var mockservice=Mock();
            mockservice.Setup(x => x.GetAll()).Returns(Lisobj);
            return mockservice;
        }

        private Mock<ICustomer> GetByIdMock(Customer obj)
        {
            var mockservice = Mock();
            mockservice.Setup(x => x.GetCustomerDetailById(It.IsAny<int>())).Returns(obj);
            return mockservice;
        }

        private Mock<ICustomer> GetByNumberMock(Customer obj)
        {
            var mockservice = Mock();
            mockservice.Setup(x => x.GetCustomerDetailByNumber(It.IsAny<string>())).Returns(obj);
            return mockservice;
        }

        private Mock<ICustomer> GetByEmailMock(Customer obj)
        {

            var mockservice = Mock();
            mockservice.Setup(x => x.GetCustomerDetailByEmail(It.IsAny<string>())).Returns(obj);
            return mockservice;
        }


        private Mock<ICustomer> InsertMock(Messages msg)
        {

            var mockservice = Mock();
            mockservice.Setup(x => x.InsertCustomerDetail(It.IsAny<Customer>())).Returns(msg);
            return mockservice;
        }
        private Mock<ICustomer> UpdateMock(Messages msg)
        {
            
            var mockservice = Mock();
            //mockservice.Setup(x => x.GetCustomerDetailById(It.IsAny<int>())).Returns(TestData);
            //return 

            mockservice.Setup(x => x.UpdateCustomerDetail(It.IsAny<Customer>())).Returns(msg);
            
            return mockservice;
        }
        private Mock<ICustomer> DeleteMock(Messages msg)
        {

            var mockservice = Mock();
            mockservice.Setup(x => x.DeleteCustomerDetail(It.IsAny<int>())).Returns(msg);
            return mockservice;
        }

       


        private Customer TestData = new Customer()
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

        [Fact]
        public void GetAllCustomerOk()
        {
            
            List<Customer> Lisobj = new List<Customer>();

            Lisobj.Add(TestData);
           
            var controller = new CustomerController(GetallMock(Lisobj).Object);

            var okresult = controller.GetAll();

            Assert.IsType<OkObjectResult>(okresult);
        }


        [Fact]
        public void GetAllCustomerNotOk()
        {
            Messages obj = new Messages();
            obj.Success = false;
            obj.Message = "Customer List Is Empty";
            List<Customer> ObjList = null;
            
            var controller = new CustomerController(GetallMock(ObjList).Object);

            var NotFoundResult =controller.GetAll();
            var output = NotFoundResult as NotFoundObjectResult;
            Assert.Equal("Customer List Is Empty", output.Value);
        }

        [Fact]

        public void GetCustomerIdFound()
        {

            
            List<Customer> Lisobj = new List<Customer>();

            var controller = new CustomerController(GetByIdMock(TestData).Object);
            var okresult = controller.GetCustomerDetailById(5);

            Assert.IsType<OkObjectResult>(okresult);


        }

        [Fact]

        public void GetCustomerIdNotFound()
        {
            Messages obj = new Messages();
            obj.Success = false;
            obj.Message = "Customer Id Is Not Found";
            Customer cus = null;
            
            var controller = new CustomerController(GetByIdMock(cus).Object);

            var NotFoundResult = controller.GetCustomerDetailById(5);
            var output = NotFoundResult as NotFoundObjectResult;
            Assert.Equal("Customer Id Is Not Found", output.Value);
        }

        [Fact]
        public void AddCustomerOK()
        {
            Messages obj = new Messages();
            obj.Success = true;
            obj.Message = "This  Customer Added Succesfully";
            
            List<Customer> Obj = new List<Customer>();
            Obj.Add(TestData);
            
            var controller = new CustomerController(InsertMock(obj).Object);

            var okinsert = controller.InsertCustomerDetail(TestData);

            Assert.NotNull(okinsert);
        }

        [Fact]
        public void AddCustomerPhoneNumberConflict()
        {
            Messages obj = new Messages();
            obj.Success = false;
            obj.Message = "The Phone  Number Already Taked";
            var mockservice = new Mock<ICustomer>();
            mockservice.Setup(x=>x.GetCustomerDetailByNumber(It.IsAny<string>())).Returns(TestData);
            mockservice.Setup(x => x.GetCustomerDetailByEmail(It.IsAny<string>())).Returns(TestData);
            mockservice.Setup(x => x.InsertCustomerDetail(It.IsAny<Customer>())).Returns(obj);
            var controller = new CustomerController(mockservice.Object);

            var okinsert = controller.InsertCustomerDetail(TestData);
            var output = okinsert as ConflictObjectResult;
            Assert.IsType<ConflictObjectResult>(output);   
         }


        [Fact]
        public void AddCustomerPhoneEmailConflict()
        {
            Messages obj = new Messages();
            obj.Success = false;
            obj.Message = "The Email  Id Already Taked";
            var mockservice = new Mock<ICustomer>();
            mockservice.Setup(x => x.GetCustomerDetailByNumber(It.IsAny<string>())).Returns(TestData);
            mockservice.Setup(x => x.GetCustomerDetailByEmail(It.IsAny<string>())).Returns(TestData);
            mockservice.Setup(x => x.InsertCustomerDetail(It.IsAny<Customer>())).Returns(obj);
            var controller = new CustomerController(mockservice.Object);

            var okinsert = controller.InsertCustomerDetail(TestData);
            var output = okinsert as ConflictObjectResult;
            Assert.IsType<ConflictObjectResult>(output);
        }

        [Fact]
        public void UpdateCustomerOk()
        {
            Messages obj = new Messages();
            obj.Success = true;
            obj.Message = "Customer Updated Succesfully!!";
            Customer Cus1 = new Customer
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
            Customer Cus2 = new Customer
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
            List<Customer> Obj = new List<Customer>();
            Obj.Add(Cus1);
            var mockservice = new Mock<ICustomer>();
            mockservice.Setup(x => x.UpdateCustomerDetail(It.IsAny<Customer>())).Returns(obj);
            var controller = new CustomerController(mockservice.Object);

            var Okupdate = controller.UpdateCustomerDetail(Cus2);

            Assert.Equal(Cus1.CustomerId, Cus2.CustomerId);
        }


        [Fact]
        public void Update_BadRequest()
        {
            Customer customer = new Customer { CustomerId = 0 };
            Messages messages = new Messages();
            messages.Success = false;
            messages.Message = "This customer id not registered";
           
           
           
            var controller = new CustomerController(UpdateMock(messages).Object);

            var output = controller.UpdateCustomerDetail(customer);
            var ok = output as BadRequestObjectResult;
            Assert.IsType<BadRequestObjectResult>(output);
            //Assert.Equal("This customer id not registered", ok.Message);
            //Assert.False(ok.Success);

        }




        [Fact]
        public void UpdateNotFound()
        {
            Messages messages = new Messages();
            messages.Success = false;
            messages.Message = "This customer id not registered";
           
            var controller = new CustomerController(UpdateMock(messages).Object);

            var output = controller.UpdateCustomerDetail(TestData);
            var result = output  as NotFoundObjectResult;
            Assert.IsType<NotFoundObjectResult>(output);
        

        }


        [Fact]
        public void Update_PhoneNumber_Conflict()
        {

            Messages messages = new Messages();
            messages.Success = false;
            messages.Message = "The Contact Number Already Taked";
            var mockservice = new Mock<ICustomer>();
            mockservice.Setup(x => x.GetCustomerDetailById(It.IsAny<int>())).Returns(TestData);
            mockservice.Setup(x => x.UpdateCustomerDetail(It.IsAny<Customer>())).Returns(messages);

            var controller = new CustomerController(mockservice.Object);

            var output= controller.UpdateCustomerDetail(TestData);

            var result=output as ConflictResult;
       //     Assert.StrictEqual(409, result.StatusCode);
            Assert.IsType<ConflictObjectResult>(output);


        }

        [Fact]
        public void Update_Email_Conflict()
        {

            Messages messages = new Messages();
            messages.Success = false;
            messages.Message = "The Email Already Taked";
            var mockservice = new Mock<ICustomer>();
            mockservice.Setup(x => x.GetCustomerDetailById(It.IsAny<int>())).Returns(TestData);
            mockservice.Setup(x => x.UpdateCustomerDetail(It.IsAny<Customer>())).Returns(messages);

            var controller = new CustomerController(mockservice.Object);

            var output = controller.UpdateCustomerDetail(TestData);

            var result = output as ConflictResult;
       //     Assert.StrictEqual(409, result.StatusCode);
            Assert.IsType<ConflictObjectResult>(output);


        }



        [Fact]

        public void deleteok()
        {
           
            Messages obj = new Messages();
            obj.Success = true;
            obj.Message = "Customer Deleted Succesfully";

          
            List<Customer> Obj = new List<Customer>();
            Obj.Add(TestData);

            var mockservice = new Mock<ICustomer>();
            mockservice.Setup(x => x.GetCustomerDetailById(It.IsAny<int>())).Returns(TestData);
            mockservice.Setup(x => x.DeleteCustomerDetail(It.IsAny<int>())).Returns(obj);
            var controller = new CustomerController(mockservice.Object);

            var okdelete = controller.DeleteCustomerDetail(1);

            Assert.IsType<OkObjectResult>(okdelete);   
        }

        [Fact]
        public void deletenotfound()
        {
            Messages obj = new Messages();
            obj.Success = false;
            obj.Message = "This Customer Id Not Registered";
            Customer Cus = null;
            List<Customer> Obj = new List<Customer>();

            var mockservice = new Mock<ICustomer>();
            mockservice.Setup(x => x.GetCustomerDetailById(It.IsAny<int>())).Returns(Cus);

            var controller = new CustomerController(mockservice.Object);

            var NotFoundResult = controller.DeleteCustomerDetail(5);
            var output = NotFoundResult as NotFoundObjectResult;
            Assert.IsType<NotFoundObjectResult>(output);
            

          
        }
    }
}
