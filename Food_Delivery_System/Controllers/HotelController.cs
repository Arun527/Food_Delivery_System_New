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
                return NotFound("Hotel List Is Not Found");
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
                return NotFound("The Hotel Id Is Not Found");
            }

            return Ok(hotel);
        }

        [HttpGet("Name/{hotelName}")]
        public IActionResult GetHotelByName(string hotelName)
        {
            var hotel = _hotel.GetHotelDetailByName(hotelName);
            if (hotel.Count() == 0)
            {
                return NotFound("The Hotel Is Not Found");
            }

            return Ok(hotel);
        }


        [HttpGet("HotelAgainstFood/{hotelName}")]
        public IActionResult GetHotelByNameAgainsFood(string hotelName)
        {
            Messages msg = new Messages();
            var hotel = _hotel.GetFoodByHotelName(hotelName);
            if (hotel.Count() == 0)
            {
                return NotFound("The Food Is Not Found");
            }
            
            return Ok(hotel);
        }


        [HttpPost("")]
        public IActionResult AddHotelDetail(Hotel detaile)
        {
              
            var number = _hotel.GetHotelDetailByNumber(detaile.ContactNumber);
            if (number != null)
            {
                return Conflict("Contact Number Already Taked");
            }
            var email = _hotel.GetHotelDetailByEmail(detaile.Email);
            if (email != null)
            {
                return Conflict("Email Id Already Taked");
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
                return BadRequest("The Hotel Field Is Required");
            }
            if(id == null)
            {
                return NotFound("Hotel Id Not Found");
            }
           var hotelUpdate = _hotel.UpdateHotelDetail(hotel);
            if (hotelUpdate.Message== "This Email Id Already taked")
            {
                return Conflict("The Email Already Taked");
            }
            if (hotelUpdate.Message=="This Contact Number  Already taked")
            {
                return Conflict("This Contact Number  Already taked");
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
                return NotFound("The Hotel Id Not Found");
            }
            var hotel = _hotel.DeleteHotelDetail(hotelDetailId);
            if (hotel.Message == "The hotel Food Is Available For Users")
            {
                return BadRequest("The Hotel Id Is  Not Deleted,Because This Hotel Management  Created  Available  Food List ");
            }
            return Ok(hotel);
        }

        [HttpGet("Type/{hoteltype}")]

        public  IActionResult GetHotelType(string hoteltype)
        {
            var type=_hotel.GetHotelType(hoteltype);
            if(type.Count() == 0)
            {
                return NotFound("The Food Type Not Found");
            }
            var hotel = _hotel.GetHotelType(hoteltype);
            return Ok(hotel);
        }
    }
}
