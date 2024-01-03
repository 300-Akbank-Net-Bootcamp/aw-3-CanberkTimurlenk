using AutoMapper;
using MediatR;
using Vb.Base.Response;
using Vb.Business.Features.EftTransactions.Constants;
using Vb.Data;
using Vb.Data.Entity;

namespace Vb.Business.Features.EftTransactions.Commands.Delete;
public class DeleteEftTransactionCommandHandler : IRequestHandler<DeleteEftTransactionCommand, ApiResponse>
{
    private readonly VbDbContext dbContext;
    private readonly IMapper mapper;

    public DeleteEftTransactionCommandHandler(VbDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse> Handle(DeleteEftTransactionCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<EftTransaction>().FindAsync(request.Id, cancellationToken);

        if (!CheckIfExistedEftTransactionIsEligibleToDelete(fromdb, out var errorMessage))
            return new ApiResponse(errorMessage);

        if (fromdb == null)
            return new ApiResponse(EftTransactionMessages.RecordNotExists);

        fromdb.IsActive = false;
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    /// <summary>
    /// Checks if the specified EFT transaction is eligible to delete.
    /// </summary>
    /// <param name="transactionToDelete">The existing EFT transaction to be checked.</param>
    /// <param name="errorMessage">
    /// If the transaction is not eligible for deletion, this parameter will contain one or more error messages;
    /// otherwise, it will be an empty string.
    /// </param>
    /// <returns>
    /// Returns true if the EFT transaction is eligible to delete; otherwise, returns false.
    /// </returns>
    private bool CheckIfExistedEftTransactionIsEligibleToDelete(EftTransaction transactionToDelete, out string errorMessage)
    {
        // since the implementation completely consist of business logic, fluent validation were not used       
        List<string> errorMessages = new();

        if (transactionToDelete.TransactionDate < DateTime.Now)
            errorMessages.Add(EftTransactionMessages.DeletionNotAllowedForCompletedTransaction);

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
