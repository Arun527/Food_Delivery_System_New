using Food_Delivery.Models;
using Food_Delivery_System.Models.Commands;
using Food_Delivery_System.Models.Queries;
using MediatR;

namespace Food_Delivery_System.Models.Handler
{
    public class InsertDeliveryHandler : IRequestHandler<InsertDeliveryCommand,DeliveryPerson>
    {
        private readonly FoodDeliveryDbContext context;

        public InsertDeliveryHandler(FoodDeliveryDbContext _context)
        {
            context = _context;
        }

        public async Task<DeliveryPerson> Handle(InsertDeliveryCommand request, CancellationToken cancellationToken)
        {
            context.DeliveryPerson.Add(request.DeliveryPerson);
            context.SaveChanges();

            return await Task.FromResult(request.DeliveryPerson);
        }
    }
}
