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
    [Route("batch-management")]
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

        [HttpGet("{id:guid}")]
        [SwaggerOperation(Summary = "Get batch by id")]
        [ProducesResponseType(typeof(BatchResource), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetBatchByIdQuery(id));
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
            var id = await _mediator.Send(cmd);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpPut("{id:guid}")]
        [SwaggerOperation(Summary = "Update an existing batch")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateBatchDto dto)
        {
            var cmd = new UpdateBatchCommand(
                id,
                dto.Name,
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

        [HttpDelete("{id:guid}")]
        [SwaggerOperation(Summary = "Delete a batch")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteBatchCommand(id));
            return NoContent();
        }
    }
}

