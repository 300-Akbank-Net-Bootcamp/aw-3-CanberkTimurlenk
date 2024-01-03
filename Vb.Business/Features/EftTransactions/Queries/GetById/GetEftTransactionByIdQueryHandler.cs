using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vb.Base.Response;
using Vb.Business.Features.EftTransactions.Constants;
using Vb.Data;
using Vb.Data.Entity;
using Vb.Schema;

namespace Vb.Business.Features.EftTransactions.Queries.GetById;
public class GetEftTransactionByIdQueryHandler :
    IRequestHandler<GetEftTransactionByIdQuery, ApiResponse<EftTransactionResponse>>
{
    private readonly VbDbContext dbContext;
    private readonly IMapper mapper;

    public GetEftTransactionByIdQueryHandler(VbDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<EftTransactionResponse>> Handle(GetEftTransactionByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<EftTransaction>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
            return new ApiResponse<EftTransactionResponse>(EftTransactionMessages.RecordNotExists);

        var response = mapper.Map<EftTransactionResponse>(entity);

        return new ApiResponse<EftTransactionResponse>(response);
    }
}