using MediatR;
using Vb.Base.Response;
using Vb.Schema;

namespace Vb.Business.Features.Addresses.Queries.GetByParameter;

public record GetAddressByParameterQuery(int customerId, string county, string postalCode) : IRequest<ApiResponse<List<AddressResponse>>>;