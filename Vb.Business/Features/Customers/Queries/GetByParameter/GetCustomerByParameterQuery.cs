using MediatR;
using Vb.Base.Response;
using Vb.Schema;

namespace Vb.Business.Features.Customers.Queries.GetByParameter;

public record GetCustomerByParameterQuery(string FirstName, string LastName, string IdentityNumber) : IRequest<ApiResponse<List<CustomerResponse>>>;
