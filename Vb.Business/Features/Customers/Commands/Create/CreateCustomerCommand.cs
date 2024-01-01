using MediatR;
using Vb.Base.Response;
using Vb.Schema;

namespace Vb.Business.Features.Customers.Commands.Create;
public record CreateCustomerCommand(CustomerRequest Model) : IRequest<ApiResponse<CustomerResponse>>;