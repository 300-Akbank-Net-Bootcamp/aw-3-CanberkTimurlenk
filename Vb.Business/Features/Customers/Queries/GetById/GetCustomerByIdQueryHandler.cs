﻿using AutoMapper;
using Vb.Base.Response;
using Vb.Data.Entity;
using Vb.Data;
using Vb.Schema;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace Vb.Business.Features.Customers.Queries.GetById;

public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, ApiResponse<CustomerResponse>>
{
    private readonly VbDbContext dbContext;
    private readonly IMapper mapper;

    public GetCustomerByIdQueryHandler(VbDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<CustomerResponse>> Handle(GetCustomerByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<Customer>()
            .Include(x => x.Accounts)
            .Include(x => x.Contacts)
            .Include(x => x.Addresses)
            .FirstOrDefaultAsync(x => x.CustomerNumber == request.Id, cancellationToken);

        if (entity == null)
            return new ApiResponse<CustomerResponse>("Record not found");

        var mapped = mapper.Map<Customer, CustomerResponse>(entity);
        return new ApiResponse<CustomerResponse>(mapped);
    }
}
