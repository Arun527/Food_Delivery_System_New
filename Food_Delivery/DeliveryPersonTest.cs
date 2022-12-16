using Food_Delivery.Controllers;
using Food_Delivery.Models;
using Food_Delivery.RepositoryInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Food_Delivery.Models.Messages;

//namespace Food_Delivery
//{
//    public class DeliveryPersonTest
//    {
//        private Mock<IDeliveryPerson> Mock()
//        {
//            var mockservice = new Mock<IDeliveryPerson>();
//            return mockservice;
//        }

        private Mock<IDeliveryPerson> getallMock(List<DeliveryPerson> listObj)
        {
            var mockService = new Mock<IDeliveryPerson>();
            mockService.Setup(x => x.GetAllDeliveryPersons()).Returns(listObj);
            return mockService;
        }

        public Mock<IDeliveryPerson> getByIdMock(DeliveryPerson obj)
        {
            var mockService = new Mock<IDeliveryPerson>();
            mockService.Setup(x => x.GetDeliveryPerson(It.IsAny<int>())).Returns(obj);
            return mockService;
        }

        public Mock<IDeliveryPerson> InsertMock(Messages obj)
        {
            var mockservice = new Mock<IDeliveryPerson>();
            mockservice.Setup(x => x.InsertDeliveryPerson(It.IsAny<DeliveryPerson>())).Returns(obj);
            return mockservice;
        }

        private DeliveryPerson testData = new DeliveryPerson()
        {
            DeliveryPersonId = 1,
            DeliveryPersonName = "Mohan",
            ContactNumber = "8547124857",
            Gender = "Male",
            CreatedOn = DateTime.Now,
            IsActive = true

//        };

        [Fact]
        public void GetAll()
        {
            List<DeliveryPerson> list = new List<DeliveryPerson>();
            list.Add(testData);
            var controller = new DeliveryPersonController(getallMock(list).Object);
            var output = controller.GetAllDeliveryPersons();
            Assert.IsType<OkObjectResult>(output);
            Assert.Equal(1, list.Count);
            Assert.NotNull(output);

//        }

        [Fact]
        public void GetAllNotOk()
        {
            Messages messages = new Messages();
            messages.Success = false;
            messages.Message = "Delivery person is not found";
            messages.Status=Statuses.NotFound;
            List<DeliveryPerson> list = null;
            var controller = new DeliveryPersonController(getallMock(list).Object);
            var notFoundObjectResult = controller.GetAllDeliveryPersons();
            var output = notFoundObjectResult as NotFoundObjectResult;
            Assert.Equal(messages.Message, output.Value);
            Assert.IsType<NotFoundObjectResult>(output);
            Assert.StrictEqual(404,output.StatusCode);

        }

        [Fact]
        public void GetById()
        {
            var controller = new DeliveryPersonController(getByIdMock(testData).Object);
            var output = controller.GetDeliveryPerson(5);
            var id = output as OkObjectResult;
            Assert.IsAssignableFrom<OkObjectResult>(output);
            Assert.IsType<OkObjectResult>(id);
            Assert.NotNull(output);
        }

        [Fact]
        public void GetByIdNotOk()
        {
            Messages messages = new Messages();
            messages.Success = false;
            messages.Message = "This delivery person id is not found";
            DeliveryPerson person = null;
            var controller = new DeliveryPersonController(getByIdMock(person).Object);
            var result = controller.GetDeliveryPerson(5);
            var output = result as NotFoundObjectResult;
            Assert.IsType<NotFoundObjectResult>(output);
            Assert.Equal(messages.Message, output.Value);
            Assert.StrictEqual(404, output.StatusCode);
        }


        [Fact]
        public void InsertOk()
        {
            Messages messages = new Messages();
            messages.Success = false;
            messages.Message = "DeliveryPerson Added Succesfully";
            messages.Status = Statuses.Created;
            var controller = new DeliveryPersonController(InsertMock(messages).Object);
            var okinsert = controller.InsertDeliveryPerson(testData);
            var output = okinsert as CreatedResult;
            Assert.IsType<CreatedResult>(output);
            Assert.Equal(messages.Message, output.Value);
            Assert.StrictEqual(201, output.StatusCode);
        }


        [Fact]
        public void InsertContactNumberConflict()
        {
            Messages messages = new Messages();
            messages.Success = false;
            messages.Message = "This Contact  Number  Already Taked";
            messages.Status = Statuses.Conflict;
            var mockservice = new Mock<IDeliveryPerson>();
    
            mockservice.Setup(x => x.InsertDeliveryPerson(It.IsAny<DeliveryPerson>())).Returns(messages);
            var controller = new DeliveryPersonController(mockservice.Object);
            var okinsert = controller.InsertDeliveryPerson(testData);
            var output = okinsert as ConflictObjectResult;
            Assert.IsType<ConflictObjectResult>(output);
            Assert.StrictEqual(409, output.StatusCode);
            Assert.Equal(messages.Message, output.Value);
        }

        [Fact]
        public void UpdateOk()
        {
            Messages messages = new Messages();
            messages.Success = true;
            messages.Message = "Delivery person updated succesfully!!";
            messages.Status=Statuses.Success;
            var mockservice = new Mock<IDeliveryPerson>();
            mockservice.Setup(x => x.UpdateDeliveryPerson(It.IsAny<DeliveryPerson>())).Returns(messages);
            var controller = new DeliveryPersonController(mockservice.Object);
            var okinsert = controller.UpdateDeliveryPerson(testData);
            var output = okinsert as OkObjectResult;
            Assert.IsType<OkObjectResult>(output);
            Assert.StrictEqual(200, output.StatusCode);
            Assert.Equal(messages.Message, output.Value);

//        }

        [Fact]
        public void UpdatePhoneNumberConflictOk()
        {
            Messages messages = new Messages();
            messages.Success = false;
            messages.Message = "This Contact  Number  Already Taked";
            messages.Status =Statuses.Conflict;
            var mockservice = new Mock<IDeliveryPerson>();
            mockservice.Setup(x => x.UpdateDeliveryPerson(It.IsAny<DeliveryPerson>())).Returns(messages);
            var controller = new DeliveryPersonController(mockservice.Object);
            var okinsert = controller.UpdateDeliveryPerson(testData);
            var output = okinsert as ConflictObjectResult;
            Assert.IsType<ConflictObjectResult>(output);
            Assert.StrictEqual(409, output.StatusCode);
            Assert.Equal(messages.Message, output.Value);


//        }

        [Fact]
        public void DeleteOk()
        {
            Messages messages = new Messages();
            messages.Success = true;
            messages.Message = "DeliveryPerson Deleted Succesfully";
            messages.Status = Statuses.Success;
            var mockservice = new Mock<IDeliveryPerson>();
            mockservice.Setup(x => x.DeleteDeliveryPerson(It.IsAny<int>())).Returns(messages);
            var controller = new DeliveryPersonController(mockservice.Object);
            var okinsert = controller.DeleteDeliveryPerson(5);
            var output = okinsert as OkObjectResult;
            Assert.IsType<OkObjectResult>(output);
            Assert.StrictEqual(200, output.StatusCode);
            Assert.Equal(messages.Message, output.Value);
        }

        [Fact]
        public void DeleteNotOk()
        {
            Messages messages = new Messages();
            messages.Success = true;
            messages.Message = "This Delivery Person Id Is Not Found";
            messages.Status = Messages.Statuses.NotFound;
            DeliveryPerson person = null;
            var mockservice = new Mock<IDeliveryPerson>();
            mockservice.Setup(x => x.DeleteDeliveryPerson(It.IsAny<int>())).Returns(messages);
            var controller = new DeliveryPersonController(mockservice.Object);
            var okinsert = controller.DeleteDeliveryPerson(5);
            var output = okinsert as NotFoundObjectResult;
            Assert.IsType<NotFoundObjectResult>(output);
            Assert.StrictEqual(404, output.StatusCode);
            Assert.Equal(messages.Message, output.Value);
        }

//    }
//}
