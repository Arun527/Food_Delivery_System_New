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

namespace Food_Delivery
{
    public class HotelTest
    {
        private Hotel TestData => new()
        {
            HotelId = 1,
            HotelName = "saravanabhavan",
            Email = "Saravanabhavan123@gmail.com",
            ImageId = "d2f5",
            Type = "Veg",
            Address = "Madurai",
            ContactNumber = "9874563214"
        };
        private Mock<IHotel> Mock()
        {
            var mockservice = new Mock<IHotel>();
            return mockservice;
        }

        private Mock<IHotel> GetAllMock(List<Hotel> hotels)
        {
            var mockservice = Mock();
            mockservice.Setup(x => x.GetAll()).Returns(hotels);
            return mockservice;
        }

        private Mock<IHotel> GetById(Hotel hotels)
        {
            var mockservice = Mock();
            mockservice.Setup(x => x.GetHotelById(It.IsAny<int>())).Returns(hotels);
            return mockservice;
        }
        private Mock<IHotel> GetByNumberMock(Hotel hotels)
        {
            var mockservice = Mock();
            mockservice.Setup(x => x.GetHotelDetailByNumber(It.IsAny<string>())).Returns(hotels);
            return mockservice;
        }

        private Mock<IHotel> GetByEmailMock(Hotel hotels)
        {
            var mockservice = Mock();
            mockservice.Setup(x => x.GetHotelDetailByEmail(It.IsAny<string>())).Returns(hotels);
            return mockservice;
        }

        private Mock<IHotel> AddHotelMock(Messages message)
        {
            var mockservice = Mock();
            mockservice.Setup(x => x.InsertHotelDetail(It.IsAny<Hotel>())).Returns(message);
            return mockservice;
        }

        private Mock<IHotel> UpdateHotelMock(Messages message)
        {
            var mockservice = Mock();
            mockservice.Setup(x => x.UpdateHotelDetail(It.IsAny<Hotel>())).Returns(message);
            return mockservice;
        }

        private Mock<IHotel> DeleteHotelMock(Messages message)
        {
            var mockservice = Mock();
            mockservice.Setup(x => x.DeleteHotelDetail(It.IsAny<int>())).Returns(message);
            return mockservice;
        }

        private Mock<IHotel> GetHotelDetailByNameMock(List<Hotel> hotels)
        {
            var mockservice = Mock();
            mockservice.Setup(x => x.GetHotelDetailByName(It.IsAny<string>())).Returns(hotels);
            return mockservice;
        }


        private Mock<IHotel> GetHotelType(List<Hotel> hotels)
        {
            var mockservice = Mock();
            mockservice.Setup(x => x.GetHotelType(It.IsAny<string>())).Returns(hotels);
            return mockservice;
        }

        private Mock<IHotel> UpdateHotelDetailMock(Hotel id)
        {
            var mockservice = Mock();
            mockservice.Setup(x => x.GetHotelById(It.IsAny<int>())).Returns(id);
            return mockservice;

        }

        [Fact]
        public void HotelGetAll_OkResult()
        {
            Hotel obj = (new Hotel
            {
                HotelId = 1,
                HotelName = "saravanabhavan",
                Email = "Saravanabhavan123@gmail.com",
                ImageId = "d2f5",
                Type = "Veg",
                Address = "Madurai",
                ContactNumber = "9874563214"
            });
            List<Hotel> Lisobj = new List<Hotel>();
            Lisobj.Add(obj);
            var mockservice = new Mock<IHotel>();
            mockservice.Setup(x => x.GetAll()).Returns(Lisobj);
            var controller = new HotelController(mockservice.Object);

            var okresult = controller.GetHotelList();

            Assert.IsType<OkObjectResult>(okresult);
        }

        [Fact]
        public void HotelGetAll_NotFoundResult()
        {
            Messages obj = new Messages();
            obj.Success = false;
            obj.Message = "Hotel List Is Not Found";
            List<Hotel> Lisobj = null;
            var mockservice = new Mock<IHotel>();
            mockservice.Setup(x => x.GetAll()).Returns(Lisobj);
            var controller = new HotelController(mockservice.Object);
            var NotFoundResult = controller.GetHotelList();
            var output = NotFoundResult as NotFoundObjectResult;
            Assert.Equal("Hotel List Is Not Found", output.Value);

        }



        [Fact]
        public void HotelGetById_OkResult()
        {
            var controller = new HotelController(GetById(TestData).Object);
            var okresult = controller.GetHotelById(2);
            var list = okresult as OkObjectResult;
            var result = list.Value as Hotel;
            Assert.IsType<OkObjectResult>(okresult);
            Assert.Equal(TestData.ContactNumber, result.ContactNumber);
            Assert.NotNull(result);
            Assert.StrictEqual(1, result.HotelId);
            Assert.True(result.IsActive);
            Assert.StrictEqual(200, list.StatusCode);
        }


        [Fact]
        public void HotelGetById_NotFoundResult()
        {
            Messages msg = new Messages();
            msg.Success = true;
            msg.Message = "The Hotel Id Is Not Found";
            Hotel obj1 = null;
            var mockservice = new Mock<IHotel>();
            var controller = new HotelController(mockservice.Object);
            var NotFoundResult = controller.GetHotelById(2);
            var output = NotFoundResult as NotFoundObjectResult;
            Assert.IsType<NotFoundObjectResult>(NotFoundResult);
            Assert.StrictEqual(msg.Message, output.Value);
            Assert.StrictEqual(404, output.StatusCode);
        }



        [Fact]
        public void AddHotel_OKResult()
        {
            Messages msg = new Messages();
            msg.Success = true;
            msg.Message = "Contact Number Already Taked";
            var controller = new HotelController(AddHotelMock(msg).Object);
            var output = controller.AddHotelDetail(TestData);
            var result = output as CreatedResult;
            Assert.IsType<CreatedResult>(output);
            Assert.StrictEqual(msg, result.Value);
            Assert.StrictEqual("https://localhost:7187/Api/Hotel/1", result.Location);
            Assert.StrictEqual(201, result.StatusCode);
        }


        [Fact]
        public void AddHotel_ContactNumberConflict()
        {
            Messages msg = new Messages();
            msg.Message = "Contact Number Already Taked";
            msg.Success = false;
            var mockservice = new Mock<IHotel>();
            mockservice.Setup(x => x.GetHotelDetailByNumber(It.IsAny<string>())).Returns(TestData);
            mockservice.Setup(x => x.UpdateHotelDetail(It.IsAny<Hotel>())).Returns(msg);
            var controller = new HotelController(mockservice.Object);
            var output = controller.AddHotelDetail(TestData);
            var result = output as ConflictObjectResult;
            Assert.Equal(msg.Message, result.Value);
            Assert.StrictEqual(409, result.StatusCode);
            Assert.IsType<ConflictObjectResult>(output);
        }

        public void AddHotel_EmailConflict()
        {
            Messages msg = new Messages();
            msg.Message = "Email Id Already Taked";
            msg.Success = false;
            var mockservice = new Mock<IHotel>();
            mockservice.Setup(x => x.GetHotelDetailByEmail(It.IsAny<string>())).Returns(TestData);
            mockservice.Setup(x => x.UpdateHotelDetail(It.IsAny<Hotel>())).Returns(msg);
            var controller = new HotelController(mockservice.Object);
            var output = controller.AddHotelDetail(TestData);
            var result = output as ConflictObjectResult;
            Assert.Equal(msg.Message, result.Value);
            Assert.StrictEqual(409, result.StatusCode);
            Assert.IsType<ConflictObjectResult>(output);
        }


        [Fact]
        public void Update_OKResult()
        {
            Messages obj = new Messages();
            obj.Success = true;
            obj.Message = "The hotel updated succesfully";
            Hotel obj1 = (new Hotel
            {
                HotelId = 1,
                HotelName = "saravanabhavan",
                Email = "Saravanabhavan123@gmail.com",
                ImageId = "d2f5",
                Type = "Veg",
                Address = "Madurai",
                ContactNumber = "9874563214"
            });
            List<Hotel> Obj = new List<Hotel>();
            Obj.Add(obj1);
            var mockservice = new Mock<IHotel>();
            mockservice.Setup(x => x.GetHotelById(It.IsAny<int>())).Returns(TestData);
            mockservice.Setup(x => x.UpdateHotelDetail(It.IsAny<Hotel>())).Returns(obj);
            var controller = new HotelController(mockservice.Object);
            var output = controller.UpdateHotelDetail(TestData);
            var result = output as OkObjectResult;
            Assert.IsType<OkObjectResult>(output);
            Assert.StrictEqual(obj, result.Value);
            Assert.StrictEqual(200, result.StatusCode);
        }

        [Fact]
        public void Update_NotFoundResult()
        {
            Messages msg = new Messages();
            msg.Success = true;
            msg.Message = "Hotel Id Not Found";
            var controller = new HotelController(UpdateHotelMock(msg).Object);
            var output = controller.UpdateHotelDetail(TestData);
            var result = output as NotFoundObjectResult;
            Assert.IsType<NotFoundObjectResult>(output);
            Assert.StrictEqual(msg.Message, result.Value);
            Assert.StrictEqual(404, result.StatusCode);
        }

        [Fact]
        public void Update_BadResquest()
        {
            Hotel hotel = new Hotel { HotelId = 0 };
            Messages msg = new Messages();
            msg.Success = false;
            msg.Message = "The Hotel Field Is Required";
            var controller = new HotelController(UpdateHotelMock(msg).Object);
            var output = controller.UpdateHotelDetail(hotel);
            var result = output as BadRequestObjectResult;
            Assert.IsType<BadRequestObjectResult>(output);
            Assert.StrictEqual("The Hotel Field Is Required", result.Value);
            Assert.StrictEqual(400, result.StatusCode);
            Assert.True(hotel.HotelId == 0);
        }


        [Fact]
        public void Update_EmailConfict()
        {
            Messages msg = new Messages();
            msg.Success = false;
            msg.Message = "This Email Id Already taked";
            var mockservice = new Mock<IHotel>();
            mockservice.Setup(x => x.GetHotelById(It.IsAny<int>())).Returns(TestData);
            mockservice.Setup(x => x.UpdateHotelDetail(It.IsAny<Hotel>())).Returns(msg);
            var controller = new HotelController(mockservice.Object);
            var output = controller.UpdateHotelDetail(TestData);
            var result = output as ConflictObjectResult;
            Assert.IsType<ConflictObjectResult>(output);
            Assert.Equal("The Email Already Taked", result.Value);
            Assert.StrictEqual(409, result.StatusCode);
        }


        [Fact]
        public void DeleteHotel_SuccessOK()
        {
            Messages msg = new Messages();
            msg.Success = false;
            msg.Message = "The hotel Id deleted Succesfully";
            var mockservice = new Mock<IHotel>();
            mockservice.Setup(x => x.GetHotelById(It.IsAny<int>())).Returns(TestData);
            mockservice.Setup(x => x.DeleteHotelDetail(It.IsAny<int>())).Returns(msg);
            var controller = new HotelController(mockservice.Object);
            var output = controller.DeleteHotelDetail(TestData.HotelId);
            var result = output as OkObjectResult;
            Assert.IsType<OkObjectResult>(output);
            Assert.Equal(msg, result.Value);
            Assert.StrictEqual(200, result.StatusCode);
        }

        [Fact]
        public void DeleteHotel_NotFound()
        {
            Messages msg = new Messages();
            msg.Success = true;
            msg.Message = "The Hotel Id Not Found";
            var controller = new HotelController(DeleteHotelMock(msg).Object);
            var output = controller.DeleteHotelDetail(3);
            var result = output as NotFoundObjectResult;
            Assert.IsType<NotFoundObjectResult>(output);
            Assert.Equal(msg.Message, result.Value);
            Assert.StrictEqual(404, result.StatusCode);
        }


        [Fact]
        public void DeleteHotel_BadRequest()
        {
            Messages msg = new Messages();
            msg.Success = false;
            msg.Message = "The hotel Food Is Available For Users";
            var mockservice = new Mock<IHotel>();
            mockservice.Setup(x => x.GetHotelById(It.IsAny<int>())).Returns(TestData);
            mockservice.Setup(x => x.DeleteHotelDetail(It.IsAny<int>())).Returns(msg);
            var controller = new HotelController(mockservice.Object);
            var output = controller.DeleteHotelDetail(TestData.HotelId);
            var result = output as BadRequestObjectResult;
            Assert.IsType<BadRequestObjectResult>(output);
            Assert.StrictEqual(400, result.StatusCode);
        }

    }
}