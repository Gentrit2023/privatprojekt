using FederataFutbollit.Data;
using FederataFutbollit.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FederataFutbollit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperligaController : ControllerBase
    {
        private readonly DataContext _context;

        public SuperligaController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Superliga>>> GetSuperligat()
        {
            var superligat = await _context.Superligat.ToListAsync();
            return Ok(superligat);

        }

        [HttpGet("{id}")]

        public async Task<ActionResult<Superliga>> GetSuperliga(int id)
        {
            var superliga = await _context.Superligat.FindAsync(id);
            if (superliga is null)
                return NotFound("Superliga nuk u gjet");
            return Ok(superliga);

        }


        [HttpPost]

        public async Task<ActionResult<List<Superliga>>> AddSuperliga(Superliga super)
        {
            _context.Superligat.Add(super);
            await _context.SaveChangesAsync();
            return Ok(await _context.Superligat.ToListAsync());

        }

        [HttpPut("{id}")]

        public async Task<ActionResult<List<Superliga>>> UpdateSuperliga(Superliga super)
        {
            var dbSuperliga = await _context.Superligat.FindAsync(super.Id);
            if (dbSuperliga is null)
                return NotFound("Superliga nuk u gjet");

            dbSuperliga.Emri = super.Emri;
            dbSuperliga.Sponzori = super.Sponzori;
            dbSuperliga.NumriSkuadrave = super.NumriSkuadrave;

            await _context.SaveChangesAsync();

            return Ok(await _context.Superligat.ToListAsync());

        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<List<Superliga>>> DeleteSuperliga(int id)
        {
            var dbSuperliga = await _context.Superligat.FindAsync(id);
            if (dbSuperliga is null)
                return NotFound("Superliga nuk u gjet");

            _context.Superligat.Remove(dbSuperliga);

            await _context.SaveChangesAsync();

            return Ok(await _context.Superligat.ToListAsync());

        }
    }
}
