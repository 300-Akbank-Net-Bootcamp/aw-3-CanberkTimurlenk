using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vb.Base.Response;
using Vb.Business.Features.Addresses.Constants;
using Vb.Data;
using Vb.Data.Entity;

namespace Vb.Business.Features.Addresses.Commands.Update;
public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommand, ApiResponse>
{
    private readonly VbDbContext dbContext;
    private readonly IMapper mapper;

    public UpdateAddressCommandHandler(VbDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<Address>().FindAsync(request.Id, cancellationToken);

        if (fromdb == null)
            return new ApiResponse(AddressMessages.RecordNotExists);

        var hasDefaultAddress = await dbContext.Set<Address>().AnyAsync(a => a.IsDefault && a.CustomerId.Equals(request.Id), cancellationToken);
        // true; if the customer already has a default address

        if (hasDefaultAddress && request.Model.IsDefault)
            return new ApiResponse(AddressMessages.CustomerAlreadyHasDefaultAddress);
        // if the customer already has a default address and the request model is default,
        // just returns a message

        if (!(hasDefaultAddress || request.Model.IsDefault))
            fromdb.IsDefault = true;
        // if the customer doesn't have a default address and the request model is not default
        // set the current address as default

        else
            fromdb.IsDefault = request.Model.IsDefault;

        fromdb.City = request.Model.City;
        fromdb.Country = request.Model.Country;
        fromdb.County = request.Model.County;
        fromdb.Address1 = request.Model.Address1;
        fromdb.Address2 = request.Model.Address2;
        fromdb.PostalCode = request.Model.PostalCode;

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }
}
