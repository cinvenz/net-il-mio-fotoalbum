using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using net_il_mio_fotoalbum.Models;
using System.Data;

namespace net_il_mio_fotoalbum.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MessageController : Controller
    {

        private readonly PhotoContext _context;

        public MessageController(PhotoContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var messages = _context.Messages!.ToArray();
            return View(messages);
        }
    }
}
