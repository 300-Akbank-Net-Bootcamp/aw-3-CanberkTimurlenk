using MediatR;
using Vb.Base.Response;
using Vb.Schema;

namespace Vb.Business.Features.Accounts.Queries.GetAll;
public record GetAllAccountsQuery() : IRequest<ApiResponse<List<AccountResponse>>>;