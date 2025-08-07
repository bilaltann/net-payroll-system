using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parameter.API.DTOs.IncomeTaxBracket;
using Parameter.API.Models;
using Parameter.API.Models.Entities;

namespace Parameter.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IncomeTaxBracketsController(ParameterDbContext _context , IMapper _mapper) : ControllerBase
    {

        // GET: api/IncomeTaxBrackets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetIncomeTaxBracketDto>>> GetAllIncomeTaxBrackets()
        {
           var brackets = await  _context.IncomeTaxBrackets.ToListAsync();

            return Ok(_mapper.Map<List<GetIncomeTaxBracketDto>>(brackets));
        }


        // GET: api/IncomeTaxBrackets/5
        [HttpGet("{id}")]  

        public async Task<ActionResult<GetIncomeTaxBracketDto>> GetByIncomeTaxBracketId(int id)
        {
            var bracket = await _context.IncomeTaxBrackets.FindAsync(id);

            if (bracket == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<GetIncomeTaxBracketDto>(bracket));
        }

        // POST
        [HttpPost]

        public async Task<IActionResult> CreateIncomeTaxBracket(CreateIncomeTaxBracketDto dto)
        {
            var bracket = _mapper.Map<IncomeTaxBracket>(dto);
            _context.IncomeTaxBrackets.Add(bracket);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetByIncomeTaxBracketId), new { id = bracket.Id },
                        _mapper.Map<GetIncomeTaxBracketDto>(bracket));
        }


        // PUT 
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateIncomeTaxBracket(int id , UpdateIncomeTaxBracketDto dto)
        {
            if (id !=dto.Id)
            {
                return BadRequest();
            }

            var bracket = await _context.IncomeTaxBrackets.FindAsync(id);
            if (bracket == null)
            {
                return NotFound();
            }

            _mapper.Map(dto, bracket);
            await _context.SaveChangesAsync();
            return NoContent();

        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteIncomeTaxBracket(int id)
        {
            var bracket = await _context.IncomeTaxBrackets.FindAsync(id);
            if (bracket == null) return NotFound();

            _context.IncomeTaxBrackets.Remove(bracket);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
