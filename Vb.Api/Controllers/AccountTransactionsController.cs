using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vb.Base.Response;
using Vb.Business.Features.AccountTransactions.Commands.Create;
using Vb.Business.Features.AccountTransactions.Commands.Delete;
using Vb.Business.Features.AccountTransactions.Commands.Update;
using Vb.Business.Features.AccountTransactions.Queries.GetById;
using Vb.Business.Features.AccountTransactions.Queries.GetByParameter;
using Vb.Schema;

namespace VbApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountTransactionsController : ControllerBase
{
    private readonly IMediator mediator;

    public AccountTransactionsController(IMediator mediator)
    {
        this.mediator = mediator;
    }


    [HttpGet]
    public async Task<ApiResponse<List<AccountTransactionResponse>>> GetAccountTransactionsByParameter(string? description, string? transferType)
    {
        var operation = new GetAccountTransactionsByParameterQuery(description, transferType);
        var result = await mediator.Send(operation);
        return result;
    }

    //[HttpGet]
    //public async Task<ApiResponse<List<AccountTransactionResponse>>> GetAllAccountTransactions()
    //{
    //    var operation = new GetAllAccountTransactionsQuery();
    //    var result = await mediator.Send(operation);
    //    return result;
    //}

    [HttpGet("{id}")]
    public async Task<ApiResponse<AccountTransactionResponse>> GetAccountTransactionById(int id)
    {
        var operation = new GetAccountTransactionByIdQuery(id);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPost]
    public async Task<ApiResponse<AccountTransactionResponse>> Post([FromBody] AccountTransactionRequest request)
    {
        var operation = new CreateAccountTransactionCommand(request);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPut("{id}")]
    public async Task<ApiResponse> Put(int id, [FromBody] AccountTransactionRequest request)
    {
        var operation = new UpdateAccountTransactionCommand(id, request);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpDelete("{id}")]
    public async Task<ApiResponse> Delete(int id)
    {
        var operation = new DeleteAccountTransactionCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }
}