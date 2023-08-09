using BigBangIII_Api.Models;
using BigBangIII_Api.Repository;
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
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _repository;
        public BookingController(IBookingRepository repos)
        {
            _repository = repos;
        }


        [HttpGet]
        public IActionResult Get()
        {
            var pat = _repository.GetBookings();
            return Ok(pat);
        }



        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var pat = _repository.GetBookingsById(id);

            //if (pat == null)
            //{
            //    return NotFound();
            //}

            return Ok(pat);
        }


        [HttpPost]
        public IActionResult Post([FromBody] Bookings ag)
        {
            _repository.AddBookings(ag);
            //return Ok();

            return CreatedAtAction(nameof(Get), new { id = ag.Book_Id }, ag);
        }

        //[Authorize(Roles = "traveller")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBookingst(int id, Bookings b)
        {
            // Find the agent with the specified id in the database
            if (id == null)
            {
                return BadRequest();
            }
            _repository.UpdateBookings(id, b);
            return Ok();
        }


        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repository.DeleteBookings(id);
            return Ok();
        }
    }
}
