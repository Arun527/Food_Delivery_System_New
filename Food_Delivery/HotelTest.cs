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
using static Food_Delivery.Models.Messages;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Food_Delivery_System.Models;

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

        private FoodList foodList => new()
        {
            HotelId=1,
            FoodId=1,
            HotelName = "saravanabhavan",
            ImageId = "d2f5",
            FoodName="idly",
            Price=12,
            Quantity=1,
            Type="veg"

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
        private Mock<IHotel> GetByName(List<Hotel> hotels)
        {
            var mockservice = Mock();
            mockservice.Setup(x => x.GetHotelDetailByName(It.IsAny<string>())).Returns(hotels);
            return mockservice;
        }
        private Mock<IHotel> GetByType(List<Hotel> hotels)
        {
            var mockservice = Mock();
            mockservice.Setup(x => x.GetHotelType(It.IsAny<string>())).Returns(hotels);
            return mockservice;
        }
        private Mock<IHotel> GetByNameAgainsFood(List<FoodList> hotels)
        {
            var mockservice = Mock();
            mockservice.Setup(x => x.GetFoodByHotelName(It.IsAny<string>())).Returns(hotels);
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
            Assert.Equal(1, Lisobj.Count);
            Assert.NotNull(okresult);
        }

        [Fact]
        public void HotelGetAll_NotFoundResult()
        {
            Messages obj = new Messages();
            obj.Success = false;
            List<Hotel> Lisobj = null;
            var mockservice = new Mock<IHotel>();
            mockservice.Setup(x => x.GetAll()).Returns(Lisobj);
            var controller = new HotelController(mockservice.Object);
            var NotFoundResult = controller.GetHotelList();
            var output = NotFoundResult as NotFoundObjectResult;
            Assert.Equal("Hotel list is not found", output.Value);

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
            msg.Message = "The hotel id is not found";
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
        public void HotelGetName_OkResult()
        {
            List<Hotel> hotel = new List<Hotel>();
            hotel.Add(TestData);
            var controller = new HotelController(GetByName(hotel).Object);
            var okresult = controller.GetHotelByName("KFC");
            var list = okresult as OkObjectResult;
            var result = list.Value as Hotel;
            Assert.IsType<OkObjectResult>(okresult);
            Assert.StrictEqual(200, list.StatusCode);
        }

        [Fact]
        public void HotelGetName_NotFound()
        {
            Messages msg = new Messages();
            msg.Success = true;
            msg.Message = "The hotel is not found";
            List<Hotel> hotel = null;
            var controller = new HotelController(GetByName(hotel).Object);
            var okresult = controller.GetHotelByName("KFC");
            var list = okresult as NotFoundObjectResult;
            var result = list.Value as Hotel;
            Assert.IsType<NotFoundObjectResult>(okresult);
            Assert.StrictEqual(404, list.StatusCode);
            Assert.Equal("The hotel is not found", list.Value);
        }

        [Fact]
        public void HotelAgainsFood_OkResult()
        {
            List<FoodList> hotel = new List<FoodList>();
            hotel.Add(foodList);
            var controller = new HotelController(GetByNameAgainsFood(hotel).Object);
            var okresult = controller.GetHotelByNameAgainsFood("KFC");
            var list = okresult as OkObjectResult;
            var result = list.Value as Hotel;
            Assert.IsType<OkObjectResult>(okresult);
            Assert.StrictEqual(200, list.StatusCode);
        }

        [Fact]
        public void HotelAgainsFood_NotFound()
        {
            Messages msg = new Messages();
            msg.Success = true;
            msg.Message = "The hotel is not found";
            List<FoodList> hotel = null;
            var controller = new HotelController(GetByNameAgainsFood(hotel).Object);
            var okresult = controller.GetHotelByNameAgainsFood("KFC");
            var list = okresult as NotFoundObjectResult;
            var result = list.Value as Hotel;
            Assert.IsType<NotFoundObjectResult>(okresult);
            Assert.StrictEqual(404, list.StatusCode);
            Assert.Equal("The food is not found", list.Value);
        }

      
        [Fact]
        public void GetHoteltype_OkResult()
        {
            List<Hotel> hotel = new List<Hotel>();
            hotel.Add(TestData);
            var controller = new HotelController(GetHotelType(hotel).Object);
            var okresult = controller.GetHotelType("veg");
            var list = okresult as OkObjectResult;
            var result = list.Value as Hotel;
            Assert.IsType<OkObjectResult>(okresult);
            Assert.StrictEqual(200, list.StatusCode);
        }


        [Fact]
        public void GetHoteltype_NotFound()
        {
            Messages msg = new Messages();
            msg.Success = true;
            msg.Message = "The food type not found";
            msg.Status=Statuses.NotFound;
            List<Hotel> hotel = null;
            var controller = new HotelController(GetHotelType(hotel).Object);
            var okresult = controller.GetHotelType("veg");
            var list = okresult as NotFoundObjectResult;
            var result = list.Value as Hotel;
            Assert.IsType<NotFoundObjectResult>(okresult);
            Assert.StrictEqual(404, list.StatusCode);
            Assert.Equal("The food type not found", list.Value);
        }

        [Fact]
        public void AddHotel_OKResult()
        {
            Messages msg = new Messages();
            msg.Success = true;
            msg.Message = "Contact Number Already Taked";
            msg.Status = Statuses.Created;
            var controller = new HotelController(AddHotelMock(msg).Object);
            var output = controller.AddHotelDetail(TestData);
            var result = output as CreatedResult;
            Assert.IsType<CreatedResult>(output);
            Assert.StrictEqual(msg.Message, result.Value);
            Assert.StrictEqual(201, result.StatusCode);
        }


        [Fact]
        public void AddHotel_ContactNumberConflict()
        {
            Messages msg = new Messages();
            msg.Message = "This contact number id already exists";
            msg.Success = false;
            msg.Status = Statuses.Conflict;
            var mockservice = new Mock<IHotel>();
            mockservice.Setup(x => x.InsertHotelDetail(It.IsAny<Hotel>())).Returns(msg);
            var controller = new HotelController(mockservice.Object);
            var output = controller.AddHotelDetail(TestData);
            var result = output as ConflictObjectResult;
            Assert.Equal(msg.Message, result.Value);
            Assert.StrictEqual(409, result.StatusCode);
            Assert.IsType<ConflictObjectResult>(output);
        }

        [Fact]
        public void AddHotel_EmailConflict()
        {
            Messages msg = new Messages();
            msg.Message = "Email Id Already Taked";
            msg.Success = false;
            msg.Status = Statuses.Conflict;
            var mockservice = new Mock<IHotel>();
            mockservice.Setup(x => x.InsertHotelDetail(It.IsAny<Hotel>())).Returns(msg);
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
            Assert.StrictEqual(obj.Message, result.Value);
            Assert.StrictEqual(200, result.StatusCode);
        }

        [Fact]
        public void Update_NotFoundResult()
        {
            Messages msg = new Messages();
            msg.Success = true;
            msg.Message = "Hotel id not found";
            msg.Status = Statuses.NotFound;
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
            msg.Status = Statuses.BadRequest;
            var controller = new HotelController(UpdateHotelMock(msg).Object);
            var output = controller.UpdateHotelDetail(hotel);
            var result = output as BadRequestObjectResult;
            Assert.IsType<BadRequestObjectResult>(output);
            Assert.StrictEqual(msg.Message, result.Value);
            Assert.StrictEqual(400, result.StatusCode);
            Assert.True(hotel.HotelId == 0);
        }


        [Fact]
        public void Update_EmailConfict()
        {
            Messages msg = new Messages();
            msg.Success = false;
            msg.Message = "This email id already exist";
            msg.Status = Statuses.Conflict;
            var mockservice = new Mock<IHotel>();
            mockservice.Setup(x => x.GetHotelById(It.IsAny<int>())).Returns(TestData);
            mockservice.Setup(x => x.UpdateHotelDetail(It.IsAny<Hotel>())).Returns(msg);
            var controller = new HotelController(mockservice.Object);
            var output = controller.UpdateHotelDetail(TestData);
            var result = output as ConflictObjectResult;
            Assert.IsType<ConflictObjectResult>(output);
            Assert.Equal("This email id already exist", result.Value);
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
            Assert.Equal(msg.Message, result.Value);
            Assert.StrictEqual(200, result.StatusCode);
        }

        [Fact]
        public void DeleteHotel_NotFound()
        {
            Messages msg = new Messages();
            msg.Success = true;
            msg.Message = "The hotel id not found";
            msg.Status = Statuses.NotFound;
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
            msg.Message = "The hotel food is available for users";
            msg.Status = Statuses.BadRequest;
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