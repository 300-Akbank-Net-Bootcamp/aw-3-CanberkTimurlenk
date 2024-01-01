using MediatR;
using Vb.Base.Response;
using Vb.Schema;

namespace Vb.Business.Features.Contacts.Queries.GetByParameter;
public record GetContactByParameterQuery(int customerId, string contactType, string information) : IRequest<ApiResponse<List<ContactResponse>>>;