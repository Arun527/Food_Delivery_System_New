using FluentValidation;
using Food_Delivery.Models;
namespace Food_Delivery_System.Models
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(2).MaximumLength(50).WithMessage("Minimum lenth of 2");
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.ContactNumber).NotEmpty().MinimumLength(10).MaximumLength(13).WithMessage("Minimum lenth of 10");
            RuleFor(x => x.Address).NotEmpty().MinimumLength(2).MaximumLength(500).WithMessage("Minimum lenth of 2");
        }
    }
}