using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Corebyte_platform.batch_management.Application.Infernal.CommandServices;
using Corebyte_platform.batch_management.Application.Infernal.QueryServices;
using Corebyte_platform.batch_management.Domain.Model.Commands;
using Corebyte_platform.batch_management.Domain.Model.Queries;
using Corebyte_platform.batch_management.Interfaces.REST.Resources;
using Corebyte_platform.batch_management.Interfaces.REST.Transform;
using System;

namespace Corebyte_platform.batch_management.Interfaces.REST.Controllers
{
    /// <summary>
    /// API Controller for Batch Management operations
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class BatchController : ControllerBase
    {
        private readonly IBatchCommandService _commandService;
        private readonly IBatchQueryService _queryService;
        private readonly ILogger<BatchController> _logger;

        /// <summary>
        /// Constructor for BatchController
        /// </summary>
        /// <param name="commandService">Service for batch command operations</param>
        /// <param name="queryService">Service for batch query operations</param>
        /// <param name="logger">Logger for the controller</param>
        public BatchController(
            IBatchCommandService commandService,
            IBatchQueryService queryService,
            ILogger<BatchController> logger)
        {
            _commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));
            _queryService = queryService ?? throw new ArgumentNullException(nameof(queryService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Get all batches with optional filtering
        /// </summary>
        /// <param name="filter">Filter criteria for batches</param>
        /// <returns>List of batches matching filter criteria</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BatchResource>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<BatchResource>>> GetBatches([FromQuery] BatchFilterResource filter)
        {
            _logger.LogInformation("Getting batches with filter: {Filter}", filter);
            var query = filter.ToQuery();
            var batches = await _queryService.GetBatchesAsync(query);
            return Ok(batches.ToResourceList());
        }

        /// <summary>
        /// Get a specific batch by ID
        /// </summary>
        /// <param name="id">The batch ID</param>
        /// <returns>The batch if found</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BatchResource), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BatchResource>> GetBatch(string id)
        {
            _logger.LogInformation("Getting batch with ID: {Id}", id);
            var query = new GetBatchQuery(id);
            var batch = await _queryService.GetBatchAsync(query);
            
            if (batch == null)
            {
                throw new InvalidOperationException($"Batch with ID {id} not found");
            }
            
            return Ok(batch.ToResource());
        }

        /// <summary>
        /// Get batches by lot number
        /// </summary>
        /// <param name="nlote">The lot number</param>
        /// <returns>List of batches with the specified lot number</returns>
        [HttpGet("lot/{nlote}")]
        [ProducesResponseType(typeof(IEnumerable<BatchResource>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<BatchResource>>> GetBatchesByLot(string nlote)
        {
            _logger.LogInformation("Getting batches with lot number: {NLote}", nlote);
            var query = new GetBatchesByLotQuery(nlote);
            var batches = await _queryService.GetBatchesByLotAsync(query);
            return Ok(batches.ToResourceList());
        }

        /// <summary>
        /// Create a new batch
        /// </summary>
        /// <param name="resource">The batch creation data</param>
        /// <returns>The ID of the created batch</returns>
        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> CreateBatch([FromBody] CreateBatchResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Creating new batch: {Name}", resource.Name);
            var command = resource.ToCommand();
            var batchId = await _commandService.CreateBatchAsync(command);
            return CreatedAtAction(nameof(GetBatch), new { id = batchId }, batchId);
        }

        /// <summary>
        /// Update an existing batch
        /// </summary>
        /// <param name="id">The batch ID</param>
        /// <param name="resource">The batch update data</param>
        /// <returns>No content if successful</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateBatch(string id, [FromBody] UpdateBatchResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Updating batch with ID: {Id}", id);
            
            // The service layer will throw InvalidOperationException if batch not found
            var command = resource.ToCommand(id);
            await _commandService.UpdateBatchAsync(command);
            return NoContent();
        }

        /// <summary>
        /// Delete a batch
        /// </summary>
        /// <param name="id">The ID of the batch to delete</param>
        /// <returns>No content if successful</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteBatch(string id)
        {
            _logger.LogInformation("Deleting batch with ID: {Id}", id);
            
            // The service layer will throw InvalidOperationException if batch not found
            var command = new DeleteBatchCommand(id);
            await _commandService.DeleteBatchAsync(command);
            return NoContent();
        }
    }
}

