using MediatR;
using Vb.Base.Response;
using Vb.Schema;

namespace Vb.Business.Features.EftTransactions.Queries.GetById;
public record GetEftTransactionByIdQuery(int Id) : IRequest<ApiResponse<EftTransactionResponse>>;