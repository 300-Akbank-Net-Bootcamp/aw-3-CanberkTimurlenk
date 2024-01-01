using MediatR;
using Vb.Base.Response;
using Vb.Schema;

namespace Vb.Business.Features.Addresses.Commands.Update;

public record UpdateAddressCommand(int id, AddressRequest Model) : IRequest<ApiResponse>;