using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly PhotoContext _context;

        public PhotoController(PhotoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetPhotos([FromQuery] string? title)
        {
            var photos = _context.Photos
                 .Where(f => title == null || f.Title.ToLower().Contains(title.ToLower()))
                 .Where(f => f.Visible).ToList();

            return Ok(photos);
        }

        [HttpGet("{id}")]
        public IActionResult GetPhoto(int id)
        {
            var photo = _context.Photos.FirstOrDefault(p => p.Id == id);

            if (photo is null)
            {
                return NotFound();
            }

            return Ok(photo);
        }

        [HttpPost]
        public IActionResult CreateMessage(Message message)
        {
            _context.Messages.Add(message);
            _context.SaveChanges();

            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult PutPhoto(int id, [FromBody] Photo photo)
        {
            var savedPhoto = _context.Photos.FirstOrDefault(p => p.Id == id);

            if (savedPhoto is null)
            {
                return NotFound();
            }

            savedPhoto.Title = photo.Title;
            savedPhoto.Description = photo.Description;
            savedPhoto.Image = photo.Image;

            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePhoto(int id)
        {
            var savedPhoto = _context.Photos.FirstOrDefault(p => p.Id == id);

            if (savedPhoto is null)
            {
                return NotFound();
            }

            _context.Photos.Remove(savedPhoto);
            _context.SaveChanges();

            return Ok();
        }


    }
}
