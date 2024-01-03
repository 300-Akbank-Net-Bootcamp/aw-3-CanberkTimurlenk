using MediatR;
using Vb.Base.Response;
using Vb.Schema;

namespace Vb.Business.Features.Accounts.Queries.GetById;
public record GetAccountByIdQuery(int AccountNumber) : IRequest<ApiResponse<AccountResponse>>;