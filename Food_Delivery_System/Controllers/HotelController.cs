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
            if(hotel != null)
            {
                return Ok(hotel);
            }

            msg.Message = "The Hotel List Is Empty";
            msg.Success = false;
            return Ok(msg);
        }

        [HttpGet("Get/{hotelid}")]
        public IActionResult GetHotelById(int hotelId)
        {
            Messages msg = new Messages();
            var hotel = _hotel.GetHotelById(hotelId);
            if (hotel == null)
            {
                return NotFound();
            }

            msg.Message = "The Hotel Id Not Registered";
            msg.Success = false;
            return Ok(msg);
        }

        [HttpPost("")]
        public Messages AddHotelDetail(Hotel detaile)
        {
            var hoteldetail = _hotel.InsertHotelDetail(detaile);
            return hoteldetail;
        }


        [HttpPut("")]

        public Messages UpdateHotelDetail(Hotel hotel)
        {
            var hotelUpdate=_hotel.UpdateHotelDetail(hotel);

            return hotelUpdate;
        }

        [HttpDelete("/{hotelDetailId}")]

        public Messages DeleteHotelDetail(int hotelDetailId)
        {
            var hotel = _hotel.DeleteHotelDetail(hotelDetailId);
            return hotel;
        }

        [HttpGet("GetType/{hoteltype}")]

        public IEnumerable<Hotel> GetHotelType(string hoteltype)
        {
            var hotel = _hotel.GetHotelType(hoteltype);
            return hotel;
        }
    }
}
