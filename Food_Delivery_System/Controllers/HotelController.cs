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
            return Created("https://localhost:7187/Api/Hotel/" + detaile.HotelId + "", hoteldetail);
        }


        [HttpPut("")]

        public IActionResult UpdateHotelDetail(Hotel hotel)
        {
            var id = _hotel.GetHotelById(hotel.HotelId);
            if(id == null)
            {
                return NotFound("Hotel Id Not Found");
            }
            if (hotel.HotelId == 0)
            {
                return BadRequest("The Hotel Field Is Required");
            }

            var hotelUpdate=_hotel.UpdateHotelDetail(hotel);
            if (hotelUpdate.Success == false)
            {
                return Conflict("The Contact Number Already Taked");
            }

            return Ok(hotelUpdate);
        }

        [HttpDelete("/{hotelDetailId}")]

        public IActionResult DeleteHotelDetail(int hotelDetailId)
        {
            var id = _hotel.GetHotelById(hotelDetailId);
            if (id == null)
            {
                return NotFound("The Hotel Id Not Found");
            }
            var hotel = _hotel.DeleteHotelDetail(hotelDetailId);
            return Ok(hotel);
        }

        [HttpGet("Type/{hoteltype}")]

        public  IActionResult GetHotelType(string hoteltype)
        {
            var hotel = _hotel.GetHotelType(hoteltype);
            if(hotel.Count() == 0)
            {
                return NotFound("The Hotel Type Not Found");
            }
            return Ok(hotel);
        }
    }
}
