using Microsoft.AspNetCore.Mvc;
using Shared.Abstractions;
using Parameter.API.DTOs.IncomeTaxBracket;
using Parameter.API.Models.Entities;
using Parameter.API.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Parameter.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IncomeTaxBracketsController : ControllerBase
    {
        private readonly ParameterDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICrudService<IncomeTaxBracket, Guid, GetIncomeTaxBracketDto, CreateIncomeTaxBracketDto, UpdateIncomeTaxBracketDto> _service;

        public IncomeTaxBracketsController(
            ParameterDbContext context,
            IMapper mapper,
            ICrudService<IncomeTaxBracket, Guid, GetIncomeTaxBracketDto, CreateIncomeTaxBracketDto, UpdateIncomeTaxBracketDto> service)
        {
            _context = context;
            _mapper = mapper;
            _service = service;
        }

        // --- CRUD ---

        // GET: api/IncomeTaxBrackets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetIncomeTaxBracketDto>>> GetAll(CancellationToken ct)
            => Ok(await _service.GetAllAsync(ct));


        // GET: api/IncomeTaxBrackets/{id}
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GetIncomeTaxBracketDto>> GetById(Guid id, CancellationToken ct)
            => (await _service.GetByIdAsync(id, ct)) is { } dto ? Ok(dto) : NotFound();


        // POST: api/IncomeTaxBrackets
        [HttpPost]
        public async Task<ActionResult<GetIncomeTaxBracketDto>> Create([FromBody] CreateIncomeTaxBracketDto dto, CancellationToken ct)
        {
            var created = await _service.CreateAsync(dto, ct);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }



        // PUT: api/IncomeTaxBrackets/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateIncomeTaxBracketDto dto, CancellationToken ct)
        {
            if (id != dto.Id) return BadRequest("Route id ile body id aynı olmalı.");
            var updated = await _service.UpdateAsync(id, dto, ct);
            return updated is null ? NotFound() : NoContent();
        }

        // DELETE: api/IncomeTaxBrackets/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
            => await _service.DeleteAsync(id, ct) ? NoContent() : NotFound();

        // --- READ-ONLY EK UÇLAR ---

        // GET: api/IncomeTaxBrackets/active?onDate=2025-01-01
        [HttpGet("active")]
        public async Task<ActionResult<List<GetIncomeTaxBracketDto>>> GetActive([FromQuery] DateTime? onDate, CancellationToken ct)
        {
            // 1) Tarihi al (yoksa bugünkü tarih)
            var date = (onDate ?? DateTime.Today).Date;

            // 2) Listeyi getir
            var list = await _context.IncomeTaxBrackets
                .Where(b => b.StartDate <= date && (b.EndDate == null || b.EndDate >= date))
                .OrderBy(b => b.LowerLimit)
                .ProjectTo<GetIncomeTaxBracketDto>(_mapper.ConfigurationProvider)
                .ToListAsync(ct);

            // 3) Eğer liste boşsa fallback (bugünkü tarihe bak)
            if (list.Count == 0 && onDate.HasValue)
            {
                var today = DateTime.Today.Date;
                list = await _context.IncomeTaxBrackets
                    .Where(b => b.StartDate <= today && (b.EndDate == null || b.EndDate >= today))
                    .OrderBy(b => b.LowerLimit)
                    .ProjectTo<GetIncomeTaxBracketDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(ct);
            }

            return Ok(list);
        }


        // GET: api/IncomeTaxBrackets/find?income=500000&onDate=2025-01-01
        [HttpGet("find")]
        public async Task<ActionResult<GetIncomeTaxBracketDto>> Find([FromQuery] decimal income, [FromQuery] DateTime? onDate, CancellationToken ct)
        {
            var date = (onDate ?? DateTime.Today).Date;

            var item = await _context.IncomeTaxBrackets
                .Where(b => b.StartDate <= date && (b.EndDate == null || b.EndDate >= date)
                            && income >= b.LowerLimit && (b.UpperLimit == null || income <= b.UpperLimit))
                .OrderBy(b => b.LowerLimit)
                .ProjectTo<GetIncomeTaxBracketDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(ct);

            return item is null ? NotFound() : Ok(item);
        }
    }
}
