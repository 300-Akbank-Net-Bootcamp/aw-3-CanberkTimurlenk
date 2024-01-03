using MediatR;
using Vb.Base.Response;
using Vb.Schema;

namespace Vb.Business.Features.EftTransactions.Queries.GetByParameter;
public record GetEftTransactionsByParameterQuery(int AccountId, string ReferenceNumber, string SenderAccount, string SenderIban, string SenderName) : IRequest<ApiResponse<List<EftTransactionResponse>>>;