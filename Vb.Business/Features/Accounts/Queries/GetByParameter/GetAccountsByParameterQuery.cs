using MediatR;
using Vb.Base.Response;
using Vb.Schema;

namespace Vb.Business.Features.Accounts.Queries.GetByParameter;
public record GetAccountsByParameterQuery(int CustomerId, string IBAN) : IRequest<ApiResponse<List<AccountResponse>>>;