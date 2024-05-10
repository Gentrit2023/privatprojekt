using FederataFutbollit.Data;
using FederataFutbollit.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FederataFutbollit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StafiController : ControllerBase
    {
        private readonly DataContext _context;
        public StafiController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]

        public async Task<ActionResult<List<Stafi>>> GetAllStafi()
        {
            var stafi = await _context.Stafi.ToListAsync();

            return Ok(stafi);

        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Stafi>> GetStafi(int id)
        {
            var stafi = await _context.Stafi.FindAsync(id);
            if (stafi is null)
                return NotFound("Stafi nuk u gjet");

            return Ok(stafi);

        }


            [HttpPost]
            public async Task<ActionResult<List<Stafi>>> AddStafi(Stafi stafi)
            { 
               _context.Stafi.Add(stafi);
                await _context.SaveChangesAsync();
               return Ok(await _context.Stafi.ToListAsync());

            }


         [HttpPut("{id}")]

        public async Task<ActionResult<List<Stafi>>> UpdateStafi(Stafi updatedStafi)
            {
                var dbStafi = await _context.Stafi.FindAsync(updatedStafi.Id);
                if (dbStafi is null)
                    return NotFound("Stafi nuk u gjet");

               dbStafi.Emri = updatedStafi.Emri;
                dbStafi.Mbiemri = updatedStafi.Mbiemri;
                dbStafi.Pozita = updatedStafi.Pozita;
                dbStafi.Paga = updatedStafi.Paga;
               dbStafi.Email = updatedStafi.Email;
                dbStafi.Telefoni = updatedStafi.Telefoni;

                await _context.SaveChangesAsync();



                return Ok(await _context.Stafi.ToListAsync());

            }


        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Stafi>>> DeleteStafi(int id)
            {
               var dbStafi = await _context.Stafi.FindAsync(id);
                if (dbStafi is null)
                    return NotFound("Stafi nuk u gjet");

               _context.Stafi.Remove(dbStafi);
                await _context.SaveChangesAsync();



                return Ok(await _context.Stafi.ToListAsync());

             }

        }
    }

