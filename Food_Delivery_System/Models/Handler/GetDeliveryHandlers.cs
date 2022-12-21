using Food_Delivery.Models;
using Food_Delivery_System.Models.Queries;
using MediatR;

namespace Food_Delivery_System.Models.Handler
{
    public class GetDeliveryHandlers : IRequestHandler<GetAllDeliveryQuery, List<DeliveryPerson>>
    { 
        private readonly FoodDeliveryDbContext context;

        public GetDeliveryHandlers(FoodDeliveryDbContext _context)
        {
            context = _context;
        }

        public Task<List<DeliveryPerson>> Handle(GetAllDeliveryQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(context.DeliveryPerson.ToList()); 
        }
    }
}
