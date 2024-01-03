using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vb.Base.Response;
using Vb.Business.Features.Accounts.Constants;
using Vb.Business.Features.AccountTransactions.Constants;
using Vb.Data;
using Vb.Data.Entity;
using Vb.Schema;

namespace Vb.Business.Features.Accounts.Queries.GetById;
public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdQuery, ApiResponse<AccountResponse>>
{
    private readonly VbDbContext dbContext;
    private readonly IMapper mapper;

    public GetAccountByIdQueryHandler(VbDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<AccountResponse>> Handle(GetAccountByIdQuery request,
               CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<Account>()
            .Include(x => x.EftTransactions)
            .Include(x => x.AccountTransactions)
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(x => x.AccountNumber == request.AccountNumber, cancellationToken);
        // since the operation will be done for read only purposes
        // AsNoTrackingWithIdentityResolution() is used to improve performance        

        if (entity == null)
            return new ApiResponse<AccountResponse>(AccountMessages.RecordNotExists);

        var mapped = mapper.Map<Account, AccountResponse>(entity);
        return new ApiResponse<AccountResponse>(mapped);
    }
}
