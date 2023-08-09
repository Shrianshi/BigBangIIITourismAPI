using BigBangIII_Api.Models;
using BigBangIII_Api.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BigBangIII_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;

        //public UserController(ApplicationDbContext context)
        //{
        //    _context = context;
        //}

        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            //if (_context.users == null)
            //{
            //    return NotFound();
            //}
            //return await _context.users.ToListAsync();
            var users = await _userRepository.GetUsersAsync();
            return Ok(users);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id, [FromQuery] string role)
        {
            //if (_context.users == null)
            //{
            //    return NotFound();
            //}
            //var user = await _context.users.FindAsync(id);

            //if (user == null)
            //{
            //    return NotFound();
            //}

            //if (!string.IsNullOrEmpty(role) && user.Role != role)
            //{
            //    return NotFound(); // Return 404 if the user does not have the specified role
            //}

            //return user;
            var user= await _userRepository.GetUserAsync(id,role);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
            
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            await _userRepository.UpdateUserAsync(user);
            return Ok();
            //if (id != user.UserId)
            //{
            //    return BadRequest();
            //}

            //_context.Entry(user).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!UserExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            //return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            await _userRepository.CreateUserAsync(user);
            return Ok();
            //if (_context.users == null)
            //{
            //    return Problem("Entity set 'ProCatContext.Users'  is null.");
            //}
            //_context.users.Add(user);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            _userRepository?.DeleteUserAsync(id);
            return Ok();
            //if (_context.users == null)
            //{
            //    return NotFound();
            //}
            //var user = await _context.users.FindAsync(id);
            //if (user == null)
            //{
            //    return NotFound();
            //}

            //_context.users.Remove(user);
            //await _context.SaveChangesAsync();

            //return NoContent();
        }

        //private bool UserExists(int id)
        //{
        //    return (_context.users?.Any(e => e.UserId == id)).GetValueOrDefault();
        //}
    }
}
