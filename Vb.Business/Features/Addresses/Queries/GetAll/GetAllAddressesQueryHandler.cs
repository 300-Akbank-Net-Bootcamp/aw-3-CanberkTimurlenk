using AutoMapper;
using Vb.Base.Response;
using Vb.Data.Entity;
using Vb.Data;
using Vb.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vb.Business.Features.Addresses.Queries.GetAll;

public class GetAllAddressesQueryHandler
{
    private readonly VbDbContext dbContext;
    private readonly IMapper mapper;

    public GetAllAddressesQueryHandler(VbDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<AddressResponse>>> Handle(GetAllAddressesQuery request,
        CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<Address>()
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var mappedList = mapper.Map<List<Address>, List<AddressResponse>>(list);
        return new ApiResponse<List<AddressResponse>>(mappedList);
    }
}
