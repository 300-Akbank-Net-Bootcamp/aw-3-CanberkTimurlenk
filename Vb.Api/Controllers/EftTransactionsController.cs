using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vb.Business.Features.EftTransactions.Commands.Create;
using Vb.Business.Features.EftTransactions.Commands.Delete;
using Vb.Business.Features.EftTransactions.Commands.Update;
using Vb.Business.Features.EftTransactions.Queries.GetAll;
using Vb.Business.Features.EftTransactions.Queries.GetById;
using Vb.Business.Features.EftTransactions.Queries.GetByParameter;
using Vb.Schema;

namespace VbApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EftTransactionsController : ControllerBase
{
    private readonly IMediator mediator;

    public EftTransactionsController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetEftTransactionsByParameter(int AccountId, string? ReferenceNumber, string? SenderAccount, string? SenderIban, string? SenderName)
    {
        var operation = new GetEftTransactionsByParameterQuery(AccountId, ReferenceNumber, SenderAccount, SenderIban, SenderName);
        var result = await mediator.Send(operation);
        return Ok(result);
    }

    //[HttpGet]
    //public async Task<IActionResult> GetAllEftTransactions()
    //{
    //    var operation = new GetAllEftTransactionsQuery();
    //    var result = await mediator.Send(operation);
    //    return Ok(result);
    //}

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEftTransactionById(int id)
    {
        var operation = new GetEftTransactionByIdQuery(id);
        var result = await mediator.Send(operation);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] EftTransactionRequest request)
    {
        var operation = new CreateEftTransactionCommand(request);
        var result = await mediator.Send(operation);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] EftTransactionRequest request)
    {
        var operation = new UpdateEftTransactionCommand(id, request);
        var result = await mediator.Send(operation);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var operation = new DeleteEftTransactionCommand(id);
        var result = await mediator.Send(operation);
        return Ok(result);
    }





}
