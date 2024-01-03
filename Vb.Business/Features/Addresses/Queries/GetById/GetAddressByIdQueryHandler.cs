using AutoMapper;
using Vb.Base.Response;
using Vb.Data.Entity;
using Vb.Data;
using Vb.Schema;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Vb.Business.Features.Addresses.Constants;

namespace Vb.Business.Features.Addresses.Queries.GetById;
public class GetAddressByIdQueryHandler : IRequestHandler<GetAddressByIdQuery, ApiResponse<AddressResponse>>
{
    private readonly VbDbContext dbContext;
    private readonly IMapper mapper;

    public GetAddressByIdQueryHandler(VbDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<AddressResponse>> Handle(GetAddressByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<Address>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
            return new ApiResponse<AddressResponse>(AddressMessages.RecordNotExists);

        var mapped = mapper.Map<Address, AddressResponse>(entity);
        return new ApiResponse<AddressResponse>(mapped);
    }
}
