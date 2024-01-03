using AutoMapper;
using MediatR;
using Vb.Base.Response;
using Vb.Data.Entity;
using Vb.Data;
using Vb.Business.Features.Customers.Constants;

namespace Vb.Business.Features.Customers.Commands.Delete;

public class DeleteCustomerCommandHandler :
    IRequestHandler<DeleteCustomerCommand, ApiResponse>

{
    private readonly VbDbContext dbContext;
    private readonly IMapper mapper;

    public DeleteCustomerCommandHandler(VbDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }
    public async Task<ApiResponse> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<Customer>().FindAsync(request.Id, cancellationToken);

        if (fromdb == null)
            return new ApiResponse(CustomerMessages.RecordNotExists);

        fromdb.IsActive = false;
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }
}
