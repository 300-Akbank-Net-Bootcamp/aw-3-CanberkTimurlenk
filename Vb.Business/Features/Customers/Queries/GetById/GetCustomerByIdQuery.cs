using MediatR;
using Vb.Base.Response;
using Vb.Schema;

namespace Vb.Business.Features.Customers.Queries.GetById;

public record GetCustomerByIdQuery(int Id) : IRequest<ApiResponse<CustomerResponse>>;