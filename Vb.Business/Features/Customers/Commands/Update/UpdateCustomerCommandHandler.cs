using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vb.Base.Response;
using Vb.Data;
using Vb.Data.Entity;

namespace Vb.Business.Features.Customers.Commands.Update;
public class UpdateCustomerCommandHandler :
    IRequestHandler<UpdateCustomerCommand, ApiResponse>

{
    private readonly VbDbContext dbContext;
    private readonly IMapper mapper;

    public UpdateCustomerCommandHandler(VbDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<Customer>().Where(x => x.CustomerNumber == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (fromdb == null)
            return new ApiResponse("Record not found");

        fromdb.FirstName = request.Model.FirstName;
        fromdb.LastName = request.Model.LastName;

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }
}