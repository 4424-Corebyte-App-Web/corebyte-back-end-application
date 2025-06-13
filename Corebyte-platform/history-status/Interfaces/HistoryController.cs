namespace Corebyte_platform.history_status.Interfaces
{
    using Corebyte_platform.history_status.Domain.Services;
    using Corebyte_platform.history_status.Interfaces.REST.Resources;
    using Corebyte_platform.history_status.Interfaces.REST.Transform;
    using Corebyte_platform.history_status.Domain.Model.Queries;
    using Corebyte_platform.history_status.Domain.Exceptions;
    using Corebyte_platform.history_status.Domain.Model.Commands;
    using Microsoft.AspNetCore.Mvc;
    using System.Net.Mime;
    using Swashbuckle.AspNetCore.Annotations;

    /// <summary>
    /// History controller.
    /// </summary>
    /// <param name="favoriteSourceCommandService">The History Command Service</param>
    /// <param name="favoriteSourceQueryService">The History Query Service</param>
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Tags("Report History")]
    public class HistoryController(IHistoryCommandService historyCommandService, IHistoryQueryService historyQueryService) : ControllerBase
    {
        /// <summary>
        /// Gets all history records.
        /// </summary>
        /// <returns>
        /// A response as an action result containing the history records, or no content if no history records were found.
        /// </returns>
        [HttpGet]
        [SwaggerOperation(
            Summary = "Gets all history records",
            Description = "Returns all history records with pagination support",
            OperationId = "GetAllHistories")]
        [SwaggerResponse(200, "The history records were found", typeof(IEnumerable<HistoryResource>))]
        [SwaggerResponse(204, "No history records found")]
        public async Task<IActionResult> GetAllHistories()
        {
            var query = new GetAllHistoriesQuery();
            var histories = await historyQueryService.Handle(query);
            
            if (!histories.Any())
                return NoContent();
                
            var resources = histories.Select(HistoryResourceFromEntityAssembler.ToResourceFromEntity);
            return Ok(resources);
        }
        /// <summary>
        /// Creates a report history.
        /// </summary>
        /// <param name="resource">The report history data to create</param>
        /// <returns>
        /// A response as an action result containing the created report history, or a bad request if the report history was not created.
        /// </returns>
        [HttpPost]
        [SwaggerOperation(
        Summary = "Creates a report history",
        Description = "Creates a report history with a given customer, date, product,amount, total and status ",
        OperationId = "CreateHistory")]
        [SwaggerResponse(201, "The report history was created", typeof(HistoryResource))]
        [SwaggerResponse(400, "The report history was not created")]
        [SwaggerResponse(409, "A history with the same details already exists")]
        public async Task<ActionResult> CreateHistory([FromBody] CreateHistoryResource resource)
        {
            try
            {
                var createHistoryCommand = CreateHistoryCommandFromResourceAssembler.ToCommandFromResource(resource);
                var result = await historyCommandService.Handle(createHistoryCommand);
                return CreatedAtAction(nameof(GetHistoryById), new { id = result.Id }, HistoryResourceFromEntityAssembler.ToResourceFromEntity(result));
            }
            catch (DuplicateHistoryException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception)
            {
                return BadRequest("The report history was not created");
            }
        }
        /// <summary>
        /// Gets all report history by customer. 
        /// </summary>
        /// <param name="customer">The Customer from  the service provider</param>
        /// <returns>
        /// A response as an action result containing the history, or not found if no history were found.
        /// </returns>
        [HttpGet("customer/{customer}")]
        [SwaggerOperation(
            Summary = "Gets histories by customer",
            Description = "Returns all history records for a specific customer",
            OperationId = "GetHistoryByCustomer")]
        [SwaggerResponse(200, "The history records were found", typeof(IEnumerable<HistoryResource>))]
        [SwaggerResponse(404, "No history records found for the customer")]
        public async Task<IActionResult> GetHistoryByCustomer(string customer)
        {
            var query = new GetHistoryByCustomerQuery(customer);
            var histories = await historyQueryService.Handle(query);
            
            if (!histories.Any())
                return NotFound(new { message = $"No history records found for customer: {customer}" });
                
            var resources = histories.Select(HistoryResourceFromEntityAssembler.ToResourceFromEntity);
            return Ok(resources);
        }

        /// <summary>
        /// Deletes all history records for a specific id
        /// </summary>
        /// <param name="id">The idname to delete records for</param>
        /// <returns>Number of records deleted</returns>
        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Deletes history for a id",
            Description = "Deletes history associated with the specified id",
            OperationId = "DeleteHistoriesById")]
        [SwaggerResponse(200, "History was successfully deleted", typeof(HistoryResource))]
        [SwaggerResponse(404, "No history records found for the id")]
        public async Task<IActionResult> DeleteHistories([FromBody] DeleteHistoryResource resource)
        {
            var deleteHistoryCommand = DeleteHistoryCommandFromResourceAssembler.ToCommandFromResource(resource);
            try
            {
                var history= await historyCommandService.Handle(deleteHistoryCommand);
                if (history is null)
                {
                    return NotFound(new { message = "No history records found for the specified id" });
                }
                var historyResource=HistoryResourceFromEntityAssembler.ToResourceFromEntity(history);
                return Ok(historyResource);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the history" });
            }
        }

        /// <summary>
        /// Updates an existing history record by ID
        /// </summary>
        /// <param name="id">The ID of the history record to update</param>
        /// <param name="resource">The updated history data</param>
        /// <returns>The updated history record</returns>
        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Updates an existing history record by ID",
            Description = "Updates an existing history record with the specified ID",
            OperationId = "UpdateHistoryById")]
        [SwaggerResponse(200, "History record was updated successfully", typeof(HistoryResource))]
        [SwaggerResponse(400, "Invalid input data")]
        [SwaggerResponse(404, "No history records found for the id")]
        [SwaggerResponse(409, "A history record with the same customer, product and date already exists")]
        public async Task<IActionResult> UpdateHistory(int id, [FromBody] UpdateHistoryResource resource)
        {
            var updateHistoryCommand = UpdateHistoryCommandFromResourceAssembler.ToCommandFromResource(id, resource);
            var history = await historyCommandService.Handle(updateHistoryCommand);
            if (history is null)
            {
                return NotFound(new { message = "The history record was not found" });
            }
            var historyResource = HistoryResourceFromEntityAssembler.ToResourceFromEntity(history);
            return Ok(historyResource);
        }
        /// <summary>
        /// Gets a history by id. 
        /// </summary>
        /// <param name="id">The id of the history to get</param>
        /// <returns>
        /// A response as an action result containing the history, or not found if no history was found.
        /// </returns>
        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Gets a history by id",
            Description = "Gets a historyfor a given history identifier",
            OperationId = "GetHistoryById")]
        [SwaggerResponse(200, "The History was found", typeof(HistoryResource))]
        [SwaggerResponse(404, "The History was not found")]
        public async Task<ActionResult> GetHistoryById(int id)
        {
            var getHistoryByIdQuery = new GetHistoryByIdQuery(id);
            var result = await historyQueryService.Handle(getHistoryByIdQuery);
            if (result is null)
            {
                return NotFound("The History was not found");
            }
            var resource = HistoryResourceFromEntityAssembler.ToResourceFromEntity(result);
            return Ok(resource);
        }

        /// <summary>
        /// Gets a report history by product. 
        /// </summary>
        /// <param name="product">The product from  the news service provider</param>
        /// <returns>
        /// A response as an action result containing the history, or not found if no history was found.
        /// </returns>
        private async Task<ActionResult> GetHistoryByProduct(string product)
        {
            var getHistoryByProductQuery = new GetHistoryByProductQuery(product);
            var result = await historyQueryService.Handle(getHistoryByProductQuery);
            if (result is null)
            {
                return NotFound("The History was not found");
            }
            var resource = HistoryResourceFromEntityAssembler.ToResourceFromEntity(result);
            return Ok(resource);
        }

        /// <summary>
        /// Gets a report history by status. 
        /// </summary>
        /// <param name="status">The statusfrom  the news service provider</param>
        /// <returns>
        /// A response as an action result containing the history, or not found if no history was found.
        /// </returns>
        private async Task<ActionResult> GetHistoryByStatus(string status)
        {
            var getHistoryByStatusQuery = new GetHistoryByStatusQuery(status);
            var result = await historyQueryService.Handle(getHistoryByStatusQuery);
            if (result is null)
            {
                return NotFound("The History was not found");
            }
            var resource = HistoryResourceFromEntityAssembler.ToResourceFromEntity(result);
            return Ok(resource);
        }

        /// <summary>
        /// Gets a report history by amount and total. 
        /// </summary>
        /// <param name="amount">The amount from the news service provider</param>
        /// <param name="total">The titak from the news service provider</param>
        /// <returns>
        /// A response as an action result containing the history, or not found if no history was found.
        /// </returns>
        private async Task<ActionResult> GetHistoryByAmountAndTotal(int amount, double total)
        {
            var getHistoryByAmountAndTotalQuery = new GetHistoryByAmountAndTotalQuery(amount, total);
            var result = await historyQueryService.Handle(getHistoryByAmountAndTotalQuery);
            if (result is null)
            {
                return NotFound("The History was not found");
            }
            var resource = HistoryResourceFromEntityAssembler.ToResourceFromEntity(result);
            return Ok(resource);
        }
    }
}
