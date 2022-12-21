using Food_Delivery.Models;
using MediatR;

namespace Food_Delivery_System.Models.Commands
{
    public record InsertDeliveryCommand(DeliveryPerson DeliveryPerson): IRequest<DeliveryPerson>; 
  
}
