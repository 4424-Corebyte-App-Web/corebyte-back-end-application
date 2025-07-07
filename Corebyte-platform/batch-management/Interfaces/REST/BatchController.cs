using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Corebyte_platform.batch_management.Application.Infernal.CommandServices;
using Corebyte_platform.batch_management.Application.Infernal.QueryServices;
using Corebyte_platform.batch_management.Interfaces.REST.Resources;

namespace Corebyte_platform.batch_management.Interfaces.REST.Controllers
{
    [ApiController]
    [Route("api/v1/batch-management")]
    public class BatchController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BatchController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "List all batches")]
        [ProducesResponseType(typeof(IEnumerable<BatchResource>), 200)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new ListBatchesQuery());
            return Ok(result);
        }

        [HttpGet("api/v1/{name}")]
        [SwaggerOperation(Summary = "Get batch by name")]
        [ProducesResponseType(typeof(BatchResource), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetByName(string name)
        {
            var result = await _mediator.Send(new GetBatchByIdQuery(name));
            return Ok(result);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new batch")]
        [ProducesResponseType(typeof(Guid), 201)]
        public async Task<IActionResult> Create([FromBody] CreateBatchDto dto)
        {
            var cmd = new CreateBatchCommand(
                dto.Name,
                dto.Type,
                dto.Status,
                dto.Temperature,
                dto.Amount,
                dto.Total,
                dto.Date,
                dto.NLote
            );
            var name = await _mediator.Send(cmd);
            return CreatedAtAction(nameof(GetByName), new { name }, name);
        }
        //name
        [HttpPut("api/v1/{name}")]
        [SwaggerOperation(Summary = "Update an existing batch by name")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(string name, [FromBody] UpdateBatchDto dto)
        {
            var cmd = new UpdateBatchCommand(
                name,                    // Current name (used to find the batch)
                dto.Name,               // New name (can be the same as current)
                dto.Type,
                dto.Status,
                dto.Temperature,
                dto.Amount,
                dto.Total,
                dto.Date,
                dto.NLote
            );
            await _mediator.Send(cmd);
            return NoContent();
        }

        [HttpDelete("api/v1/{name}")]
        [SwaggerOperation(Summary = "Delete a batch by name")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Delete(string name)
        {
            await _mediator.Send(new DeleteBatchCommand(name));
            return NoContent();
        }
    }
}