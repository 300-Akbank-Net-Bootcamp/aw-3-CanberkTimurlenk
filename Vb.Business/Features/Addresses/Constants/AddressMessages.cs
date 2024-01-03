using Vb.Data.Entity;

namespace Vb.Business.Features.Addresses.Constants;
public static class AddressMessages
{
    // Business Logic
    public const string DefaultAddressAlreadyExistsForCustomerId = "A default address already exists for the customer id: {0} ";
    public const string RecordNotExists = "Record not found";
    public const string CustomerAlreadyHasDefaultAddress = "The customer already has a default address";


    // Fluent Validation
    public const string PostalCodeDisplayedName = "Zip code or postal code";
    public const string Address1DisplayedName = "Customer address line 1";
    public const string Address2DisplayedName = "Customer address line 2";
}
