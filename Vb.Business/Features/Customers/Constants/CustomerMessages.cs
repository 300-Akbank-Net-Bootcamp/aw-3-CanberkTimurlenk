using Vb.Data.Entity;

namespace Vb.Business.Features.Customers.Constants;
public static class CustomerMessages
{
    // Business Logic    
    public const string IdentityNumberUsedByAnotherCustomer = "{0} is used by another customer.";
    public const string RecordNotExists = "Record not found";

    // Fluent Validation
    public const string IdentityNumberDisplayedName = "Customer tax or identity number";
}
