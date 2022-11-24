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
            Messages messages = new Messages();
            messages.Message = "Food List Is Empty";
            var food = _food.GetAll();
            if(food == null)
            {
                return NotFound(messages.Message);
            }
            return Ok(food);
        }

        [HttpGet("{Id}")]
        public IActionResult GetFoodById(int Id)
        {
            Messages messages = new Messages();
            messages.Message = "FoodId Id Is Not Found";
            var hotel = _food.GetFoodTypeById(Id);
            if(hotel == null)
            {
                return NotFound(messages.Message);
            }
            return Ok(hotel);
        }

        [HttpPost("")]
        public Messages InsertFoodType(Food food)
        {
            var fooddetail = _food.InsertFoodType(food);
            return fooddetail;
        }

        [HttpPut("")]
        public Messages UpdateFoodType(Food food)
        {
            var fooddetail = _food.UpdateFood(food);
            return fooddetail;
        }

        [HttpDelete("{foodId}")]

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
