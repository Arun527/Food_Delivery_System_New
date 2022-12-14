﻿using Food_Delivery.Controllers;
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
using Food_Delivery_System.Models;

namespace Food_Delivery
{
    public class FoodTest
    {
        private Food testData => new()
        {
            FoodId = 1,
            FoodName = "idli",
            Price = 12,
            ImageId = "d2f5",
            Type = "veg"
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
        private Mock<IFood> Mock()
        {
            var mockservice = new Mock<IFood>();
            return mockservice;
        }
        private Mock<IHotel> Mockhotel()
        {
            var mockservice = new Mock<IHotel>();
            return mockservice;
        }

        private Mock<IFood> GetAllMock(List<Food> food)
        {
            var mockservice = Mock();
            mockservice.Setup(x => x.GetAll()).Returns(food);
            return mockservice;
        }

        private Mock<IFood> GetById(Food food)
        {
            var mockservice = Mock();
            mockservice.Setup(x => x.GetFoodTypeById(It.IsAny<int>())).Returns(food);
            return mockservice;
        }
        private Mock<IFood> GetFoodByName(List<Food> food)
        {
            var mockservice = Mock();
            mockservice.Setup(x => x.GetFoodByName(It.IsAny<string>())).Returns(food);
            return mockservice;
        }

        private Mock<IFood> AddFoodMock(Messages message)
        {
            var mockservice = Mock();
            mockservice.Setup(x => x.InsertFoodType(It.IsAny<Food>())).Returns(message);
            return mockservice;
        }
       
        private Mock<IFood> UpdateFoodMock(Messages message)
        {
            var mockservice = Mock();
            mockservice.Setup(x => x.UpdateFood(It.IsAny<Food>())).Returns(message);
            return mockservice;
        }

        private Mock<IFood> DeleteFoodMock(Messages message)
        {
            var mockservice = Mock();
            mockservice.Setup(x => x.DeleteFoodType(It.IsAny<int>())).Returns(message);
            return mockservice;
        }

        private Mock<IFood> GetFoodByHotelIdMock(List<FoodList> food)
        {
            var mockservice = Mock();
            mockservice.Setup(x => x.GetFoodByHotelId(It.IsAny<int>())).Returns(food);
            return mockservice;
        }


        [Fact]
        public void GetAll_OkResult()
        {
            Food obj = (new Food
            {
                HotelId = 1,
                FoodId = 1,
                FoodName = "idli",
                Price = 12,
                ImageId = "d2f5",
                Type = "veg"
            });
            List<Food> Lisobj = new List<Food>();
            Lisobj.Add(obj);
            var mockservice = new Mock<IFood>();
            mockservice.Setup(x => x.GetAll()).Returns(Lisobj);
            var controller = new FoodController(mockservice.Object);

            var okresult = controller.GetAll();

            Assert.IsType<OkObjectResult>(okresult);
        }

        [Fact]
        public void FoodGetAll_NotFoundResult()
        {
            Messages obj = new Messages();
            obj.Success = false;
            obj.Message = "Food List Is Empty";
            List<Food> Lisobj = null;
            var mockservice = new Mock<IFood>();
            mockservice.Setup(x => x.GetAll()).Returns(Lisobj);
            var controller = new FoodController(mockservice.Object);
            var NotFoundResult = controller.GetAll();
            var output = NotFoundResult as NotFoundObjectResult;
            Assert.Equal("The Food Id Not Found", output.Value);

        }

        [Fact]
        public void FoodGetById_OkResult()
        {
            var controller = new FoodController(GetById(testData).Object);
            var okresult = controller.GetById(2);
            var list = okresult as OkObjectResult;
            var result = list.Value as Food;
            Assert.IsType<OkObjectResult>(okresult);
            Assert.NotNull(result);
            Assert.StrictEqual(1, result.FoodId);
            Assert.StrictEqual(200, list.StatusCode);
        }

        [Fact]
        public void FoodGetById_NotFoundResult()
        {
            Messages msg = new Messages();
            msg.Success = true;
            msg.Message = "The Food Id Not Found";
            Hotel obj1 = null;
            var mockservice = new Mock<IFood>();
            var controller = new FoodController(mockservice.Object);
            var NotFoundResult = controller.GetById(2);
            var output = NotFoundResult as NotFoundObjectResult;
            Assert.IsType<NotFoundObjectResult>(NotFoundResult);
            Assert.StrictEqual(msg.Message, output.Value);
            Assert.StrictEqual(404, output.StatusCode);
        }

        [Fact]
        public void AddFood_OkResult()
        {
            Messages msg = new Messages();
            msg.Success = true;
            msg.Message = "The Food Type Inserted Succesfully";
            var controller = new FoodController(AddFoodMock(msg).Object);
            var output = controller.InsertFoodType(testData);
            var result = output as OkObjectResult;
            Assert.IsType<OkObjectResult>(output);
            Assert.StrictEqual(msg, result.Value);
            Assert.StrictEqual(200, result.StatusCode);
        }

        [Fact]
        public void AddFood_BadRequest()
        {
            Food food= new Food { HotelId=0 };
            Messages msg = new Messages();
            msg.Success = true;
            msg.Message = "The HotelId Field Is Required";
            var controller = new FoodController(AddFoodMock(msg).Object);
            var output = controller.InsertFoodType(food);
            var result = output as BadRequestObjectResult;
            Assert.IsType<BadRequestObjectResult>(output);
            Assert.StrictEqual(msg.Message, result.Value);
            Assert.StrictEqual(400, result.StatusCode);
        }

        [Fact]
        public void AddFood_NotFound()
        {
            Messages msg = new Messages();
            msg.Success = true;
            msg.Message = "The Hotel Id Not Found";
            var controller = new FoodController(AddFoodMock(msg).Object);
            var output = controller.InsertFoodType(testData);
            var result = output as NotFoundObjectResult;
            Assert.IsType<NotFoundObjectResult>(output);
            Assert.StrictEqual(msg.Message, result.Value);
            Assert.StrictEqual(404, result.StatusCode);
        }

        [Fact]
        public void GetFoodName_SuccessOK()
        {
            Food obj = (new Food
            {
                FoodId = 1,
                FoodName = "idli",
                Price = 12,
                ImageId = "d2f5",
                Type = "veg"
            });
            List<Food> Lisobj = new List<Food>();
            var controller = new FoodController(GetFoodByName(Lisobj).Object);
            var output = controller.GetByFoodName("idli");
            var result = output as OkObjectResult;
            Assert.IsType<OkObjectResult>(output);
            Assert.StrictEqual(200, result.StatusCode);
        }

        [Fact]
        public void GetFoodName_NotFound()
        {
            Messages msg = new Messages();
            msg.Success = false;
            msg.Message = "The Food Id Not Found";
            List<Food> Lisobj = null;
            var controller = new FoodController(GetFoodByName(Lisobj).Object);
            var output = controller.GetByFoodName("grsd");
            var result = output as NotFoundObjectResult;
            Assert.IsType<NotFoundObjectResult>(output);
            Assert.Equal("The Food Id Not Found", result.Value);
            Assert.StrictEqual(404, result.StatusCode);
        }


        [Fact]
        public void Update_OKResult()
        {
            Messages obj = new Messages();
            obj.Success = true;
            obj.Message = "The Food  Is Updated Succesfully";
            Food obj1 = (new Food
            {
                FoodId = 1,
                FoodName = "idli",
                Price = 12,
                ImageId = "d2f5",
                Type = "veg"
            });
            List<Food> Obj = new List<Food>();
            Obj.Add(obj1);
            var mockservice = new Mock<IFood>();
            mockservice.Setup(x => x.GetFoodTypeById(It.IsAny<int>())).Returns(testData);
            mockservice.Setup(x => x.UpdateFood(It.IsAny<Food>())).Returns(obj);
            var controller = new FoodController(mockservice.Object);
            var output = controller.UpdateFoodType(testData);
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
            msg.Message = "The Food Id Not Found";
            var controller = new FoodController(UpdateFoodMock(msg).Object);
            var output = controller.UpdateFoodType(testData);
            var result = output as NotFoundObjectResult;
            Assert.IsType<NotFoundObjectResult>(output);
            Assert.StrictEqual(msg.Message, result.Value);
            Assert.StrictEqual(404, result.StatusCode);
        }


        [Fact]
        public void DeleteHotel_SuccessOK()
        {
            Messages msg = new Messages();
            msg.Success = false;
            msg.Message = "The hotel Id deleted Succesfully";
            var mockservice = new Mock<IFood>();
            mockservice.Setup(x => x.GetFoodTypeById(It.IsAny<int>())).Returns(testData);
            mockservice.Setup(x => x.DeleteFoodType(It.IsAny<int>())).Returns(msg);
            var controller = new FoodController(mockservice.Object);
            var output = controller.DeleteFoodType(testData.FoodId);
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
            msg.Message = "The Food Id Not Found";
            var controller = new FoodController(DeleteFoodMock(msg).Object);
            var output = controller.DeleteFoodType(3);
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
            msg.Message = "The Food Id Is Not Deleted Because Order The Customer";
            var mockservice = new Mock<IFood>();
            mockservice.Setup(x => x.GetFoodTypeById(It.IsAny<int>())).Returns(testData);
            mockservice.Setup(x => x.DeleteFoodType(It.IsAny<int>())).Returns(msg);
            var controller = new FoodController(mockservice.Object);
            var output = controller.DeleteFoodType(testData.FoodId);
            var result = output as BadRequestObjectResult;
            Assert.IsType<BadRequestObjectResult>(output);
            Assert.StrictEqual(400, result.StatusCode);
        }

        

    }
}