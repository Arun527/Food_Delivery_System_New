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
        IOrders _orders;
        //private ICustomer @object;
        //private IOrderDetail Object;

        public OrderDetailController(IOrderDetail orderDetail, ICustomer customer, IHotel hotel,IFood food , IOrders orders)
        {
            _orderDetail = orderDetail;
            _customer = customer;
            _hotel = hotel;
            _food = food;
            _orders = orders;
        }

       

        //public OrderDetailController(IOrderDetail object1)
        //{
        //    _orderDetail = object1;
        //}

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
        public IActionResult GetAllById(int id)
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
            
            List<FoodDetaile> obj = new List<FoodDetaile>();
            obj = food.Food;
            foreach (FoodDetaile foodDetaile in obj)
            {
                var foods = _food.GetFoodTypeById(foodDetaile.FoodId);
                if (foods == null)
                {
                    return NotFound("The Food Id Is Not Found");
                }
                var hotel = _hotel.GetHotelById(foodDetaile.HotelId);
                if (hotel == null)
                {
                    return NotFound("The Hotel Id Is Not Found");
                }
                var quantity = foodDetaile.Quantity;
                if(quantity==0)
                {
                    return BadRequest("Please Enter Minimum Quantity Of 1 !!");
                }
            }
           
            var orderDetail = _orderDetail.InsertOrderDetail(food);
            return Created(food.OrderId + "",orderDetail);
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
                return NotFound("The OrderDetail Id Is Not Found");
            }
            var customer = _customer.GetCustomerDetailById(detail.CustomerId.Value);
            if (customer == null)
            {
                return NotFound("The CustomerId Is Not Found");
            }
            var orderId = _orders.GetOrder(detail.OrderId);
            if (orderId == null)
            {
                return NotFound("The OrderId Is Not Found");
            }
            var hotelId = _hotel.GetHotelById(detail.HotelId.Value);
            if(hotelId == null)
            {
                return NotFound("The HotelId Is Not Found");
            }
            var foodId = _food.GetFoodTypeById(detail.FoodId.Value);
            if(foodId == null)
            {
                return NotFound("The FoodId Is Not Found");
            }
            var orderDetail = _orderDetail.UpdateOrderDetail(detail);
            if(orderDetail.Success == false)
            {
                messages.Message = "The Order is Can't Update Because Out For Delivery";
                return Conflict(messages.Message);
            }
            return Ok(orderDetail);
        }
          
        [HttpDelete("{id}")]

        public IActionResult DeleteOrderDetail(int id)
        {
            Messages messages = new Messages();
            var orderid = _orderDetail.GetOrderDetail(id);
            if (orderid == null)
            {
                return NotFound("This Order Id Is Not Found");
            }

           
            var orderDetail = _orderDetail.DeleteOrderDetail(id);
           
            return Ok(orderDetail);
        }
    }
}
