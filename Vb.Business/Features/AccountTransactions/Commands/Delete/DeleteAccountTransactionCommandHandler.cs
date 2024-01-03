using AutoMapper;
using MediatR;
using Vb.Base.Response;
using Vb.Business.Features.AccountTransactions.Constants;
using Vb.Data;
using Vb.Data.Entity;

namespace Vb.Business.Features.AccountTransactions.Commands.Delete;

public class DeleteAccountTransactionCommandHandler : IRequestHandler<DeleteAccountTransactionCommand, ApiResponse>
{
    private readonly VbDbContext dbContext;
    private readonly IMapper mapper;

    public DeleteAccountTransactionCommandHandler(VbDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse> Handle(DeleteAccountTransactionCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<AccountTransaction>().FindAsync(request.Id, cancellationToken);

        if (fromdb == null)
            return new ApiResponse(AccountTransactionMessages.RecordNotExists);

        if (!CheckRequestIsValid(fromdb, out var errorMessage))
            return new ApiResponse(errorMessage);

        fromdb.IsActive = false;
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    /// <summary>
    /// Checks if the specified account transaction is valid for deletion.
    /// </summary>
    /// <param name="accountTransactionToDelete">The existing transaction to be checked.</param>
    /// <param name="errorMessage">
    /// If the transaction is not valid for deletion, this parameter will contain one or more error messages;
    /// otherwise, it will be an empty string.
    /// </param>
    /// <returns>
    /// Returns true if the transaction is valid to delete; otherwise, returns false.
    /// </returns>
    private bool CheckRequestIsValid(AccountTransaction accountTransactionToDelete, out string errorMessage)
    {
        // since the implementation completely consist of business logic, fluent validation were not used
        List<string> errorMessages = new();

        if (accountTransactionToDelete.TransactionDate < DateTime.Now)
            errorMessages.Add(AccountTransactionMessages.DeletionNotAllowedForCompletedTransaction);

        if (errorMessages.Count > 0)
        {
            errorMessage = string.Join("\n", errorMessages);
            // when using json parse, \n could be recognized as new line
            // or, in ApiResponse the property "Messages" could be changed as collection of string
            return false;
        }

        errorMessage = string.Empty;
        return true;
    }
}