using AutoMapper;
using MediatR;
using Vb.Base.Response;
using Vb.Business.Features.Accounts.Constants;
using Vb.Data;
using Vb.Data.Entity;

namespace Vb.Business.Features.Accounts.Commands.Update;
public class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand, ApiResponse>
{
    private readonly VbDbContext dbContext;
    private readonly IMapper mapper;

    public UpdateAccountCommandHandler(VbDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<Account>()
            .FindAsync(request.AccountNumber, cancellationToken);

        if (entity == null)
            return new ApiResponse(AccountMessages.RecordNotExists);

        entity.Name = request.Model.Name;
        entity.Balance = request.Model.Balance;
        entity.CurrencyType = request.Model.CurrencyType;
        // Only three properties are allowed to be updated.        

        await dbContext.SaveChangesAsync(cancellationToken);

        return new ApiResponse();
    }
}
