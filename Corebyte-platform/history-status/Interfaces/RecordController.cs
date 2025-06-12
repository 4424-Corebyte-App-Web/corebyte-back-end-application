using Corebyte_platform.history_status.Domain.Exceptions;
using Corebyte_platform.history_status.Domain.Model.Commands;
using Corebyte_platform.history_status.Domain.Model.Queries;
using Corebyte_platform.history_status.Domain.Model.ValueObjects;
using Corebyte_platform.history_status.Domain.Services;
using Corebyte_platform.history_status.Interfaces.REST.Resources;
using Corebyte_platform.history_status.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace Corebyte_platform.history_status.Interfaces
{
    /// <summary>
    /// Record controller.
    /// </summary>
    /// <param name="recordCommandService">The Record Command Service</param>
    /// <param name="recordQueryService">The Record Query Service</param>
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Tags("Record")]
    public class RecordController(IRecordCommandService recordCommandService, IRecordQueryService recordQueryService):ControllerBase
    {
        /// <summary>
        /// Gets all records.
        /// </summary>
        /// <returns>
        /// A response as an action result containing the records, or no content if no records were found.
        /// </returns>
        [HttpGet]
        [SwaggerOperation(
            Summary = "Gets all recods",
            Description = "Returns all records with pagination support",
            OperationId = "GetAllRecords")]
        [SwaggerResponse(200, "The recods were found", typeof(IEnumerable<RecordResource>))]
        [SwaggerResponse(204, "No records found")]
        public async Task<IActionResult> GetAllRecords() {
            var query = new GetAllRecordQuery();
            var records = await recordQueryService.Handle(query);

            if (!records.Any()) return NoContent();
            var resources = records.Select(RecordResourceFromEntityAssembler.ToResourceFromEntity);
            return Ok(resources);
        }
        /// <summary>
        /// Creates a record.
        /// </summary>
        /// <param name="resource">The record data to create</param>
        /// <returns>
        /// A response as an action result containing the created record, or a bad request if the record was not created.
        /// </returns>
        [HttpPost]
        [SwaggerOperation(
        Summary = "Creates a record",
        Description = "Creates a record with a given customerId,type,year,product,batch,stock ",
        OperationId = "CreateHistory")]
        [SwaggerResponse(201, "The record was created", typeof(RecordResource))]
        [SwaggerResponse(400, "The record was not created")]
        [SwaggerResponse(409, "A record with the same details already exists")]
        public async Task<ActionResult> CreateRecord([FromBody] CreateRecordResource resource)
        {
            try
            {
                var createRecordCommand = CreateRecordCommandFromResourceAssembler.ToCommandFromResource(resource);
                var result = await recordCommandService.Handle(createRecordCommand);
                return CreatedAtAction(nameof(GetRecordById), new { id = result.Id }, RecordResourceFromEntityAssembler.ToResourceFromEntity(result));
            }
            catch (DuplicateRecordException ex) { 
                return Conflict(new { message = ex.Message });
            }
            catch (Exception) {
                return BadRequest("The record was not created");
            }
        }
        /// <summary>
        /// Gets records by customer.
        /// </summary>
        /// <param name="customerId">The customer ID to search for</param>
        /// <returns>
        /// A response as an action result containing the records, or not found if no records were found for the customer.
        /// </returns>
        [HttpGet("customerId/{customerId}")]
        [SwaggerOperation(
            Summary = "Gets records by customer",
            Description = "Returns all records for a specific customer",
            OperationId = "GetRecordsByCustomer")]
        [SwaggerResponse(200, "The records were found", typeof(IEnumerable<RecordResource>))]
        [SwaggerResponse(404, "No records found for the customer")]
        public async Task<IActionResult> GetRecordsByCustomerId(int customerId)
        {
            var customerIdObj = new CustomerId(customerId);
            var query = new GetRecordByCustomerIdQuery(customerIdObj);
            var records = await recordQueryService.Handle(query);
            
            if (!records.Any())
                return NotFound(new { message = $"No records found for customer: {customerId}" });
                
            var resources = records.Select(RecordResourceFromEntityAssembler.ToResourceFromEntity);
            return Ok(resources);
        }
        /// <summary>
        /// Deletes all records for a specific id.
        /// </summary>
        /// <param name="id">The id to delete records for</param>
        /// <returns>
        /// A response as an action result containing the number of records deleted, or not found if no records were found for the id.
        /// </returns>
        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Deletes all records for a id",
            Description = "Deletes all records associated with the specified id",
            OperationId = "DeleteRecordsById")]
        [SwaggerResponse(200, "Records were successfully deleted", typeof(ActionResult<int>))]
        [SwaggerResponse(404, "No records found for the id")]
        public async Task<IActionResult> DeleteRecordsById(int id)
        {
            var command = new DeleteRecordByIdCommand(id);
            var deletedCount = await recordCommandService.Handle(command);
            
            if (deletedCount == 0)
                return NotFound(new { message = $"No records found for id: {id}" });
                
            return Ok(new { 
                message = $"Successfully deleted {deletedCount} record(s) for id: {id}",
                deletedCount 
            });
        }
        /// <summary>
        /// Updates a record by ID.
        /// </summary>
        /// <param name="id">The ID of the record to update</param>
        /// <param name="resource">The record data to update</param>
        /// <returns>
        /// A response as an action result containing the updated record, or not found if no record was found for the ID.
        /// </returns>
        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Updates a record by ID",
            Description = "Updates an existing record with the provided data",
            OperationId = "UpdateRecordById")]
        [SwaggerResponse(200, "Record was updated successfully", typeof(RecordResource))]
        [SwaggerResponse(400, "Invalid input data")]
        [SwaggerResponse(404, "Record not found")]
        [SwaggerResponse(409, "A record with the same customerId, type, year, product and batch already exists")]
        public async Task<IActionResult> UpdateRecord(int id, [FromBody] UpdateRecordResource resource)
        {
            try
            {
                var customerId = new CustomerId(resource.CustomerId);
                var command = new UpdateRecordCommand(
                    id,
                    customerId,
                    resource.Type,
                    resource.Year,
                    resource.Product,
                    resource.Batch,
                    resource.Stock);

                var result = await recordCommandService.Handle(command);
                var resultResource = RecordResourceFromEntityAssembler.ToResourceFromEntity(result);
                
                return Ok(resultResource);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (DuplicateRecordException ex) { 
                return Conflict(new { message = ex.Message });
            }
            catch (Exception) {
                return BadRequest("The record was not updated");
            }
        }
        /// <summary>
        /// Gets a record by id.
        /// </summary>
        /// <param name="id">The id of the record to get</param>
        /// <returns>
        /// A response as an action result containing the record, or not found if no record was found for the id.
        /// </returns>
        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Gets a record by id",
            Description = "Gets a recordfor a given record identifier",
            OperationId = "GetRecordById")]
        [SwaggerResponse(200, "The Record was found", typeof(RecordResource))]
        [SwaggerResponse(404, "The Record was not found")]
        public async Task<IActionResult> GetRecordById(int id)
        {
            var getRecordByIdQuery = new GetRecordByIdQuery(id);
            var result = await recordQueryService.Handle(getRecordByIdQuery);
            if (result is null)
            {
                return NotFound("The Record was not found");
            }
            var resource = RecordResourceFromEntityAssembler.ToResourceFromEntity(result);
            return Ok(resource);
        }
        /// <summary>
        /// Gets a record by stock.
        /// </summary>
        /// <param name="stock">The stock of the record to get</param>
        /// <returns>
        /// A response as an action result containing the record, or not found if no record was found for the stock.
        /// </returns>
        [HttpGet("stock/{stock}")]
        [SwaggerOperation(
            Summary = "Gets a record by stock",
            Description = "Gets a recordfor a given record identifier",
            OperationId = "GetRecordByStock")]
        [SwaggerResponse(200, "The Record was found", typeof(RecordResource))]
        [SwaggerResponse(404, "The Record was not found")]
        public async Task<ActionResult>GetRecordByStock(int stock)
        {
            var getRecordByStockQuery = new GetRecordByStockQuery(stock);
            var result = await recordQueryService.Handle(getRecordByStockQuery);
            if (result is null)
            {
                return NotFound("The Record was not found");
            }
            var resource = RecordResourceFromEntityAssembler.ToResourceFromEntity(result);
            return Ok(resource);
        }
        /// <summary>
        /// Gets a record by type and product.
        /// </summary>
        /// <param name="type">The type of the record to get</param>
        /// <param name="product">The product of the record to get</param>
        /// <returns>
        /// A response as an action result containing the record, or not found if no record was found for the type and product.
        /// </returns>
        [HttpGet("type/{type}/product/{product}")]
        [SwaggerOperation(
            Summary = "Gets a record by type and product",
            Description = "Gets a recordfor a given record identifier",
            OperationId = "GetRecordByTypeAndProduct")]
        [SwaggerResponse(200, "The Record was found", typeof(RecordResource))]
        [SwaggerResponse(404, "The Record was not found")]
        public async Task<ActionResult>GetRecordByTypeAndProduct(string type, string product)
        {
            var getRecordByTypeAndProductQuery = new GetRecordByTypeAndProductQuery(type, product);
            var result = await recordQueryService.Handle(getRecordByTypeAndProductQuery);
            if (result is null)
            {
                return NotFound("The Record was not found");
            }
            var resource = RecordResourceFromEntityAssembler.ToResourceFromEntity(result);
            return Ok(resource);
        }
    }
}
