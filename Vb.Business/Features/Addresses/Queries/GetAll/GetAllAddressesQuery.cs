using MediatR;
using Vb.Base.Response;
using Vb.Schema;

namespace Vb.Business.Features.Addresses.Queries.GetAll;
public record GetAllAddressesQuery() : IRequest<ApiResponse<List<AddressResponse>>>;
