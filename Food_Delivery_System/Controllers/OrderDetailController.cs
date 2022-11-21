using Food_Delivery.Models;
using Food_Delivery.RepositoryInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Food_Delivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {

        IOrderDetail _orderDetail;

        public OrderDetailController(IOrderDetail orderDetail)
        {
            _orderDetail = orderDetail;
        }



        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var food = _orderDetail.GetAll();
            return Ok(food);
        }

        [HttpGet("Get/{id}")]
        public IActionResult GetAll(int id)
        {
            var food = _orderDetail.GetAll();
            return Ok(food);
        }

        [HttpPost("Add")]
        public Messages InsertFoodType(OrderRequest food)
        {
            var fooddetail = _orderDetail.InsertOrderDetail(food);
            return fooddetail;
        }

        [HttpPut("Update")]
        public Messages UpdateOrderDetail(OrderDetail detail)
        {
            var orderDetail = _orderDetail.UpdateOrderDetail(detail);
            return orderDetail;
        }

        [HttpDelete("Delete/{foodId}")]

        public Messages DeleteOrderDetail(int foodId)
        {
            var orderDetail = _orderDetail.DeleteOrderDetail(foodId);
            return orderDetail;
        }
    }
}
