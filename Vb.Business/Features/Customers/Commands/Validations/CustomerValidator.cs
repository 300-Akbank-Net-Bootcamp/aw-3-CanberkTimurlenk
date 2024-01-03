using FluentValidation;
using Vb.Business.Features.Addresses.Commands.Validator;
using Vb.Business.Features.Customers.Constants;
using Vb.Schema;

namespace Vb.Business.Features.Customers.Commands.Validations;

public class CustomerValidator : AbstractValidator<CustomerRequest>
{
    public CustomerValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.IdentityNumber).NotEmpty().MaximumLength(11).WithName(CustomerMessages.IdentityNumberDisplayedName);
        RuleFor(x => x.DateOfBirth).NotEmpty();

        RuleForEach(x => x.Addresses).SetValidator(new CreateAddressValidator());
    }
}