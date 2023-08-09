using BigBangIII_Api.Models;
using BigBangIII_Api.Reqirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BigBangIII_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillingController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BillingController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var pat = _context.billings.ToList();
            return Ok(pat);
        }

      
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var pat = _context.billings.Find(id);

            if (pat == null)
            {
                return NotFound();
            }

            return Ok(pat);
        }


        [HttpPost]
        public IActionResult Post([FromBody] Billing ag)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.billings.Add(ag);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = ag.Bill_id }, ag);
        }

        

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var ag = _context.billings.Find(id);

            if (ag == null)
            {
                return NotFound();
            }

            _context.billings.Remove(ag);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
