using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parameter.API.DTOs.TaxParameter;
using Parameter.API.Models;
using Parameter.API.Models.Entities;

namespace Parameter.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class TaxParametersController(ParameterDbContext _context , IMapper _mapper) : ControllerBase
    {
        // GET: api/TaxParameters
        [HttpGet]

        public async Task<ActionResult<IEnumerable<GetTaxParameterDto>>> GetAllTaxParameters()
        {
            var taxParams = await _context.TaxParameters.ToListAsync();

            return Ok(_mapper.Map<List<GetTaxParameterDto>>(taxParams));
        }

        // GET: api/TaxParameters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetTaxParameterDto>> GetByTaxParameterId(int id)
        {
           var taxParam = await _context.TaxParameters.FindAsync(id);
           if (taxParam == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<GetTaxParameterDto>(taxParam));
        }

        // POST : api/TaxParameters
        [HttpPost]

        public async Task<ActionResult> CreateTaxParameter(CreateTaxParameterDto dto)
        {
            var taxParam = _mapper.Map<TaxParameter>(dto);
            _context.TaxParameters.Add(taxParam);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetByTaxParameterId), new { id = taxParam.Id }, _mapper.Map<GetTaxParameterDto>(taxParam));
            // bu return eklenen kayıdın ekranda görünmesini sağlar GetByTaxParameterId fonk sayesinde
        }

        // PUT : api/TaxParameters/5
        [HttpPut("{id}")]

        public async Task<ActionResult> UpdateTaxParameter(int id ,UpdateTaxParameterDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }
           var taxParam = await _context.TaxParameters.FindAsync(id);

            if (taxParam == null)
            {
                return NotFound();
            }

            _mapper.Map(dto, taxParam); //Bu işlem, DTO’daki verileri alır ve veritabanından gelen taxParam nesnesinin içine aktarır
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE : api/TaxParameters/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTaxParameter(int id)
        {
            var taxParam = await _context.TaxParameters.FindAsync(id);
            if (taxParam == null)
            {
                return NotFound();
            }

            _context.TaxParameters.Remove(taxParam);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
