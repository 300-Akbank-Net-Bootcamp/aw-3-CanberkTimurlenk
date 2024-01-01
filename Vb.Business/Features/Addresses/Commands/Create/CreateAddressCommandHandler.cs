using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vb.Base.Response;
using Vb.Data;
using Vb.Data.Entity;
using Vb.Schema;

namespace Vb.Business.Features.Addresses.Commands.Create
{
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

            bool isValidToAdd = request.model.IsDefault
                    ? !(await dbContext.Set<Address>().AnyAsync(x => x.CustomerId == request.model.CustomerId && x.IsDefault))
                    : true;

            if (!isValidToAdd)
                return new ApiResponse<AddressResponse>("A default address already exists");

            var entity = mapper.Map<Address>(request.model);

            await dbContext.Set<Address>().AddAsync(entity, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            var mapped = mapper.Map<Address, AddressResponse>(entity);
            return new ApiResponse<AddressResponse>(mapped);

        }
    }
}
