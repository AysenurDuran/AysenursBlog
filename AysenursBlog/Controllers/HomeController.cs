using AysenursBlog.Filter;
using AysenursBlog.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AysenursBlog.Controllers
{
   
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BlogContext _context;

        public HomeController(ILogger<HomeController> logger, BlogContext context)
        {
            _logger = logger;
            _context = context; 
        }

        public ActionResult About()
        {
            return View();
        }
        public IActionResult MainIndex()
        {
            var list = _context.Blog.Take(4).Where(b=> b.IsPublish).OrderByDescending(x=>x.CreateTime).ToList();
            foreach(var blog in list)
            {
                blog.Author = _context.Author.Find(blog.AuthorId);
            }
            return View(list);
        }
		public IActionResult Login(string Email, string Password)
		{
			var author = _context.Author.FirstOrDefault(w => w.Email == Email && w.Password == Password);
			if (author == null)
			{
				return RedirectToAction(nameof(Index));

			}

			HttpContext.Session.SetInt32("id", author.Id);
			HttpContext.Session.SetString("fullname", author.Name);

			return RedirectToAction("Index", "Blog");
		}
		public IActionResult LogOut()
		{

			HttpContext.Session.Clear();
			return RedirectToAction(nameof(Index));
		}
		public IActionResult Post(int Id)
        {
            var blog = _context.Blog.Find(Id);
            blog.Author = _context.Author.Find(blog.AuthorId);
            blog.ImagePath = " /img" + blog.ImagePath;
            return View(blog);
        }
        public IActionResult Index()
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