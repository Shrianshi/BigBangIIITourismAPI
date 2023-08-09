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
    public class AgentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AgentController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult Get()
        {
            var pat = _context.agents.ToList();
            return Ok(pat);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("Requirements")]
        public IActionResult GetReq()
        {

            var pat = _context.agents
                .Select(s => new agentview
                {
                    AgentName = s.AgentName,
                    Country = s.Country,
                    City = s.City,
                   
                }).ToList();

            return Ok(pat);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var pat = _context.agents.Find(id);

            if (pat == null)
            {
                return NotFound();
            }

            return Ok(pat);
        }

        
        [HttpPost]
        public IActionResult Post([FromBody] Agent ag)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.agents.Add(ag);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = ag.Agent_id }, ag);
        }

        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAgent(int id, Agent agent)
        {
            // Find the agent with the specified id in the database
            var existingAgent = await _context.agents.FindAsync(id);

            if (existingAgent == null)
            {
                return NotFound();
            }

            // Update agent properties
            existingAgent.AgentName = agent.AgentName;
            existingAgent.Email = agent.Email;
            existingAgent.Phone = agent.Phone;
            existingAgent.City = agent.City;
            existingAgent.Country = agent.Country;

            // Check if the status has changed
            if (existingAgent.UserId != null && agent.UserId != null && existingAgent.User.Status != agent.User.Status)
            {
                // Update the status in the Users table based on the userId
                var user = await _context.users.FindAsync(existingAgent.User.Status);
                if (user != null)
                {
                    user.Status = agent.User.Status;
                    _context.Entry(user).State = EntityState.Modified;
                }
            }

            // Save changes to the database
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var ag = _context.agents.Find(id);

            if (ag == null)
            {
                return NotFound();
            }

            _context.agents.Remove(ag);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
