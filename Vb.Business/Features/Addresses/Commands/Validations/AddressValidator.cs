using FluentValidation;
using Vb.Business.Features.Addresses.Constants;
using Vb.Schema;

namespace Vb.Business.Features.Addresses.Commands.Validator;

public class CreateAddressValidator : AbstractValidator<AddressRequest>
{
    public CreateAddressValidator()
    {
        RuleFor(addr => addr.PostalCode)
            .NotEmpty()
            .MaximumLength(6)
            .MinimumLength(6)
            .WithName(AddressMessages.PostalCodeDisplayedName);

        RuleFor(addr => addr.Address1)
            .NotEmpty()
            .MinimumLength(20)
            .MaximumLength(100)
            .WithName(AddressMessages.Address1DisplayedName);

        RuleFor(addr => addr.Address2)
            .NotEmpty()
            .MaximumLength(100)
            .WithName(AddressMessages.Address2DisplayedName);
    }
}