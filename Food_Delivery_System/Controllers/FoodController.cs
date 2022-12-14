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
        IHotel _hotel;
        private IFood @object;

        public FoodController(IFood food,IHotel hotel)
        {
            _food = food;
            _hotel = hotel;
        }

        public FoodController(IFood @object)
        {
            _food = @object;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            Messages messages = new Messages();
            messages.Message = "Food list is empty";
            var food = _food.GetAll();
            if(food == null)
            {
                return NotFound("The food id not found");
            }
            return Ok(food);
        }

        [HttpGet("{Id}")]
        public IActionResult GetById(int Id)
        {
            var hotel = _food.GetFoodTypeById(Id);
            if(hotel == null)
            {
                return NotFound("The food id not found");
            }
            var food = _food.GetFoodTypeById(Id);
            return Ok(food);
        }
          

        [HttpGet("FoodByName/{name}")]
        public IActionResult GetByFoodName(string name)
        {
            var hotel = _food.GetFoodByName(name);
            if (hotel == null)
            {
                return NotFound("The food id not found");
            }
            var food = _food.GetFoodByName(name);
            return Ok(food);
        }

        [HttpPost("")]
        public IActionResult InsertFoodType(Food food)
        {
            Messages messages = new Messages();
            if (food.HotelId == 0)
            {
                return BadRequest("The hotel id field is required");
            }

            var fooddetail = _food.InsertFoodType(food);
            if (fooddetail.Message == "The hotel id not found")
            {
                return NotFound("The hotel id not found");
            }
           
            return Ok(fooddetail);
        }

        [HttpPut("")]
        public IActionResult UpdateFoodType(Food food)
        {
            var detail = _food.GetFoodTypeById(food.FoodId);
            if(detail == null)
            {
                return NotFound("The food id not found");
            }
           

            var fooddetail = _food.UpdateFood(food);
            if (fooddetail.Success == false)
            {
                return NotFound("The hotel id not found");
            }
            return Ok(fooddetail);
        }

        [HttpDelete("{foodId}")]
        public IActionResult DeleteFoodType(int foodId)
        {
            var food =_food.GetFoodTypeById(foodId);
            if (food == null)
            {
                return NotFound("The food id not found");
            }
            var hotel = _food.DeleteFoodType(foodId);

            if(hotel.Message == "The food id is not deleted because order the customer")
            {
                return BadRequest("The food id is not deleted because order the customer");
            }
            return Ok(hotel);
        }

        [HttpGet("GetType/{foodtype}")]

        public IActionResult GetHotelType(string foodtype)
        {
            var food=_food.GetFoodType(foodtype);
            if (food.Count() == 0)
            {
                return NotFound("The food type is not found");
            }
            var foodType = _food.GetFoodType(foodtype);
            return Ok(foodType);
        }

        [HttpGet("HotelBy/{hotelId}")]
        public IActionResult GetHotelType(int hotelId)
        {
            var food = _hotel.GetHotelById(hotelId);
            if (food == null)
            {
                return NotFound("The food type is not found");
            }
            var foodType = _food.GetFoodByHotelId(hotelId);
            return Ok(foodType);
        }

    }
}
