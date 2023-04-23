using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using net_il_mio_fotoalbum.Models;
using System.Data;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;

namespace net_il_mio_fotoalbum.Controllers
{
	public class PhotoController : Controller
	{
		private readonly ILogger<PhotoController> _logger;
		private readonly PhotoContext _context;

		public PhotoController(ILogger<PhotoController> logger, PhotoContext context)
		{
			_logger = logger;
			_context = context;
		}

		public IActionResult Index()
		{
			
			var photos = _context.Photos.ToArray();
			return View(photos);
		}

        public IActionResult Detail(int id)
        {
            var photos = _context.Photos.SingleOrDefault(p => p.Id == id);

			if (photos is null) 
			{ 
				return NotFound($"Photo with id {id} not found.");	
			}

            return View(photos);
        }

		public IActionResult Create()
		{
			var photo = new Photo
			{
				Image = "https://picsum.photos/200/300"
			};
			return View(photo);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Photo photo)
		{

			if (!ModelState.IsValid)
			{
				return View(photo);
			}

			_context.Photos.Add(photo);
			_context.SaveChanges();	
			return RedirectToAction("index");
		}

        public IActionResult Update(int id)
        {

          
            var photo = _context.Photos.FirstOrDefault(p => p.Id == id);

			if(photo is null)
			{
				return View("NotFound");
			}
          
			return View(photo);	
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Update(int id, Photo photo)
		{
			if (!ModelState.IsValid) 
			{ 
				return View(photo); 
			}
			var photoToUpdate = _context.Photos.FirstOrDefault(p => p.Id == id);

			if (photoToUpdate is null)
			{
				return View("NotFound");
			}

			photoToUpdate.Title = photo.Title;
			photoToUpdate.Description = photo.Description;
			photoToUpdate.ImageFile = photo.ImageFile;
			photoToUpdate.Visible = photo.Visible;

			_context.SaveChanges();
			return RedirectToAction("index");	
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Delete(int id)
		{
			var photoToDelete = _context.Photos.FirstOrDefault(p => p.Id == id);

			if (photoToDelete is null)
			{
				return View("NotFound");
			}

			_context.Photos.Remove(photoToDelete);
			_context.SaveChanges();

			return RedirectToAction("Index");
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}