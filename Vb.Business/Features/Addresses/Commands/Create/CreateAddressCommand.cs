using MediatR;
using Vb.Base.Response;
using Vb.Schema;

namespace Vb.Business.Features.Addresses.Commands.Create;
public record CreateAddressCommand(AddressRequest model) : IRequest<ApiResponse<AddressResponse>>;