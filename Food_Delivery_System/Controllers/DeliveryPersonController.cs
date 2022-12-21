using Food_Delivery.Models;
using Food_Delivery.RepositoryInterface;
using Food_Delivery_System.Models.Commands;
using Food_Delivery_System.Models.Queries;
using MediatR;
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
        IMediator mediator;
        public DeliveryPersonController(IDeliveryPerson deliveryPerson , IMediator _mediatR)
        {
            _deliveryperson = deliveryPerson;
            mediator = _mediatR;
        }

        [HttpGet("Getall")]
        public async Task<List<DeliveryPerson>> GetAllDeliveryPersons() => await mediator.Send(new GetAllDeliveryQuery());
      
        [HttpGet("{deliveryPersonId}")]
        public IActionResult GetDeliveryPerson(int deliveryPersonId)
        {
            var obj = _deliveryperson.GetDeliveryPerson(deliveryPersonId);
            return  (obj != null)?  Ok(obj) : NotFound("This delivery person id is not found");
        }

        [HttpPost("/api/DeliveryPerson")]
        public async Task<DeliveryPerson> InsertDeliveryPerson([FromBody] DeliveryPerson deliveryPerson) => await mediator.Send(new InsertDeliveryCommand(deliveryPerson));

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
        private IActionResult Output(Messages message)
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
