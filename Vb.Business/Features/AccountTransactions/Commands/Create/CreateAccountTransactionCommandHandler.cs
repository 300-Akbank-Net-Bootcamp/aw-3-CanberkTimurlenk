using AutoMapper;
using MediatR;
using Vb.Base.Response;
using Vb.Business.Features.AccountTransactions.Constants;
using Vb.Data;
using Vb.Data.Entity;
using Vb.Schema;

namespace Vb.Business.Features.AccountTransactions.Commands.Create;
public class CreateAccountTransactionCommandHandler :
    IRequestHandler<CreateAccountTransactionCommand, ApiResponse<AccountTransactionResponse>>
{
    private readonly VbDbContext dbContext;
    private readonly IMapper mapper;

    public CreateAccountTransactionCommandHandler(VbDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<AccountTransactionResponse>> Handle(CreateAccountTransactionCommand request, CancellationToken cancellationToken)
    {
        if (!CheckRequestIsValid(request.Model, out var errorMessage)) // early return if request is invalid
            return new ApiResponse<AccountTransactionResponse>(errorMessage);

        var accountTransaction = mapper.Map<AccountTransaction>(request.Model);

        await dbContext.AccountTransactions.AddAsync(accountTransaction, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<AccountTransactionResponse>(accountTransaction);

        return new ApiResponse<AccountTransactionResponse>(response);
    }

    /// <summary>
    /// Validates a Create Account Transaction request.
    /// </summary>
    /// <param name="request">The Create Account transaction request to be validated.</param>
    /// <param name="errorMessage">
    /// If the request is invalid, this parameter will contain an ApiResponse&lt;AccountTransactionResponse&gt;
    /// with an error message; otherwise, it will be null.
    /// </param>
    /// <returns>
    /// Returns true if the request is valid; otherwise, returns false.
    /// </returns>
    private bool CheckRequestIsValid(AccountTransactionRequest request, out string errorMessage)
    {
        // since the implementation completely consist of business logic, fluent validation were not used

        List<string> errorMessages = new();

        if (request.TransactionDate <= DateTime.UtcNow.AddMinutes(-2)) // allow 2 minute delay, to test         
            errorMessages.Add(AccountTransactionMessages.TransactionDateInThePastNotAllowed);

        if (errorMessages.Count > 0)
        {
            errorMessage = string.Join("\n", errorMessages);
            // when using json parse, \n could be recognized as new line
            // or, in ApiResponse the property "Messages" could be changed as collection of string
            return false;
        }
        else
        {
            errorMessage = string.Empty;
            return true;
        }
    }
}
