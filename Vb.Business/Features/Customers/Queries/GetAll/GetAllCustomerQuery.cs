using MediatR;
using Vb.Base.Response;
using Vb.Schema;

namespace Vb.Business.Features.Customers.Queries.GetAll;

public record GetAllCustomerQuery() : IRequest<ApiResponse<List<CustomerResponse>>>;