
using Food_Delivery.Models;
using MediatR;

namespace Food_Delivery_System.Models.Queries
{
    public record GetAllDeliveryQuery : IRequest<List<DeliveryPerson>>;
}
