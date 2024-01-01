using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vb.Base.Response;
using Vb.Business.Features.Addresses.Commands.Create;
using Vb.Business.Features.Addresses.Commands.Delete;
using Vb.Business.Features.Addresses.Commands.Update;
using Vb.Business.Features.Addresses.Queries.GetAll;
using Vb.Business.Features.Addresses.Queries.GetById;
using Vb.Business.Features.Addresses.Queries.GetByParameter;
using Vb.Schema;

namespace VbApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AddressesController : ControllerBase
{
    private readonly IMediator mediator;
    public AddressesController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    [HttpGet]
    public async Task<ApiResponse<List<AddressResponse>>> GetByParameter(int customerId, string? county, string? postalCode)
    {
        var operation = new GetAddressByParameterQuery(customerId, county, postalCode);
        var result = await mediator.Send(operation);
        return result;
    }
    
    //[HttpGet]
    //public async Task<ApiResponse<List<AddressResponse>>> Get()
    //{
    //    var operation = new GetAllAddressesQuery();
    //    var result = await mediator.Send(operation);
    //    return result;
    //}

    [HttpGet("{id}")]
    public async Task<ApiResponse<AddressResponse>> Get(int id)
    {
        var operation = new GetAddressByIdQuery(id);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPost]
    public async Task<ApiResponse<AddressResponse>> Post([FromBody] AddressRequest request)
    {
        var operation = new CreateAddressCommand(request);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPut("{id}")]
    public async Task<ApiResponse> Put(int id, [FromBody] AddressRequest request)
    {
        var operation = new UpdateAddressCommand(id, request);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpDelete("{id}")]
    public async Task<ApiResponse> Delete(int id)
    {
        var operation = new DeleteAddressCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }
}