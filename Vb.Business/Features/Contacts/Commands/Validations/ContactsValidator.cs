using FluentValidation;
using Vb.Business.Features.Contacts.Constants;
using Vb.Schema;

namespace Vb.Business.Features.Contacts.Commands.Validations;
public class ContactsValidator : AbstractValidator<ContactRequest>
{
    public ContactsValidator()
    {
        RuleFor(c => c.ContactType)
            .NotEmpty()
            .WithMessage(ContactMessages.ContactTypeIsRequired)
            .MaximumLength(20)
            .WithMessage(ContactMessages.ContactTypeMaxLength);

        RuleFor(c => c.Information)
            .NotEmpty()
            .WithMessage(ContactMessages.InformationIsRequired)
            .MinimumLength(10)
            .WithMessage(ContactMessages.InformationMinLength)
            .MaximumLength(50)
            .WithMessage(ContactMessages.InformationMaxLength);

        RuleFor(c => c.CustomerId)
            .NotEmpty()
            .WithMessage(ContactMessages.CustomerIdIsRequired);
    }
}
