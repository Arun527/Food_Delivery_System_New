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


        [HttpGet("Getall")]
        public IActionResult GetAllDeliveryPersons()
        {
            var obj = _deliveryperson.GetAllDeliveryPersons();
            if(obj == null)
            {
                return NotFound("Delivery Person Is Not Found");
            }
            return Ok(obj);
        }


        [HttpGet("{deliveryPersonId}")]
        public IActionResult GetDeliveryPerson(int deliveryPersonId)
        {
        
            var obj = _deliveryperson.GetDeliveryPerson(deliveryPersonId);
            if (obj == null)
            {
               
                return NotFound("This Delivery Person Id Is Not Found");
            }
            return Ok(obj);
        }


        [HttpPost("/api/DeliveryPerson")]
        public IActionResult InsertDeliveryPerson(DeliveryPerson deliveryPerson)
        {
            var number = _deliveryperson.GetDeliveryPersonByNumber(deliveryPerson.ContactNumber);
            if (number != null)
            {
                return NotFound("This Contact  Number  Already Taked");
            }
            var insertDeliveryPerson = _deliveryperson.InsertDeliveryPerson(deliveryPerson);
            return Ok(insertDeliveryPerson);
        }

        [HttpPut("/api/DeliveryPerson")]
        public IActionResult UpdateDeliveryPerson([FromBody]DeliveryPerson deliveryPerson)
        {
            var update = _deliveryperson.GetDeliveryPerson(deliveryPerson.DeliveryPersonId);
            if(update == null)
            {
                return NotFound("This Delivery Person Id Not Found");
            }
            var updateDeliveryPerson = _deliveryperson.UpdateDeliveryPerson(deliveryPerson);

            return Ok(updateDeliveryPerson);
        }


        [HttpDelete("{deliveryPersonId}")]
        public IActionResult DeleteDeliveryPerson(int deliveryPersonId)
        {
            var id = _deliveryperson.GetDeliveryPerson(deliveryPersonId);
            if (id == null)
            {
                return NotFound("This Delivery Person Id Is Not Found");
            }
            var DeliveryPerson = _deliveryperson.DeleteDeliveryPerson(deliveryPersonId);
            return Ok(DeliveryPerson);
        }

    }
}
