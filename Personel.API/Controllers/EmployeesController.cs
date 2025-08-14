using Microsoft.AspNetCore.Mvc;
using Personel.API.DTOs.Employee;
using Personel.API.Models.Entities;
using Shared.Abstractions;
using Shared.Contracts.Personel;
namespace Personel.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController(ICrudService<Employee, Guid, GetEmployeeDto, CreateEmployeeDto, UpdateEmployeeDto> _service) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetEmployeeDto>>> GetAll(CancellationToken ct)
            => Ok(await _service.GetAllAsync(ct));

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GetEmployeeDto>> GetById(Guid id, CancellationToken ct)
            => (await _service.GetByIdAsync(id, ct)) is { } dto ? Ok(dto) : NotFound();

        [HttpPost]
        public async Task<ActionResult<GetEmployeeDto>> Create([FromBody] CreateEmployeeDto dto, CancellationToken ct)
        {
            var created = await _service.CreateAsync(dto, ct);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateEmployeeDto dto, CancellationToken ct)
        {
            if (id != dto.Id) return BadRequest("Route id ile body id aynı olmalı.");
            var updated = await _service.UpdateAsync(id, dto, ct);
            return updated is null ? NotFound() : NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
            => await _service.DeleteAsync(id, ct) ? NoContent() : NotFound();


    



    }

}

