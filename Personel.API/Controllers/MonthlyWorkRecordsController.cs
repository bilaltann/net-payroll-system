using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Personel.API.DTOs.MonthlyWorkRecord; // Upsert/Update için İÇ DTO'lar
using Personel.API.Models;
using Personel.API.Models.Entities;
using Shared.Abstractions;
using Shared.Contracts.Personel;  //// <-- READ DTO buradan

namespace Personel.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MonthlyWorkRecordsController : ControllerBase
    {
        private readonly ICrudService<MonthlyWorkRecord, Guid, GetMonthlyWorkRecordDto, UpsertMonthlyWorkRecordDto, UpdateMonthlyWorkRecordDto> _svc;
        private readonly PersonelDbContext _ctx;
        private readonly IMapper _mapper;

        public MonthlyWorkRecordsController(
            ICrudService<MonthlyWorkRecord, Guid, GetMonthlyWorkRecordDto, UpsertMonthlyWorkRecordDto, UpdateMonthlyWorkRecordDto> svc,
            PersonelDbContext ctx,
            IMapper mapper)
        {
            _svc = svc;
            _ctx = ctx;
            _mapper = mapper;
        }

        // ======= Generic CRUD =======

        // GET: api/MonthlyWorkRecords
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetMonthlyWorkRecordDto>>> GetAll(CancellationToken ct)
            => Ok(await _svc.GetAllAsync(ct));

        // GET: api/MonthlyWorkRecords/{id}
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GetMonthlyWorkRecordDto>> GetById(Guid id, CancellationToken ct)
            => (await _svc.GetByIdAsync(id, ct)) is { } dto ? Ok(dto) : NotFound();

        // POST: api/MonthlyWorkRecords
        [HttpPost]
        public async Task<ActionResult<GetMonthlyWorkRecordDto>> Create([FromBody] UpsertMonthlyWorkRecordDto dto, CancellationToken ct)
        {
            var created = await _svc.CreateAsync(dto, ct);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/MonthlyWorkRecords/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateMonthlyWorkRecordDto dto, CancellationToken ct)
        {
            if (id != dto.Id) return BadRequest("Route id ile body id aynı olmalı.");
            var updated = await _svc.UpdateAsync(id, dto, ct);
            return updated is null ? NotFound() : NoContent();
        }

        // DELETE: api/MonthlyWorkRecords/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
            => await _svc.DeleteAsync(id, ct) ? NoContent() : NotFound();


        // ======= Özel İşlem: Upsert =======
        // POST: api/MonthlyWorkRecords/upsert
        [HttpPost("upsert")]
        public async Task<ActionResult<GetMonthlyWorkRecordDto>> Upsert([FromBody] UpsertMonthlyWorkRecordDto dto, CancellationToken ct)
        {
            // Aynı (EmployeeId, Year, Month) var mı?
            var existing = await _ctx.MonthlyWorkRecords
                .FirstOrDefaultAsync(x =>
                    x.EmployeeId == dto.EmployeeId &&
                    x.Year == dto.Year &&
                    x.Month == dto.Month, ct);

            if (existing is null)
            {
                // Yoksa create
                var entity = _mapper.Map<MonthlyWorkRecord>(dto);
                entity.Id = Guid.NewGuid();
                _ctx.MonthlyWorkRecords.Add(entity);
                await _ctx.SaveChangesAsync(ct);

                var result = _mapper.Map<GetMonthlyWorkRecordDto>(entity);
                return CreatedAtAction(nameof(GetById), new { id = entity.Id }, result);
            }
            else
            {
                // Varsa update
                existing.WorkDays = dto.WorkDays;
                existing.OvertimeHours = dto.OvertimeHours;
                await _ctx.SaveChangesAsync(ct);

                var result = _mapper.Map<GetMonthlyWorkRecordDto>(existing);
                return Ok(result);
            }
        }

        // İsteğe bağlı: by-key ile sorgulama
        // GET: api/MonthlyWorkRecords/by-key?employeeId=...&year=2025&month=7
        [HttpGet("by-key")]
        public async Task<ActionResult<GetMonthlyWorkRecordDto>> GetByKey([FromQuery] Guid employeeId, [FromQuery] int year, [FromQuery] int month, CancellationToken ct)
        {
            var rec = await _ctx.MonthlyWorkRecords
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.EmployeeId == employeeId && x.Year == year && x.Month == month, ct);

            return rec is null ? NotFound() : Ok(_mapper.Map<GetMonthlyWorkRecordDto>(rec));
        }
    }
}
