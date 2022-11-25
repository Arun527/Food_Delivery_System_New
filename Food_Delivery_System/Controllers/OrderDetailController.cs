using Food_Delivery.Models;
using Food_Delivery.RepositoryInterface;
using Food_Delivery_System.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Food_Delivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {

        IOrderDetail _orderDetail;
        ICustomer _customer;
        IHotel _hotel;
        IFood _food;

        public OrderDetailController(IOrderDetail orderDetail, ICustomer customer, IHotel hotel,IFood food)
        {
            _orderDetail = orderDetail;
            _customer = customer;
            _hotel = hotel;
            _food = food;
        }


        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            Messages messages = new Messages();
            messages.Message = "Customer List Is Empty";
            var order = _orderDetail.GetAll();
            if (order == null)
            {
                return NotFound(messages.Message);
            }
            return Ok(order);
        }

        [HttpGet("{id}")]
        public IActionResult GetAll(int id)
        {
            Messages messages = new Messages();
            messages.Message = "OrderDetail Id Is Not Found";
            var order = _orderDetail.GetOrderDetail(id);
            if (order == null)
            {
                return NotFound(messages.Message);
            }
            return Ok(order);
        }

        [HttpPost("")]
        public IActionResult InsertFoodType(OrderRequest food)
        {
            var customerId = _customer.GetCustomerDetailById(food.CustomerId);
            if (customerId == null)
            {
                return NotFound("The Customer Id Is Not Found");
            }
            OrderShipmentRequest ord = new OrderShipmentRequest();
            //var user =
            List<FoodDetaile> obj = new List<FoodDetaile>();
            obj = food.Food;
            foreach (FoodDetaile foodDetaile in obj)
            {
                var foo = _food.GetFoodTypeById(foodDetaile.FoodId);
                if (foo == null)
                {
                    return NotFound("The Food Id Is Not Found");
                }
                var hot = _hotel.GetHotelById(foodDetaile.HotelId);
                if (hot == null)
                {
                    return NotFound("The Hotel Id Is Not Found");
                }
            }
            //var Food = _hotel.GetHotelById().Where(x => x.HotelId ==))
            //var hotelId = _food.GetFoodTypeById();
            ////if (hotelId == null)
            //{
            //    return NotFound("The Hotel Id Is Not Found");
            //}
            //var foodId = _food.GetFoodTypeById(food.FoodId);
            //if (foodId == null)
            //{
            //    return NotFound("The Food Id Is Not Found");
            //}
            var orderDetail = _orderDetail.InsertOrderDetail(food);
            return Created("https://localhost:7187/Api/OrderDetail/" +food.OrderId + "",orderDetail);
        }

        [HttpPut("")]
        public IActionResult UpdateOrderDetail(OrderDetail detail)
        {
            Messages messages = new Messages();
            if (detail.OrderDetailId == 0)
            {
                return BadRequest("The OrderDetailId Field Is Required");
            }
            var id = _orderDetail.GetOrderDetail(detail.OrderDetailId);
            if (id == null)
            {
                return NotFound(id);
            }
            var orderDetail = _orderDetail.UpdateOrderDetail(detail);
       
            if(orderDetail.Success == false)
            {
                messages.Message = "The Order is Out Of Delivery";
                return Conflict(messages.Message);
            }
            return Ok(orderDetail);
        }

        [HttpDelete("{id}")]

        public IActionResult DeleteOrderDetail(int id)
        {
            var orderid = _orderDetail.GetOrderDetail(id);
            if (orderid == null)
            {
                return NotFound(orderid);
            }
            var orderDetail = _orderDetail.DeleteOrderDetail(id);
            return Ok(orderDetail);
        }
    }
}
