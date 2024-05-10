using FederataFutbollit.Data;
using FederataFutbollit.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FederataFutbollit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SelektoriController : ControllerBase
    {
        private readonly DataContext _context;

        public SelektoriController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Selektori>>> GetSelektort()
        {
            var selektort = await _context.Selektort.ToListAsync();
            return Ok(selektort);

        }

        [HttpGet("{id}")]

        public async Task<ActionResult<Selektori>> GetSelektori(int id)
        {
            var selektori = await _context.Selektort.FindAsync(id);
            if (selektori is null)
                return NotFound("selektori nuk u gjet");
            return Ok(selektori);

        }


        [HttpPost]

        public async Task<ActionResult<List<Selektori>>> AddSelektori(Selektori super)
        {
            _context.Selektort.Add(super);
            await _context.SaveChangesAsync();
            return Ok(await _context.Selektort.ToListAsync());

        }
        

        [HttpPut("{id}")]

        public async Task<ActionResult<List<Selektori>>> UpdateSelektori(Selektori super)
        {
            var dbSelektori = await _context.Selektort.FindAsync(super.Id);
            if (dbSelektori is null)
                return NotFound("Selektori nuk u gjet");

            dbSelektori.Emri = super.Emri;
            dbSelektori.Mbiemri = super.Mbiemri;
            dbSelektori.Mosha = super.Mosha;

            await _context.SaveChangesAsync();

            return Ok(await _context.Selektort.ToListAsync());

        }


        [HttpDelete("{id}")]

        public async Task<ActionResult<List<Superliga>>> DeleteSuperliga(int id)
        {
            var dbSelektori = await _context.Selektort.FindAsync(id);
            if (dbSelektori is null)
                return NotFound("Superliga nuk u gjet");

            _context.Selektort.Remove(dbSelektori);

            await _context.SaveChangesAsync();

            return Ok(await _context.Selektort.ToListAsync());

        }
    }
}
