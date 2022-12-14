using Food_Delivery.Models;
using Food_Delivery.RepositoryInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceStack.Messaging;

namespace Food_Delivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        IHotel _hotel;
        public HotelController(IHotel hotel)
        {
            _hotel = hotel;
        }

        [HttpGet("GetAll")]
        public IActionResult GetHotelList()
        {
            Messages msg=new Messages();
            var hotel = _hotel.GetAll();
            if(hotel == null)
            {
                return NotFound("Hotel list is not found");
            }

            return Ok(hotel);
        }

        [HttpGet("{hotelid}")]
        public IActionResult GetHotelById(int hotelId)
        {
            Messages msg = new Messages();
            var hotel = _hotel.GetHotelById(hotelId);
            if (hotel == null)
            {
                return NotFound("The hotel id is not found");
            }

            return Ok(hotel);
        }

        [HttpGet("Name/{hotelName}")]
        public IActionResult GetHotelByName(string hotelName)
        {
            var hotel = _hotel.GetHotelDetailByName(hotelName);
            if (!hotel.Any())
            {
                return NotFound("The hotel is not found");
            }

            return Ok(hotel);
        }


        [HttpGet("HotelAgainstFood/{hotelName}")]
        public IActionResult GetHotelByNameAgainsFood(string hotelName)
        {
            Messages msg = new Messages();
            var hotel = _hotel.GetFoodByHotelName(hotelName);
            if (!hotel.Any())
            {
                return NotFound("The food is not found");
            }
            
            return Ok(hotel);
        }


        [HttpPost("")]
        public IActionResult AddHotelDetail(Hotel detaile)
        {
              
            var number = _hotel.GetHotelDetailByNumber(detaile.ContactNumber);
            if (number != null)
            {
                return Conflict("This contact number id already exists");
            }
            var email = _hotel.GetHotelDetailByEmail(detaile.Email);
            if (email != null)
            {
                return Conflict("This email id already exists");
            }
            var hoteldetail = _hotel.InsertHotelDetail(detaile);

            return Created(detaile.HotelId + "", hoteldetail);
        }


        [HttpPut("")]

        public IActionResult UpdateHotelDetail(Hotel hotel)
        {
            Messages msg = new Messages();    
            var id = _hotel.GetHotelById(hotel.HotelId);
           
            if (hotel.HotelId == 0)
            {
                return BadRequest("The hotel field is required");
            }
            if(id == null)
            {
                return NotFound("Hotel id not found");
            }
           var hotelUpdate = _hotel.UpdateHotelDetail(hotel);
            if (hotelUpdate.Message == "This email id already exist")
            {
                return Conflict("This email id already exist");
            }
            if (hotelUpdate.Message== "This contact number id already exists")
            {
                return Conflict("This contact number id already exists");
            }

            return Ok(hotelUpdate);
        }

        [HttpDelete("{hotelDetailId}")]

        public IActionResult DeleteHotelDetail(int hotelDetailId)
        {
            Messages messages = new Messages();
            var id = _hotel.GetHotelById(hotelDetailId);
           
            if (id == null)
            {
                return NotFound("The hotel id not found");
            }
            var hotel = _hotel.DeleteHotelDetail(hotelDetailId);
            if (hotel.Message == "The hotel food is available for users")
            {
                return BadRequest("This hotel already having food list, So Can't delete.");
            }
            return Ok(hotel);
        }

        [HttpGet("Type/{hoteltype}")]

        public  IActionResult GetHotelType(string hoteltype)
        {
            var type=_hotel.GetHotelType(hoteltype);
            if(type.Count() == 0)
            {
                return NotFound("The food type not found");
            }
            var hotel = _hotel.GetHotelType(hoteltype);
            return Ok(hotel);
        }
    }
}
