using BigBangIII_Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BigBangIII_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public PackageController(ApplicationDbContext context,IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Packages>>> GetEmployees()
        {
            return await _context.packages
                .Select(x => new Packages()
                {
                    P_id = x.P_id,
                    P_Name = x.P_Name,
                    Desc = x.Desc,
                    Pricing=x.Pricing,
                    Food_Details=x.Food_Details,
                    Acc_details=x.Acc_details,
                    ImageName = x.ImageName,
                    ImageSrc = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, x.ImageName)
                })
                .ToListAsync();
        }



        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var pat = _context.packages.Find(id);

            if (pat == null)
            {
                return NotFound();
            }

            return Ok(pat);
        }

        //[Authorize(Roles = "agent")]
        [HttpPost]
        public async Task<ActionResult<Packages>> Post([FromForm] Packages pat)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //_context.packages.Add(pat);
            //_context.SaveChanges();

            //return CreatedAtAction(nameof(Get), new { id = pat.P_id }, pat);
            pat.ImageName = await SaveImage(pat.ImageFile);
            _context.packages.Add(pat);
            await _context.SaveChangesAsync();
            return StatusCode(201);
        }

        //[Authorize(Roles = "admin,agent")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Packages pat)
        {
            //if (id != pat.P_id)
            //{
            //    return BadRequest();
            //}

            //_context.Update(pat);
            //_context.SaveChanges();

            //return NoContent();
            if(id!=pat.P_id)
            {
                return BadRequest();
            }
            if (pat.ImageFile != null)
            {
                DeleteImage(pat.ImageName);
                pat.ImageName = await SaveImage(pat.ImageFile);
            }

            _context.Entry(pat).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if (!PackageModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        //[Authorize(Roles = "admin,agent")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var pat = _context.packages.Find(id);

            if (pat == null)
            {
                return NotFound();
            }
            DeleteImage(pat.ImageName);

            _context.packages.Remove(pat);
            _context.SaveChanges();

            return NoContent();
        }
        private bool PackageModelExists(int id)
        {
            return _context.packages.Any(e=>e.P_id == id);
        }
        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName=imageName+DateTime.Now.ToString("yymmssfff")+Path.GetExtension(imageFile.FileName);
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", imageName);
            using(var fileStream=new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);

            }
            return imageName;
        }

        [NonAction]
        public void DeleteImage(string imageName)
        {
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", imageName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
        }
    }
}
