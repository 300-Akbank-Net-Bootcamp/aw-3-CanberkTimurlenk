using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vb.Base.Response;
using Vb.Business.Features.Addresses.Constants;
using Vb.Data;
using Vb.Data.Entity;
using Vb.Schema;

namespace Vb.Business.Features.Addresses.Commands.Create;
public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, ApiResponse<AddressResponse>>
{
    private readonly VbDbContext dbContext;
    private readonly IMapper mapper;

    public CreateAddressCommandHandler(VbDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<AddressResponse>> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
    {
        // check if default address already exists, to add some business logic

        bool isValidToAdd = request.Model.IsDefault
                ? !(await dbContext.Set<Address>().AnyAsync(addr => addr.CustomerId == request.Model.CustomerId && addr.IsDefault))
                : true;
        // if the address in the request is default, then check if there is already a default address for the customer


        // if there is, then return an error message
        if (!isValidToAdd)
            return new ApiResponse<AddressResponse>(string.Format(AddressMessages.DefaultAddressAlreadyExistsForCustomerId, request.Model.Id));

        var entity = mapper.Map<Address>(request.Model);

        await dbContext.Set<Address>().AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var mapped = mapper.Map<Address, AddressResponse>(entity);
        return new ApiResponse<AddressResponse>(mapped);

    }
}
