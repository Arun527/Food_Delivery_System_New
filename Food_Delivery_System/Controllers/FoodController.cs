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

        public FoodController(IFood food,IHotel hotel)
        {
            _food = food;
            _hotel = hotel;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            Messages messages = new Messages();
            messages.Message = "Food List Is Empty";
            var food = _food.GetAll();
            if(food == null)
            {
                return NotFound("The Food Id Not Found");
            }
            return Ok(food);
        }

        [HttpGet("{Id}")]
        public IActionResult GetAll(int Id)
        {
            var hotel = _food.GetFoodTypeById(Id);
            if(hotel == null)
            {
                return NotFound("The Food Id Not Found");
            }
            var food = _food.GetFoodTypeById(Id);
            return Ok(food);
        }

        [HttpPost("")]
        public IActionResult InsertFoodType(Food food)
        {
            Messages messages = new Messages();
            if (food.HotelId == 0)
            {
                return BadRequest("The HotelId Field Is Required");
            }
            var hotel = _hotel.GetHotelById(food.HotelId.Value);
            if(hotel == null)
            {
                return NotFound("The Hotel Id Not Found");
            }
            var fooddetail = _food.InsertFoodType(food);
            return Ok(fooddetail);
        }

        [HttpPut("")]
        public IActionResult UpdateFoodType(Food food)
        {
            var detail = _food.GetFoodTypeById(food.FoodId);
            if(detail == null)
            {
                return NotFound("The Food Id Not Found");
            }
           

            var fooddetail = _food.UpdateFood(food);
            if (fooddetail.Success == false)
            {
                return NotFound("The Hotel Id Not Found");
            }
            return Ok(fooddetail);
        }

        [HttpDelete("{foodId}")]
        public IActionResult DeleteFoodType(int foodId)
        {
            var food =_food.GetFoodTypeById(foodId);
            if (food == null)
            {
                return NotFound("The Food Id Not Found");
            }
            var hotel = _food.DeleteFoodType(foodId);

            if(hotel.Success == false)
            {
                return BadRequest("The Food Id Is Not Deleted Because Order The Customer");
            }
            return Ok(hotel);
        }

        [HttpGet("GetType/{foodtype}")]

        public IActionResult GetHotelType(string foodtype)
        {
            var food=_food.GetFoodType(foodtype);
            if (food.Count() == 0)
            {
                return NotFound("The Food Type Is Not Found");
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
                return NotFound("The Food Type Is Not Found");
            }
            var foodType = _food.GetFoodByHotelId(hotelId);
            return Ok(foodType);
        }

    }
}
