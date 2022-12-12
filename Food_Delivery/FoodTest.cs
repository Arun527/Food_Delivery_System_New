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
using Food_Delivery_System.Models;

namespace Food_Delivery
{
    public class FoodTest
    {
        private Food TestData => new()
        {
            FoodId = 1,
            FoodName = "idli",
            Price = 12,
            ImageId = "d2f5",
            Type = "veg"
        };
        private Mock<IFood> Mock()
        {
            var mockservice = new Mock<IFood>();
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


        //[Fact]
        //public void GetAll_OkResult()
        //{
        //    Food obj = (new Food
        //    {
        //        FoodId = 1,
        //        FoodName = "idli",
        //        Price = 12,
        //        ImageId = "d2f5",
        //        Type = "veg"
        //    });
        //    List<Food> Lisobj = new List<Food>();
        //    Lisobj.Add(obj);
        //    var mockservice = new Mock<IFood>();
        //    mockservice.Setup(x => x.GetAll()).Returns(Lisobj);
        //    var controller = new FoodController(mockservice.Object);

        //    var okresult = controller.GetHotelList();

        //    Assert.IsType<OkObjectResult>(okresult);
        //}



    }
}