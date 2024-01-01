using MediatR;
using Vb.Base.Response;
using Vb.Schema;

namespace Vb.Business.Features.Addresses.Queries.GetById;

public record GetAddressByIdQuery(int Id) : IRequest<ApiResponse<AddressResponse>>;
