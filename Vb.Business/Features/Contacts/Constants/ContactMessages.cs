using Vb.Data.Entity;

namespace Vb.Business.Features.Contacts.Constants;
public static class ContactMessages
{
    // Business Logic
    public const string DefaultContactAlreadyExistsForCustomerId = "A default contact already exists for the customer id: {0}";
    public const string RecordNotExists = "Record not found";

    // Fluent Validation
    public const string ContactTypeIsRequired = "Contact type is required";
    public const string ContactTypeMaxLength = "Contact type must not exceed 20 characters";

    public const string InformationIsRequired = "Information is required";
    public const string InformationMaxLength = "Information must not exceed 50 characters";
    public const string InformationMinLength = "Information must be at least 10 characters";

    public const string CustomerIdIsRequired = "Customer id is required";
}
