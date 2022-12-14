//using Food_Delivery.Controllers;
//using Food_Delivery.Models;
//using Food_Delivery.RepositoryInterface;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Food_Delivery
//{
//    public class DeliveryPersonTest
//    {
//        private Mock<IDeliveryPerson> Mock()
//        {
//            var mockservice = new Mock<IDeliveryPerson>();
//            return mockservice;
//        }

//        private Mock<IDeliveryPerson> getallMock(List<DeliveryPerson> listObj)
//        {
//            var mockService= new Mock<IDeliveryPerson>();
//            mockService.Setup(x=>x.GetAllDeliveryPersons()).Returns(listObj);
//            return mockService;
//        }

//        public Mock<IDeliveryPerson> getByIdMock(DeliveryPerson obj)
//        {
//            var mockService=new Mock<IDeliveryPerson>();
//            mockService.Setup(x => x.GetDeliveryPerson(It.IsAny<int>())).Returns(obj);
//            return mockService;
//        }

//        public Mock<IDeliveryPerson>InsertMock(Messages obj)
//        {
//            var mockservice=new Mock<IDeliveryPerson>();
//            mockservice.Setup(x => x.InsertDeliveryPerson(It.IsAny<DeliveryPerson>())).Returns(obj);
//            return mockservice;
//        }

//        private DeliveryPerson testData = new DeliveryPerson()
//        {
//            DeliveryPersonId=1,
//            DeliveryPersonName="Mohan",
//            ContactNumber="8547124857",
//            Gender="Male",
//            CreatedOn=DateTime.Now,
//            IsActive=true

//        };

//        [Fact]
//        public void GetAll()
//        {
//            List<DeliveryPerson> list = new List<DeliveryPerson>();
//            list.Add(testData);
//            var controller = new DeliveryPersonController(getallMock(list).Object);
//            var output=controller.GetAllDeliveryPersons();
//            Assert.IsType<OkObjectResult>(output);
//            Assert.Equal(1, list.Count);
//            Assert.NotNull(output);

//        }

//        [Fact]
//        public void GetAllNotOk()
//        {
//            Messages obj = new Messages();
//            obj.Success = false;
//            obj.Message = "Delivery Person Is Not Found";
//            List<DeliveryPerson> list = null;
//            var controller = new DeliveryPersonController(getallMock(list).Object);
//            var notFoundObjectResult= controller.GetAllDeliveryPersons();
//            var output = notFoundObjectResult as NotFoundObjectResult;
//            Assert.Equal("Delivery Person Is Not Found", output.Value);
//          //  Assert.Null(output);
        

//        }

//        [Fact]
//        public void GetById()
//        {
//            var controller = new DeliveryPersonController(getByIdMock(testData).Object);
//            var output = controller.GetDeliveryPerson(5);
//            var id=output as OkObjectResult;
//            Assert.IsAssignableFrom<OkObjectResult>(output);
//            Assert.IsType<OkObjectResult>(id);
//            Assert.NotNull(output);
//        }

//        [Fact]
//        public void GetByIdNotOk()
//        {
//            DeliveryPerson person = null;
//            var controller = new DeliveryPersonController(getByIdMock(person).Object);
//            var result = controller.GetDeliveryPerson(5);
//            var output = result as NotFoundObjectResult;
//            Assert.IsType<NotFoundObjectResult>(output);
//            Assert.Equal("This Delivery Person Id Is Not Found", output.Value);
//        }


//        [Fact]
//        public void InsertOk()
//        {
//            Messages obj = new Messages();
//            obj.Success = false;
//            obj.Message = "DeliveryPerson Added Succesfully";
//            var controller = new DeliveryPersonController(InsertMock(obj).Object);
//            var okinsert = controller.InsertDeliveryPerson(testData);
//            var output=okinsert as OkObjectResult;
//            Assert.IsType<OkObjectResult>(output);
//        }


//        [Fact]
//        public void InsertContactNumberConflict()
//        {
//            Messages obj = new Messages();
//            obj.Success = false;
//            obj.Message = "This Contact  Number  Already Taked";
//            var mockservice = new Mock<IDeliveryPerson>();
//            mockservice.Setup(x=>x.GetDeliveryPersonByNumber(It.IsAny<string>())).Returns(testData);
//            mockservice.Setup(x => x.InsertDeliveryPerson(It.IsAny<DeliveryPerson>())).Returns(obj);
//            var controller = new DeliveryPersonController(mockservice.Object);
//            var okinsert= controller.InsertDeliveryPerson(testData);
//            var output = okinsert as ConflictObjectResult;
//            Assert.IsType<ConflictObjectResult>(output);
//        }

//        [Fact]
//        public void UpdateOk()
//        {
//            Messages obj = new Messages();
//            obj.Success = true;
//            obj.Message = "DeliveryP!!";
//            var mockservice = new Mock<IDeliveryPerson>();
//            mockservice.Setup(x => x.GetDeliveryPerson(It.IsAny<int>())).Returns(testData);
//            mockservice.Setup(x => x.UpdateDeliveryPerson(It.IsAny<DeliveryPerson>())).Returns(obj);
//            var controller = new DeliveryPersonController(mockservice.Object);
//            var okinsert = controller.UpdateDeliveryPerson(testData);
//            var output= okinsert as OkObjectResult;
//            Assert.IsType<OkObjectResult>(output);
//            Assert.Equal(obj, output.Value);

//        }

//        [Fact]
//        public void UpdatePhoneNumberConflictOk()
//        {
//            Messages obj = new Messages();
//            obj.Success = false;
//            obj.Message = "This Contact  Number  Already Taked";
//            var mockservice = new Mock<IDeliveryPerson>();
//            mockservice.Setup(x => x.GetDeliveryPerson(It.IsAny<int>())).Returns(testData);
//            mockservice.Setup(x => x.UpdateDeliveryPerson(It.IsAny<DeliveryPerson>())).Returns(obj);
//            var controller = new DeliveryPersonController(mockservice.Object);
//            var okinsert = controller.UpdateDeliveryPerson(testData);
//            var output = okinsert as OkObjectResult;
//            Assert.IsType<OkObjectResult>(output);

//        }

//        [Fact]
//        public void DeleteOk()
//        {
//            Messages obj = new Messages();
//            obj.Success = true;
//            obj.Message = "DeliveryPerson Deleted Succesfully";
//            var mockservice = new Mock<IDeliveryPerson>();
//            mockservice.Setup(x => x.GetDeliveryPerson(It.IsAny<int>())).Returns(testData);
//            mockservice.Setup(x => x.DeleteDeliveryPerson(It.IsAny<int>())).Returns(obj);
//            var controller = new DeliveryPersonController(mockservice.Object);
//            var okinsert = controller.DeleteDeliveryPerson(5);
//            var output = okinsert as OkObjectResult;
//            Assert.IsType<OkObjectResult>(output);
//            Assert.Equal(obj, output.Value);
//        }

//        [Fact]
//        public void DeleteNotOk()
//        {
//            Messages obj = new Messages();
//            obj.Success = true;
//            obj.Message = "This Delivery Person Id Is Not Found";
//            DeliveryPerson person = null;
//            var mockservice = new Mock<IDeliveryPerson>();
//            mockservice.Setup(x => x.GetDeliveryPerson(It.IsAny<int>())).Returns(person);
//            mockservice.Setup(x => x.DeleteDeliveryPerson(It.IsAny<int>())).Returns(obj);
//            var controller = new DeliveryPersonController(mockservice.Object);
//            var okinsert = controller.DeleteDeliveryPerson(5);
//            var output = okinsert as NotFoundObjectResult;
//            Assert.IsType<NotFoundObjectResult>(output);
       
//        }

//    }
//}
