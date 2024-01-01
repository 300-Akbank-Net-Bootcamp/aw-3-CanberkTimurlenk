using MediatR;
using Vb.Base.Response;
using Vb.Schema;

namespace Vb.Business.Features.Customers.Commands.Update;

public record UpdateCustomerCommand(int Id, CustomerRequest Model) : IRequest<ApiResponse>;
