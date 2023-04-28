using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using net_il_mio_fotoalbum.Models;
using System.Data;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;

namespace net_il_mio_fotoalbum.Controllers
{
    [Authorize(Roles = "Admin,User")]
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
            var pizze = _context.Photos.ToArray();

            return View(pizze);
        }

        public IActionResult Message()
        {
            return View();
        }
        public IActionResult ApiIndex()
        {
            var pizze = _context.Photos.ToArray();
            return View(pizze);
        }

        public IActionResult ApiDetail()
        {
            return View();
        }



        public IActionResult Detail(int id)
        {
			var photo = _context.Photos
			  .Include(p => p.Categories)
			  .SingleOrDefault(p => p.Id == id);

			if (photo is null)
			{
				return View("NotFound", "Post not found.");
			}

			return View(photo);
		}


		

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
		{
			var formModel = new PhotoFormModel
			{
				Categories = _context.Categories.ToArray(),
			};
			return View(formModel);
		}

        [Authorize(Roles = "Admin")]
        [HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(PhotoFormModel form)
		{

			if (!ModelState.IsValid)
			{
				form.Categories = _context.Categories.ToArray();

				return View(form);
			}

			form.Photo.Categories = _context.Categories.Where(t => form.SelectedCategoryIds.Contains(t.Id)).ToList();

			form.SetImageFileFromFormFile();

			_context.Photos.Add(form.Photo);
			_context.SaveChanges();

			return RedirectToAction("Index");
		}

        [Authorize(Roles = "Admin")]
        public IActionResult Update(int id)
        {

          
            var photo = _context.Photos.Include(p => p.Categories).FirstOrDefault(p => p.Id == id);

			if(photo is null)
			{
				return View("NotFound");
			}

			var formModel = new PhotoFormModel
			{
				Photo = photo,
				Categories = _context.Categories.ToArray(),
				SelectedCategoryIds = photo.Categories!.Select(t => t.Id).ToList()
			};

			return View(formModel);	
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Update(int id, PhotoFormModel form)
		{
			if (!ModelState.IsValid)
			{
				form.Categories = _context.Categories.ToArray();

				return View(form);
			}
			var photoToUpdate = _context.Photos.Include(p => p.Categories).FirstOrDefault(p => p.Id == id);

			if (photoToUpdate is null)
			{
				return View("NotFound");
			}

			photoToUpdate.Title = form.Photo.Title;
			photoToUpdate.Description = form.Photo.Description;
			photoToUpdate.Image = form.Photo.Image;
			photoToUpdate.ImageFile = form.Photo.ImageFile;
			photoToUpdate.Visible = form.Photo.Visible;
			photoToUpdate.Categories = _context.Categories.Where(c => form.SelectedCategoryIds.Contains(c.Id)).ToList();

			_context.SaveChanges();
			return RedirectToAction("index");	
		}

        [Authorize(Roles = "Admin")] 
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