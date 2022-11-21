using Food_Delivery.Models;
using Food_Delivery.RepositoryInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace Food_Delivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryPersonController : ControllerBase
    {
        ICustomer _customer;
        IOrders _order;
        IDeliveryPerson _deliveryperson;

        public DeliveryPersonController(IOrders order, ICustomer customer,IDeliveryPerson deliveryPerson)
        {
            _order = order;
            _customer = customer;
            _deliveryperson = deliveryPerson;
        }


        [HttpGet("/api/DeliveryPerson/Getall")]
        public IActionResult GetAllDeliveryPersons()
        {
            var obj = _deliveryperson.GetAllDeliveryPersons();
            return Ok(obj);
        }


        [HttpGet("/api/DeliveryPerson/Get/{deliveryPersonId}")]
        public DeliveryPerson GetDeliveryPerson(int deliveryPersonId)
        {
            var obj = _deliveryperson.GetDeliveryPerson(deliveryPersonId);
            return obj;
        }


        [HttpPost("/api/DeliveryPerson/Add")]
        public Messages InsertDeliveryPerson(DeliveryPerson deliveryPerson)
        {
            var insertDeliveryPerson = _deliveryperson.InsertDeliveryPerson(deliveryPerson);
            return insertDeliveryPerson;
        }

        [HttpPut("/api/DeliveryPerson/Update")]
        public Messages UpdateDeliveryPerson([FromBody]DeliveryPerson deliveryPerson)
        {
            var updateDeliveryPerson = _deliveryperson.UpdateDeliveryPerson(deliveryPerson);
            return updateDeliveryPerson;
        }


        [HttpDelete("/api/DeliveryPerson/Delete/{id}")]
        public Messages DeleteDeliveryPerson(int deliveryPersonId)
        {
            var deleteDeliveryPerson = _deliveryperson.DeleteDeliveryPerson(deliveryPersonId);
            return deleteDeliveryPerson;
        }

    }
}
