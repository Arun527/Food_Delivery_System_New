using Food_Delivery.Models;
using Food_Delivery.RepositoryInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Food_Delivery.Models.Messages;

namespace Food_Delivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryPersonController : ControllerBase
    {
        IDeliveryPerson _deliveryperson;
        public DeliveryPersonController(IDeliveryPerson deliveryPerson)
        {
            _deliveryperson = deliveryPerson;
        }

        [HttpGet("Getall")]
        public IActionResult GetAllDeliveryPersons()
        {
            var obj = _deliveryperson.GetAllDeliveryPersons();
            return (obj != null)? Ok(obj) :  NotFound("Delivery person is not found");
        }

        [HttpGet("{deliveryPersonId}")]
        public IActionResult GetDeliveryPerson(int deliveryPersonId)
        {
            var obj = _deliveryperson.GetDeliveryPerson(deliveryPersonId);
            return  (obj != null)?  Ok(obj) : NotFound("This delivery person id is not found");
        }

        [HttpPost("/api/DeliveryPerson")]
        public IActionResult InsertDeliveryPerson(DeliveryPerson deliveryPerson)
        {
            var insertDeliveryPerson = _deliveryperson.InsertDeliveryPerson(deliveryPerson);
            return Output(insertDeliveryPerson);
        }

        [HttpPut("/api/DeliveryPerson")]
        public IActionResult UpdateDeliveryPerson([FromBody]DeliveryPerson deliveryPerson)
        {
            var updateDeliveryPerson = _deliveryperson.UpdateDeliveryPerson(deliveryPerson);
            return Output(updateDeliveryPerson);
        }

        [HttpDelete("{deliveryPersonId}")]
        public IActionResult DeleteDeliveryPerson(int deliveryPersonId)
        {
            var DeliveryPerson = _deliveryperson.DeleteDeliveryPerson(deliveryPersonId);
            return Output(DeliveryPerson);
        }
        public IActionResult Output(Messages message)
        {
            switch (message.Status)
            {
                case Statuses.Conflict:
                    return Conflict(message.Message);
                case Statuses.BadRequest:
                   return BadRequest(message.Message);
                case Statuses.NotFound:
                    return NotFound(message.Message);
                case Statuses.Created:
                    return Created("",message.Message);

            }
            return Ok(message.Message);
        }

    }
}
