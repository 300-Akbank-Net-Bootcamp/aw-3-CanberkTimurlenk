using AutoMapper;
using MediatR;
using Vb.Base.Response;
using Vb.Business.Features.Addresses.Constants;
using Vb.Data;
using Vb.Data.Entity;

namespace Vb.Business.Features.Addresses.Commands.Delete
{
    public class DeleteAddressCommandHandler : IRequestHandler<DeleteAddressCommand, ApiResponse>
    {
        private readonly VbDbContext dbContext;
        private readonly IMapper mapper;

        public DeleteAddressCommandHandler(VbDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<ApiResponse> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
        {
            var fromdb = await dbContext.Set<Address>().FindAsync(request.Id, cancellationToken);
            // when using Find/FindAsync, if entity is already being tracked, it will return the tracked entity
            // instead of querying the database again

            if (fromdb == null)
                return new ApiResponse(AddressMessages.RecordNotExists);

            fromdb.IsActive = false;
            await dbContext.SaveChangesAsync(cancellationToken);
            return new ApiResponse();
        }
    }
}
