using Vb.Data.Entity;

namespace Vb.Business.Features.EftTransactions.Constants;
public static class EftTransactionMessages
{
    // Business Logic    

    public const string RecordNotExists = "Record not found";
    public const string TransactionDateInThePastNotAllowed = "Eft transaction date cannot be in the past.";
    public const string ModificationNotAllowedForCompletedTransaction = "Completed transaction cannot be modified.";
    public const string DeletionNotAllowedForCompletedTransaction = "If the transaction date is in the past, it cannot be deleted.";


    // Fluent Validation
    public const string AmountMustBeGreaterThan = "Amount must be greater than {0}";

    public const string SenderAccountIsReqired = "Sender Account is required.";

    public const string SenderIbanIsRequired = "Sender Iban is required.";

    public const string SenderNameIsRequired = "Sender Name is required.";


}
