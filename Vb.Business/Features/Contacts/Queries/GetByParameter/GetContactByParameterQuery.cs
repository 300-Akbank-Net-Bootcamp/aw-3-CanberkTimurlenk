using MediatR;
using Vb.Base.Response;
using Vb.Schema;

namespace Vb.Business.Features.Contacts.Queries.GetByParameter;
public record GetContactByParameterQuery(int CustomerId, string ContactType, string Information) : IRequest<ApiResponse<List<ContactResponse>>>;