using MediatR;
using Vb.Base.Response;

namespace Vb.Business.Features.Addresses.Commands.Delete;
public record DeleteAddressCommand(int id) : IRequest<ApiResponse>;
