using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vb.Base.Response;
using Vb.Data;
using Vb.Data.Entity;
using Vb.Schema;

namespace Vb.Business.Features.Addresses.Queries.GetByParameter;

public class GetAddressByParameterQueryHandler :
    IRequestHandler<GetAddressByParameterQuery, ApiResponse<List<AddressResponse>>>
{
    private readonly VbDbContext dbContext;
    private readonly IMapper mapper;

    public GetAddressByParameterQueryHandler(VbDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<AddressResponse>>> Handle(GetAddressByParameterQuery request,
               CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<Address>()
            .AsNoTracking() // since the data is fetched for read only purposes
                            // as no tracking is used to improve performance
            .Where(x =>
                       (request.County == null || x.County.ToUpper().Contains(request.County.ToUpper())) &&
                       (request.PostalCode == null || x.PostalCode.ToUpper().Contains(request.PostalCode.ToUpper())) &&
                       (request.CustomerId == 0 || x.CustomerId.Equals(request.CustomerId)))
            // since customerId is value type; in case of an empty value, it will be equal to 0
            // if auto increment is used, it will never be 0 for new records
            // otherwise, there must be a constraint applied
            .ToListAsync(cancellationToken);

        var mappedList = mapper.Map<List<Address>, List<AddressResponse>>(list);
        return new ApiResponse<List<AddressResponse>>(mappedList);
    }
}