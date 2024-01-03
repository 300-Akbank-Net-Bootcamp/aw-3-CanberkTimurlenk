using AutoMapper;
using MediatR;
using Vb.Base.Response;
using Vb.Data.Entity;
using Vb.Data;
using Vb.Schema;
using Microsoft.EntityFrameworkCore;
using Vb.Business.Features.Customers.Constants;

namespace Vb.Business.Features.Customers.Commands.Create;
public class CreateCustomerCommandHandler :
IRequestHandler<CreateCustomerCommand, ApiResponse<CustomerResponse>>

{
    private readonly VbDbContext dbContext;
    private readonly IMapper mapper;

    public CreateCustomerCommandHandler(VbDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<CustomerResponse>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var checkIdentity = await dbContext.Set<Customer>()
            .AnyAsync(x => x.IdentityNumber == request.Model.IdentityNumber, cancellationToken);
        // the method changes as AnyAsync since we are just checking if there is any record with the same identity number
        // we don't need to get the entire record from the database.

        if (checkIdentity)
            return new ApiResponse<CustomerResponse>(string.Format(CustomerMessages.IdentityNumberUsedByAnotherCustomer,
                request.Model.IdentityNumber));

        var entity = mapper.Map<CustomerRequest, Customer>(request.Model);

        Random rnd = new();

        entity.CustomerNumber = rnd.Next(1000000, 9999999);

        entity.Accounts = entity.Accounts.Select(acc =>
        {
            acc.AccountNumber = rnd.Next(1000000, 9999999);
            return acc;
        }).ToList();
        // we are generating random account numbers, because we have called value generated never in the entity configuration

        var entityResult = await dbContext.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var mapped = mapper.Map<Customer, CustomerResponse>(entityResult.Entity);
        return new ApiResponse<CustomerResponse>(mapped);
    }
}