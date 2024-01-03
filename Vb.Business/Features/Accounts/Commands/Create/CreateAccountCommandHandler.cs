using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vb.Base.Response;
using Vb.Business.Features.Accounts.Constants;
using Vb.Business.Features.Addresses.Constants;
using Vb.Data;
using Vb.Data.Entity;
using Vb.Schema;

namespace Vb.Business.Features.Accounts.Commands.Create;

public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, ApiResponse<AccountResponse>>
{
    private readonly VbDbContext dbContext;
    private readonly IMapper mapper;

    public CreateAccountCommandHandler(VbDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<AccountResponse>> Handle(CreateAccountCommand request,
               CancellationToken cancellationToken)
    {
        if (!await dbContext.Set<Customer>().AnyAsync(c => c.CustomerNumber.Equals(request.Model.CustomerId), cancellationToken))
            return new ApiResponse<AccountResponse>(AccountMessages.CustomerNotExists);

        var entity = mapper.Map<Account>(request.Model);

        entity.AccountNumber = new Random().Next(1000000, 9999999);

        await dbContext.Set<Account>().AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var mapped = mapper.Map<Account, AccountResponse>(entity);
        return new ApiResponse<AccountResponse>(mapped);
    }
}
