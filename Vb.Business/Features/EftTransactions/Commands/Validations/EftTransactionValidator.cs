using FluentValidation;
using Vb.Business.Features.EftTransactions.Constants;
using Vb.Schema;

namespace Vb.Business.Features.EftTransactions.Commands.Validations;
public class EftTransactionValidator : AbstractValidator<EftTransactionRequest>
{
    public EftTransactionValidator()
    {
        RuleFor(x => x.Amount)
        .GreaterThan(0)
            .WithMessage(string.Format(EftTransactionMessages.AmountMustBeGreaterThan, 0));

        RuleFor(x => x.SenderAccount)
            .NotEmpty()
            .WithMessage(EftTransactionMessages.SenderAccountIsReqired);

        RuleFor(x => x.SenderIban)
            .NotEmpty()
            .WithMessage(EftTransactionMessages.SenderIbanIsRequired);

        RuleFor(x => x.SenderName)
            .NotEmpty()
            .WithMessage(EftTransactionMessages.SenderNameIsRequired);
    }
}
