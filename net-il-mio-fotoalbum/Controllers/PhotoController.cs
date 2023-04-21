using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using net_il_mio_fotoalbum.Models;
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

			if (photos == null) 
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

	}
}