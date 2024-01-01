
using MediatR;
using Vb.Base.Response;

namespace Vb.Business.Features.Customers.Commands.Delete;

public record DeleteCustomerCommand(int Id) : IRequest<ApiResponse>;