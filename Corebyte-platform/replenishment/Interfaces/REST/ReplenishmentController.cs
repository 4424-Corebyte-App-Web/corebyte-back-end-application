using System.Net.Mime;
using Corebyte_platform.replenishment.Domain.Model.Commands;
using Corebyte_platform.replenishment.Domain.Model.Queries;
using Corebyte_platform.replenishment.Domain.Services;
using Corebyte_platform.Replenishment.Interfaces.REST.Resources;
using Corebyte_platform.Replenishment.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Corebyte_platform.replenishment.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Replenishment")]
[SwaggerTag("Available Replenishment Endpoints")]
public class ReplenishmentController(IReplenishmentQueryService replenishmentQueryService, IReplenishmentCommandService replenishmentCommandService): ControllerBase

{
    [HttpGet("{ReplenishmentId:int}")]
    [SwaggerOperation(
        Summary = "Get Replenishment by ID",
        Description = "Get Replenishment by ID",
        OperationId = "GetReplenishmentById"
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "The tutorial was successfully retrieved", typeof(ReplenishmentResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The Replenishment was not found")]

    public async Task<IActionResult> GetReplenishmentById(int ReplenishmentId)
    {
        var getReplenishmentByIdQuery = new GetReplenishmentByIdQuery(ReplenishmentId);
        var replenishment = await replenishmentQueryService.Handle(getReplenishmentByIdQuery);
        if (replenishment is null) return NotFound();
        var replenishmentResource = ReplenishmentResourceFromEntityAssembler.ToResourceFromEntity(replenishment);
        return Ok(replenishmentResource);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create a Replenishment", Description = "Create a Replenishment", OperationId = "CreateTutorial")]
    [SwaggerResponse(StatusCodes.Status201Created, "The Replenishment was successfully created", typeof(ReplenishmentResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The Replenishment was not created")]

    public async Task<IActionResult> CreateReplenishment([FromBody] CreateReplenishmentResource resource)
    {
        var createReplenishmentCommand = CreateReplenishmentCommandFromResourceAssembler.ToCommandFromResource(resource);
        var replenishment = await replenishmentCommandService.Handle(createReplenishmentCommand);
        if (replenishment is null) return BadRequest();
        var replenishmentResource = ReplenishmentResourceFromEntityAssembler.ToResourceFromEntity(replenishment);
        return CreatedAtAction(nameof(GetReplenishmentById), new { replenishmentId = replenishment.Id }, replenishmentResource);
    }
    
/*
    [HttpGet]
    [SwaggerOperation(Summary = "Get orders", Description = "Get orders", OperationId = "GetOrders")]
    [SwaggerResponse(StatusCodes.Status200OK, "The Orders were successfully retrieved",
        typeof(IEnumerable<ReplenishmentResource>))]

    public async Task<IActionResult> GetReplenishment()
    {
        var getAllOrdersQuery = new GetAllReplenishmentQuery();
        var ordersRequests = await replenishmentCommandService.Handle(getAllOrdersQuery);
        var replenishmentResources = Replenishment.Select(ReplenishmentResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(replenishmentResources);
        
    }
*/

    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete Replenishment",
        Description = "Delete Replenishment",
        OperationId = "DeleteReplenishment"
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "The Replenishment was deleted")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The Replenishment could not be deleted")]
    public async Task<IActionResult> DeleteReplenishment(int id)
    {
        var deleteReplenishmentCommand = new DeleteReplenishmentCommand(id);
        var result = await replenishmentCommandService.Handle(deleteReplenishmentCommand);
        if (result is null) return BadRequest();
        return Ok(ReplenishmentResourceFromEntityAssembler.ToResourceFromEntity(result));
    }
    /***/
    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Update Replenishment",
        Description = "Update Replenishment",
        OperationId = "UpdateReplenishment"
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "The Replenishment was updated")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The Replenishment could not be updated")]
    public async Task<IActionResult> UpdateReplenishment(int id, [FromBody] UpdateReplenishmentByIdResource updateReplenishmentByIdResource)
    {
        var updateReplenishmentByIdCommand = UpdateReplenishmentByIdCommandFromResourceAssembler.ToCommandFromResource(id, updateReplenishmentByIdResource);
        var result = await replenishmentCommandService.Handle(updateReplenishmentByIdCommand);
        if (result is null) return BadRequest();
        return Ok(ReplenishmentResourceFromEntityAssembler.ToResourceFromEntity(result));
    }
    
    
    //genera un getall de replenishment
    [HttpGet]
    [SwaggerOperation(Summary = "Get all Replenishments", Description = "Get all Replenishments", OperationId = "GetAllReplenishments")]
    [SwaggerResponse(StatusCodes.Status200OK, "The Replenishments were successfully retrieved", typeof(IEnumerable<ReplenishmentResource>))]
    public async Task<IActionResult> GetAllReplenishments()
    {
        var getAllReplenishmentsQuery = new GetAllReplenishmentQuery();
        var replenishments = await replenishmentQueryService.Handle(getAllReplenishmentsQuery);
        if (replenishments is null) return NotFound();
        var replenishmentResources = replenishments.Select(ReplenishmentResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(replenishmentResources);
    }
    
}