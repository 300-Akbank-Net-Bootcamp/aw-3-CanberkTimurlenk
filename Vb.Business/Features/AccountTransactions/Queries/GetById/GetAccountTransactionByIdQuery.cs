using MediatR;
using Vb.Base.Response;
using Vb.Schema;

namespace Vb.Business.Features.AccountTransactions.Queries.GetById;
public record GetAccountTransactionByIdQuery(int Id) : IRequest<ApiResponse<AccountTransactionResponse>>;