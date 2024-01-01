using MediatR;
using Vb.Base.Response;
using Vb.Schema;

namespace Vb.Business.Features.Contacts.Queries.GetAll;

public record GetAllContactsQuery() : IRequest<ApiResponse<List<ContactResponse>>>;