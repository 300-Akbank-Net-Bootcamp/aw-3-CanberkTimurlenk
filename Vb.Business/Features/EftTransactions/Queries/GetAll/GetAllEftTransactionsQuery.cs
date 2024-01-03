using MediatR;
using Vb.Schema;

namespace Vb.Business.Features.EftTransactions.Queries.GetAll;
public record GetAllEftTransactionsQuery() : IRequest<List<EftTransactionResponse>>;
