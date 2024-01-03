using MediatR;
using Vb.Base.Response;
using Vb.Schema;

namespace Vb.Business.Features.AccountTransactions.Queries.GetByParameter;
public record GetAccountTransactionsByParameterQuery(string Description, string TransferType) : IRequest<ApiResponse<List<AccountTransactionResponse>>>;