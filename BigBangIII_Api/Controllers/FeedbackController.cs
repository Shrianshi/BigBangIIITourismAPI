using BigBangIII_Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BigBangIII_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FeedbackController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult Get()
        {
            var pat = _context.feedbacks.ToList();
            return Ok(pat);
        }



        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var pat = _context.feedbacks.Find(id);

            if (pat == null)
            {
                return NotFound();
            }

            return Ok(pat);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Feedback ag)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.feedbacks.Add(ag);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = ag.Id }, ag);
        }

       

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var ag = _context.feedbacks.Find(id);

            if (ag == null)
            {
                return NotFound();
            }

            _context.feedbacks.Remove(ag);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
