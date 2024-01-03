using AutoMapper;
using MediatR;
using Vb.Base.Response;
using Vb.Business.Features.Accounts.Constants;
using Vb.Data;
using Vb.Data.Entity;

namespace Vb.Business.Features.Accounts.Commands.Delete;
public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand, ApiResponse>
{
    private readonly VbDbContext dbContext;
    private readonly IMapper mapper;

    public DeleteAccountCommandHandler(VbDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<Account>()
            .FindAsync(request.AccountNumber, cancellationToken);

        if (entity == null)
            return new ApiResponse(AccountMessages.RecordNotExists);

        entity.IsActive = false;
        await dbContext.SaveChangesAsync(cancellationToken);

        return new ApiResponse();
    }
}
