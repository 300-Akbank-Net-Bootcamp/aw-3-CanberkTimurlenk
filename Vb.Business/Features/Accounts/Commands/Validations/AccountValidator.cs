using FluentValidation;
using Vb.Business.Features.Accounts.Constants;
using Vb.Schema;

namespace Vb.Business.Features.Accounts.Commands.Validations;
public class AccountValidator : AbstractValidator<AccountRequest>
{
    public AccountValidator()
    {
        RuleFor(acc => acc.Balance)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage(AccountMessages.BalanceGreaterThanZero);

        RuleFor(acc => acc.IBAN)
            .NotEmpty()
            .WithMessage(AccountMessages.IbanNotEmpty)
            .Length(18)
            .WithMessage(AccountMessages.IbanLength);

        RuleFor(acc => acc.Name)
            .NotEmpty()
            .WithMessage(AccountMessages.NameNotEmpty)
            .MaximumLength(50)
            .WithMessage(AccountMessages.NameLength);
    }
}
