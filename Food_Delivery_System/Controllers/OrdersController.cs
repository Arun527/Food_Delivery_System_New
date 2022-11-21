using Microsoft.AspNetCore.Http;
using Food_Delivery.Models;
using Food_Delivery.RepositoryInterface;
using Microsoft.AspNetCore.Mvc;

namespace Food_Delivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        ICustomer _customer;
        IOrders _order;

        public OrdersController(IOrders order, ICustomer customer)
        {
            _order = order;
            _customer = customer;
        }


        [HttpGet("/api/Order/Getall")]
        public IActionResult GetAll()
        {
            var allOrder = _order.GetAll();
            return Ok(allOrder);
        }


        [HttpGet("/api/Order/get/{orderId}")]
        public Orders GetOrder(int orderId)
        {
            var obj = _order.GetOrder(orderId);
            return obj;
        }


        [HttpPost("/api/Order/Add")]
        public Messages InsertOrder(Orders order)
        {
            var insertOrder = _order.InsertOrder(order);
            return insertOrder;
        }

        [HttpPut("/api/Order/Update")]
        public Messages UpdateOrder([FromBody] Orders order)
        {
            var updateOrder = _order.UpdateOrder(order);
            return updateOrder;
        }


        [HttpDelete("/api/Order/Delete/{id}")]
        public Messages DeleteOrder(int orderId)
        {
            var deleteOrder = _order.DeleteOrder(orderId);
            return deleteOrder;
        }

    }
}
