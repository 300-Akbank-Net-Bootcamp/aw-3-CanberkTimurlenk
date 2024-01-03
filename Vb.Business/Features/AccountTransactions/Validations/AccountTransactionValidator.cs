using FluentValidation;
using Vb.Business.Features.Accounts.Constants;
using Vb.Business.Features.AccountTransactions.Constants;
using Vb.Schema;

namespace Vb.Business.Features.AccountTransactions.Validations;
public class AccountTransactionValidator : AbstractValidator<AccountTransactionRequest>
{
    public AccountTransactionValidator()
    {
        RuleFor(at => at.Description)
            .NotEmpty()
            .WithMessage(AccountTransactionMessages.DescriptionIsRequired);

        RuleFor(at => at.Description)
            .MaximumLength(50)
            .WithMessage(AccountTransactionMessages.DescriptionLength);

        RuleFor(at => at.TransferType)
            .NotEmpty()
            .WithMessage(AccountTransactionMessages.TransferTypeIsRequired);

        RuleFor(at => at.Amount)
            .NotEmpty()
            .WithMessage(AccountTransactionMessages.AmountIsRequired);

        RuleFor(at => at.Amount)
            .GreaterThan(0)
            .WithMessage(string.Format(AccountTransactionMessages.AmountMustBeGreaterThan, 0));
    }
}
