using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using net_il_mio_fotoalbum.Models;
using System.Diagnostics;

namespace net_il_mio_fotoalbum.Controllers
{
	public class PhotoController : Controller
	{
		private readonly ILogger<PhotoController> _logger;

		public PhotoController(ILogger<PhotoController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			using var ctx = new PhotoContext();
			var photos = ctx.Photos.ToArray();
			return View(photos);
		}

        public IActionResult Detail(int id)
        {
			using var ctx = new PhotoContext();
            var photos = ctx.Photos.SingleOrDefault(p => p.Id == id);

			if (photos == null) 
			{ 
				return NotFound($"Photo with id {id} not found.");	
			}

            return View(photos);
        }

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Photo photo)
		{

			if (!ModelState.IsValid)
			{
				return View(photo);
			}
			using var ctx = new PhotoContext();
			ctx.Photos.Add(photo);	 
			ctx.SaveChanges();	
			return RedirectToAction("index");
		}

	}
}