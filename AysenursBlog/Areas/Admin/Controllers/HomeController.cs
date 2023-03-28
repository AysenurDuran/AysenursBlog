using AysenursBlog.Models;
using AysenursBlog.Filter;

using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace AysenursBlog.Areas.Admin.Controllers
{
    [UserFilter]
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BlogContext _context;

        public HomeController(ILogger<HomeController> logger, BlogContext context)
        {
            _logger = logger;
            _context = context;
        }

		public IActionResult Login(string Email, string Password)
		{
			var author = _context.Author.FirstOrDefault(w => w.Email == Email && w.Password == Password);
			if (author == null)
			{
				return RedirectToAction(nameof(Index));

			}

			HttpContext.Session.SetInt32("id", author.Id);
            HttpContext.Session.SetString("fullname",author.Name+" "+author.Surname);

			return RedirectToAction("Index", "Blog", new { area = "Admin" });
		}
		public IActionResult LogOut()
        {

            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
