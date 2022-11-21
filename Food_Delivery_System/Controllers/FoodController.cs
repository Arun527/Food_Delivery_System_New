using Food_Delivery.Models;
using Food_Delivery.RepositoryInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Food_Delivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {

        IFood _food;

        public FoodController(IFood food)
        {
            _food = food;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var food = _food.GetAll();
            return Ok(food);
        }

        [HttpGet("Get/{Id}")]
        public IActionResult GetAll(int Id)
        {
            Messages msg = new Messages();
            var hotel = _food.GetFoodTypeById(Id);
            if(hotel != null)
            {
                var food = _food.GetFoodTypeById(Id);
                return Ok(food);
            }
            msg.Message = "The Food Id Not Registered";
            msg.Success = false;
            return Ok(msg);

        }

        [HttpPost("Add")]
        public Messages InsertFoodType(Food food)
        {
            var fooddetail = _food.InsertFoodType(food);
            return fooddetail;
        }

        [HttpPut("Update")]
        public Messages UpdateFoodType(Food food)
        {
            var fooddetail = _food.UpdateFood(food);
            return fooddetail;
        }

        [HttpDelete("Delete/{foodId}")]

        public Messages DeleteFoodType(int foodId)
        {
            var hotel = _food.DeleteFoodType(foodId);
            return hotel;
        }

        [HttpGet("GetType/{Foodtype}")]

        public IEnumerable<Food> GetHotelType(string foodtype)
        {
            var foodType = _food.GetFoodType(foodtype);
            return foodType;
        }

    }
}
