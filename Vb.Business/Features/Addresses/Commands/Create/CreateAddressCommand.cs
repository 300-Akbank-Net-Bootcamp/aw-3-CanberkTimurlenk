using MediatR;
using Vb.Base.Response;
using Vb.Schema;

namespace Vb.Business.Features.Addresses.Commands.Create;
public record CreateAddressCommand(AddressRequest Model) : IRequest<ApiResponse<AddressResponse>>;