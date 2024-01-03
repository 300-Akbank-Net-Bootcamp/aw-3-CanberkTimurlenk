using MediatR;
using Vb.Base.Response;
using Vb.Schema;

namespace Vb.Business.Features.AccountTransactions.Queries.GetAll;
public record GetAllAccountTransactionsQuery() : IRequest<ApiResponse<List<AccountTransactionResponse>>>;