using MediatR;
using Vb.Base.Response;
using Vb.Schema;

namespace Vb.Business.Features.Addresses.Queries.GetByParameter;

public record GetAddressByParameterQuery(int CustomerId, string County, string PostalCode) : IRequest<ApiResponse<List<AddressResponse>>>;