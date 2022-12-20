using Food_Delivery.Models;
using Food_Delivery.RepositoryInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Food_Delivery.Models.Messages;

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
            var food = _food.GetAll();
            return (food == null) ? NotFound("Food list is empty") : Ok(food);
        }

        [HttpGet("{Id}")]
        public IActionResult GetById(int Id)
        {
            var hotel = _food.GetFoodTypeById(Id);
            return (hotel == null) ? NotFound("The food id not found") : Ok(hotel);
        }
          
        [HttpGet("FoodByName/{name}")]
        public IActionResult GetByFoodName(string name)
        {
            var hotel = _food.GetFoodByName(name);
            return (hotel == null) ? NotFound("The food id not found") : Ok(hotel);
        }

        [HttpPost("")]
        public IActionResult InsertFoodType(Food food)
        {
            var fooddetail = _food.InsertFoodType(food);  
            return (food.HotelId==null)? BadRequest ("The hotel field is required") : Output(fooddetail);
        }

        [HttpPut("")]
        public IActionResult UpdateFoodType(Food food)
        {
            var fooddetail = _food.UpdateFood(food);
            return Output(fooddetail);
        }

        [HttpDelete("{foodId}")]
        public IActionResult DeleteFoodType(int foodId)
        {
            var food = _food.DeleteFoodType(foodId);
            return Output(food);
        }

        [HttpGet("GetType/{foodtype}")]
        public IActionResult GetFoodType(string foodtype)
        {
            var food=_food.GetFoodType(foodtype);
            return (food == null) ? NotFound("The food type is not found") : Ok(food);
        }

        [HttpGet("HotelBy/{hotelId}")]
        public IActionResult GetHotelType(int hotelId)
        {
            var food = _hotel.GetHotelById(hotelId);
            var foodType = _food.GetFoodByHotelId(hotelId);
            return (food == null) ? NotFound("The hotel id is not found") : Ok(foodType);
        }
        private IActionResult Output(Messages result)
        {
            switch (result.Status)
            {
                case Statuses.BadRequest:
                    return BadRequest(result.Message);
                case Statuses.NotFound:
                    return NotFound(result.Message);
                case Statuses.Conflict:
                    return Conflict(result.Message);
                case Statuses.Created:
                    return Created("", result.Message);
            }
            return Ok(result.Message);
        }
    }
}
