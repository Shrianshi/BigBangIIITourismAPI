using Azure.Storage.Blobs;
using BigBangIII_Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Threading.Tasks;


namespace BigBangIII_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ImageController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Images>>> GetUsers()
        {
            return await _context.images.ToListAsync();
        }
        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm] Images model)
        {
            if (model.Image != null)
            {
                // Connect to Azurite Blob Storage
                // string connectionString = "UseDevelopmentStorage=true"; // Azurite connection string
                //BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

                string connectionString = "DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;";
                BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

                BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("samples-workitems");

                // Generate a unique blob name
                string uniqueBlobName = Guid.NewGuid().ToString() + Path.GetExtension(model.Image.FileName);

                // Upload the image to Azure Blob Storage
                BlobClient blobClient = containerClient.GetBlobClient(uniqueBlobName);
                using (var stream = model.Image.OpenReadStream())
                {
                    await blobClient.UploadAsync(stream, true);
                }

                // Set the image path to the Blob Storage URL
                model.ImageUrl = blobClient.Uri.ToString();
            }

            _context.images.Add(model);
            await _context.SaveChangesAsync();

            return Ok();
        }



    }
}