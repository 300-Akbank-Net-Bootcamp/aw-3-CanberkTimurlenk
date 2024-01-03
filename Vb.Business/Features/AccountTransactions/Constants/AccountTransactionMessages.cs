namespace Vb.Business.Features.AccountTransactions.Constants;
public static class AccountTransactionMessages
{
    // Business Logic    
    public const string RecordNotExists = "Record not found";
    public const string TransactionDateInThePastNotAllowed = "Account transaction date cannot be in the past.";
    public const string ModificationNotAllowedForCompletedTransaction = "Completed transaction cannot be modified.";
    public const string DeletionNotAllowedForCompletedTransaction = "If the transaction date is in the past, it cannot be deleted.";


    // Fluent Validation
    public const string DescriptionIsRequired = "Description is required";
    public const string DescriptionLength = "Description must be less than 50 characters.";

    public const string AmountMustBeGreaterThan = "Amount must be greater than {0}.";
    public const string AmountIsRequired = "Amount is required.";

    public const string TransferTypeIsRequired = "TransferType is required.";
}
