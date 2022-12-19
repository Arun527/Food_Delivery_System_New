using Food_Delivery.Models;
using Food_Delivery.RepositoryInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceStack.Messaging;
using static Food_Delivery.Models.Messages;

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
            var hotel = _hotel.GetAll();
            return (hotel == null)? NotFound("Hotel list is not found") :Ok(hotel);
        }

        [HttpGet("{hotelid}")]
        public IActionResult GetHotelById(int hotelId)
        {
            var hotel = _hotel.GetHotelById(hotelId);
            return (hotel==null)? NotFound("The hotel id is not found") : Ok(hotel);
        }

        [HttpGet("Name/{hotelName}")]
        public IActionResult GetHotelByName(string hotelName)
        {
            var hotel = _hotel.GetHotelDetailByName(hotelName);
            return (hotel==null) ? NotFound("The hotel is not found") : Ok(hotel);
        }

        [HttpGet("HotelAgainstFood/{hotelName}")]
        public IActionResult GetHotelByNameAgainsFood(string hotelName)
        {
            var hotel = _hotel.GetFoodByHotelName(hotelName);
            return (hotel==null) ? NotFound("The food is not found") : Ok(hotel);
        }

        [HttpPost]
        public IActionResult AddHotelDetail(Hotel detaile)
        {
            var hoteldetail = _hotel.InsertHotelDetail(detaile);
            return Output(hoteldetail);
        }

        [HttpPut]
        public IActionResult UpdateHotelDetail(Hotel hotel)
        {
           var hotelUpdate = _hotel.UpdateHotelDetail(hotel);
            return Output(hotelUpdate);
        }

        [HttpDelete("{hotelDetailId}")]
        public IActionResult DeleteHotelDetail(int hotelDetailId)
        {
            var deletehotel = _hotel.DeleteHotelDetail(hotelDetailId);
            return Output(deletehotel);
        }

        [HttpGet("Type/{hoteltype}")]
        public  IActionResult GetHotelType(string hoteltype)
        {
            var type=_hotel.GetHotelType(hoteltype);
            return (type==null) ? NotFound("The hotel type not found") :Ok(type);
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
                    return Created("",result.Message);
            }
            return Ok(result.Message);
         }
    }
}
