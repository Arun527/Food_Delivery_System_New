using Food_Delivery.Models;
using Food_Delivery.RepositoryInterface;
using Food_Delivery_System.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using static Food_Delivery.Models.Messages;

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
        public IOrderDetail Object { get; }
        public OrderDetailController(IOrderDetail orderDetail, ICustomer customer, IHotel hotel,IFood food , IOrders orders)
        {
            _orderDetail = orderDetail;
            _customer = customer;
            _hotel = hotel;
            _food = food;
            _orders = orders;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var order = _orderDetail.GetAll();
            return (order !=null)?Ok(order) : NotFound("OrderDetail list is empty");
        }

        [HttpGet("{id}")]
        public IActionResult GetAllById(int id)
        {
            var order = _orderDetail.GetOrderDetail(id);
            return (order!=null)?  Ok(order) : NotFound("OrderDetail id is not found");
        }

        [HttpPost]
        public IActionResult InsertFoodType(OrderRequest food)
        {
            var orderDetail = _orderDetail.InsertOrderDetail(food);
            return Output(orderDetail);
        }

        [HttpPut]
        public IActionResult UpdateOrderDetail(OrderDetail detail)
        {
           
            var orderDetail = _orderDetail.UpdateOrderDetail(detail);
            return Output(orderDetail);
        }
         
        [HttpDelete("{id}")]
        public IActionResult DeleteOrderDetail(int id)
        {
            var orderDetail = _orderDetail.DeleteOrderDetail(id);
            return Output(orderDetail);
        }
        public IActionResult Output(Messages messages)
        {
            switch (messages.Status)
            {
                case Statuses.Conflict:
                    return Conflict(messages.Message);
                case Statuses.Created:
                    return Created("", messages.Message);
                case Statuses.NotFound:
                    return NotFound(messages.Message);
                case Statuses.BadRequest:
                    return BadRequest(messages.Message);

            }
            return Ok(messages.Message);
        }
    }
}
