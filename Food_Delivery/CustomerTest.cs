using Azure;
using Food_Delivery.Controllers;
using Food_Delivery.Models;
using Food_Delivery.RepositoryInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Moq;
using static Food_Delivery.Models.Messages;

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
            var output = okresult as OkObjectResult;
            Assert.IsType<OkObjectResult>(okresult);
            Assert.StrictEqual(200, output.StatusCode);
            Assert.NotNull(okresult);
        }


        [Fact]
        public void GetAllCustomerNotOk()
        {
            Messages obj = new Messages();
            obj.Success = false;
            obj.Status = Statuses.NotFound;
            obj.Message = "Customer list is empty";
            List<Customer> ObjList = null;
            var controller = new CustomerController(GetallMock(ObjList).Object);
            var result =controller.GetAll();
            var output = result as NotFoundObjectResult;
            Assert.Equal(obj.Message, output.Value);
            Assert.StrictEqual(404, output.StatusCode);
            Assert.IsType<NotFoundObjectResult>(output);
            Assert.NotNull(result);
        }

        [Fact]

        public void GetCustomerIdFound()
        {
            Messages obj = new Messages();
            obj.Success = true;
            obj.Status=Statuses.Success;
            List<Customer> Lisobj = new List<Customer>();
            var controller = new CustomerController(GetByIdMock(TestData).Object);
            var okresult = controller.GetCustomerDetailById(5);
            var output = okresult as OkObjectResult;
            Assert.IsType<OkObjectResult>(output);
            Assert.NotNull(Lisobj);
            Assert.StrictEqual(200, output.StatusCode);
          
            
        }

        [Fact]

        public void GetCustomerIdNotFound()
        {
            Messages obj = new Messages();
            obj.Success = false;
            obj.Message = "Customer id is not found";
            obj.Status=Statuses.NotFound;
            Customer cus = null;
            var controller = new CustomerController(GetByIdMock(cus).Object);
            var NotFoundResult = controller.GetCustomerDetailById(5);
            var output = NotFoundResult as NotFoundObjectResult;
            Assert.Equal("Customer id is not found", output.Value);
            Assert.IsType<NotFoundObjectResult>(output);
            Assert.Null(cus);
            Assert.StrictEqual(404, output.StatusCode);
        }

        [Fact]
        public void GetCustomerNumberFound()
        {
            Messages obj = new Messages();
            obj.Success = true;
            obj.Status = Statuses.Success;
            List<Customer> Lisobj = new List<Customer>();
            Lisobj.Add(TestData);
            var controller = new CustomerController(GetByNumberMock(TestData).Object);
            var okresult = controller.GetCustomerDetailByNumber("9791225793");
            var output = okresult as OkObjectResult;
            Assert.IsType<OkObjectResult>(output);
            Assert.NotNull(Lisobj);
            Assert.StrictEqual(200, output.StatusCode);


        }
        [Fact]

        public void GetCustomerNumberNotFound()
        {
            Messages obj = new Messages();
            obj.Success = false;
            obj.Message = "Customer id is not found";
            obj.Status = Statuses.NotFound;
            List<Customer> Lisobj = null;
            var controller = new CustomerController(GetByNumberMock(TestData).Object);
            var okresult = controller.GetCustomerDetailByNumber("9874563214");
            var output = okresult as NotFoundObjectResult;
            Assert.Equal(obj.Message, output.Value);
            Assert.IsType<NotFoundObjectResult>(output);
            Assert.StrictEqual(404, output.StatusCode);
        }

        [Fact]
        public void AddCustomerOK()
        {
            Messages messages = new Messages();
            messages.Success = true;
            messages.Message = "This  Customer Added Succesfully";
            messages.Status = Statuses.Created;
            List<Customer> list = new List<Customer>();
            list.Add(TestData);
            var controller = new CustomerController(InsertMock(messages).Object);
            var okinsert = controller.InsertCustomerDetail(TestData);
            var output = okinsert as CreatedResult;
            Assert.Equal("This  Customer Added Succesfully", output.Value);
            Assert.IsType<CreatedResult>(output);
            Assert.StrictEqual(201, output.StatusCode);
        }

        [Fact]
        public void AddCustomerPhoneNumberConflict()
        {
            Messages obj = new Messages();
            obj.Success = false;
            obj.Message = "The Phone  Number Already Taked";
            obj.Status = Statuses.Conflict;
            var mockservice = new Mock<ICustomer>();
            mockservice.Setup(x => x.InsertCustomerDetail(It.IsAny<Customer>())).Returns(obj);
            var controller = new CustomerController(mockservice.Object);
            var okinsert = controller.InsertCustomerDetail(TestData);
            var output = okinsert as ConflictObjectResult;
            Assert.StrictEqual(obj.Message, output.Value);
            Assert.Equal(409, output.StatusCode);
            Assert.IsType<ConflictObjectResult>(output);   
         }


        [Fact]
        public void AddCustomerEmailConflict()
        {
            Messages obj = new Messages();
            obj.Success = false;
            obj.Message = "The Email  Id Already Taked";
            obj.Status = Statuses.Conflict;
            var mockservice = new Mock<ICustomer>();
            mockservice.Setup(x => x.InsertCustomerDetail(It.IsAny<Customer>())).Returns(obj);
            var controller = new CustomerController(mockservice.Object);
            var okinsert = controller.InsertCustomerDetail(TestData);
            var output = okinsert as ConflictObjectResult;
            Assert.IsType<ConflictObjectResult>(output);
            Assert.StrictEqual(409, output.StatusCode);
            Assert.Equal(obj.Message, output.Value);
        }

        [Fact]
        public void UpdateCustomerOk()
        {
            Messages obj = new Messages();
            obj.Success = true;
            obj.Message = "Customer Updated Succesfully!!";
            obj.Status = Statuses.Success;
            List<Customer> Obj = new List<Customer>();
            Obj.Add(TestData);
            var mockservice = new Mock<ICustomer>();
            mockservice.Setup(x => x.UpdateCustomerDetail(It.IsAny<Customer>())).Returns(obj);
            var controller = new CustomerController(mockservice.Object);
            var Okupdate = controller.UpdateCustomerDetail(TestData);
            var output= Okupdate as OkObjectResult;
            Assert.IsType<OkObjectResult>(output);
            Assert.Equal(200, output.StatusCode);
            Assert.StrictEqual(TestData.CustomerId, TestData.CustomerId);
        }


        [Fact]
        public void Update_BadRequest()
        {
            Customer customer = new Customer { CustomerId = 0 };
            Messages messages = new Messages();
            messages.Success = false;
            messages.Status = Statuses.BadRequest;
            messages.Message = "This customer id not registered";
            var controller = new CustomerController(UpdateMock(messages).Object);
            var output = controller.UpdateCustomerDetail(customer);
            var ok = output as BadRequestObjectResult;
            Assert.IsType<BadRequestObjectResult>(output);
            Assert.Equal(400, ok.StatusCode);
        }

        [Fact]
        public void UpdateNotFound()
        {
            Messages messages = new Messages();
            messages.Success = false;
            messages.Status = Statuses.NotFound;
            messages.Message = "This customer id not registered";
            var controller = new CustomerController(UpdateMock(messages).Object);
            var output = controller.UpdateCustomerDetail(TestData);
            var result = output  as NotFoundObjectResult;
            Assert.IsType<NotFoundObjectResult>(output);
            Assert.StrictEqual(404, result.StatusCode);
            Assert.Equal(messages.Message, result.Value);
        }

        [Fact]
        public void Update_PhoneNumber_Conflict()
        {
            Messages messages = new Messages();
            messages.Success = false;
            messages.Message = "The Contact Number Already Taked";
            messages.Status = Statuses.Conflict;
            var mockservice = new Mock<ICustomer>();
            mockservice.Setup(x => x.UpdateCustomerDetail(It.IsAny<Customer>())).Returns(messages);
            var controller = new CustomerController(mockservice.Object);
            var output= controller.UpdateCustomerDetail(TestData);
            var result=output as ConflictObjectResult;
            Assert.StrictEqual(409, result.StatusCode);
            Assert.IsType<ConflictObjectResult>(output);
        }

        [Fact]
        public void Update_Email_Conflict()
        {
            Messages messages = new Messages();
            messages.Success = false;
            messages.Status = Statuses.Conflict;
            messages.Message = "The Email Already Taked";
            var mockservice = new Mock<ICustomer>();
            mockservice.Setup(x => x.UpdateCustomerDetail(It.IsAny<Customer>())).Returns(messages);
            var controller = new CustomerController(mockservice.Object);
            var output = controller.UpdateCustomerDetail(TestData);
            var result = output as ConflictObjectResult;
            Assert.StrictEqual(409, result.StatusCode);
            Assert.IsType<ConflictObjectResult>(output);
            Assert.Equal(messages.Message, result.Value);

        }

        [Fact]
        public void deleteok()
        {
            Messages messages = new Messages();
            messages.Success = true;
            messages.Message = "Customer Deleted Succesfully";
            messages.Status = Statuses.Success;
            List<Customer> Obj = new List<Customer>();
            Obj.Add(TestData);
            var mockservice = new Mock<ICustomer>();
            mockservice.Setup(x => x.DeleteCustomerDetail(It.IsAny<int>())).Returns(messages);
            var controller = new CustomerController(mockservice.Object);
            var okdelete = controller.DeleteCustomerDetail(1);
            var output= okdelete as OkObjectResult;
            Assert.IsType<OkObjectResult>(output);
            Assert.StrictEqual(200, output.StatusCode);
            Assert.Equal(messages.Message, output.Value);
        }

        [Fact]
        public void deletenotfound()
        {
            Messages obj = new Messages();
            obj.Success = false;
            obj.Message = "This Customer Id Not Registered";
            obj.Status = Statuses.NotFound;
            Customer Cus = null;
            List<Customer> Obj = new List<Customer>();
            var mockservice = new Mock<ICustomer>();
            mockservice.Setup(x => x.DeleteCustomerDetail(It.IsAny<int>())).Returns(obj);
            var controller = new CustomerController(mockservice.Object);
            var NotFoundResult = controller.DeleteCustomerDetail(5);
            var output = NotFoundResult as NotFoundObjectResult;
            Assert.IsType<NotFoundObjectResult>(output);
            Assert.StrictEqual(404, output.StatusCode);
            Assert.Equal(obj.Message, output.Value);
        }
    }
}
