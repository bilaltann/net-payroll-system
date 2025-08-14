using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parameter.API.DTOs.TaxParameter;
using Parameter.API.Models;
using Parameter.API.Models.Entities;
using Parameter.API.Services.Interfaces;
using Shared.Abstractions;
using Shared.Enums;
using Shared.Events;
using Shared.Infrastructure;

namespace Parameter.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaxParametersController(ICrudService<TaxParameter, Guid, GetTaxParameterDto, CreateTaxParameterDto, UpdateTaxParameterDto> _service,
        ParameterDbContext _context, IPublishEndpoint _publishEndpoint, ITaxParameterKeyMapper _keyMapper) : ControllerBase
    {

        // GET : api/TaxParameters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetTaxParameterDto>>> GetAll(CancellationToken ct)
        => Ok(await _service.GetAllAsync(ct));


        // GET: api/TaxParameters/{id}
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GetTaxParameterDto>> GetById(Guid id, CancellationToken ct)
            => (await _service.GetByIdAsync(id, ct)) is { } dto ? Ok(dto) : NotFound();


        [HttpPost]
        public async Task<ActionResult<GetTaxParameterDto>> Create([FromBody] CreateTaxParameterDto dto,
            CancellationToken ct)
        {
            var created = await _service.CreateAsync(dto, ct);

            // oluşan entity okuyup eventi güncel değerle yayınla

            var entity = await _context.TaxParameters.AsNoTracking().
                FirstAsync(x => x.Id == created.Id, ct);

            await _publishEndpoint.Publish(new TaxParameterUpdatedEvent
            {
                Key = entity.Key,
                Value = entity.Value,
                Year = entity.StartDate.Year
            }, ct);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/TaxParameters/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTaxParameterDto dto,
            CancellationToken ct)
        {
            if (id != dto.Id)
                return BadRequest("Route id ile body id aynı olmalı.");

            var updated = await _service.UpdateAsync(id, dto, ct);
            if (updated is null)
                return NotFound();

            var entity = await _context.TaxParameters.AsNoTracking().FirstAsync(x => x.Id == id, ct);
            await _publishEndpoint.Publish(new TaxParameterUpdatedEvent
            {
                Key = entity.Key,
                Value = entity.Value,
                Year = entity.StartDate.Year
            }, ct);

            return NoContent();
        }



        // DELETE: api/TaxParameters/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
            => await _service.DeleteAsync(id, ct) ? NoContent() : NotFound();



        // Özel: isme göre (opsiyonel year filtresiyle) güncel değer
        // GET: api/TaxParameters/by-name/{name}?year=2025
        [HttpGet("by-name/{name}")]
        public async Task<IActionResult> GetByName(TaxParameterName name, [FromQuery] int? year,
            CancellationToken ct)
        {
            var key = _keyMapper.GetKey(name);

            var q = _context.TaxParameters.AsNoTracking().Where(p => p.Key == key);

            if (year.HasValue)
                q = q.Where(p => p.StartDate.Year <= year &&
                                 (!p.EndDate.HasValue || p.EndDate.Value.Year >= year));

            var parameter = await q.OrderByDescending(p => p.StartDate).FirstOrDefaultAsync(ct);
            return parameter is null ? NotFound() : Ok(parameter.Value);
        }


    }
}
