using AysenursBlog.Models;
using Microsoft.AspNetCore.Mvc;

namespace AysenursBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
	{
		private readonly BlogContext _context;
		public AccountController(BlogContext context)
		{
			_context = context;	
		}
		public IActionResult Index()
		{
			return View();
		}
		public async Task<IActionResult> Register(Author model)
		{
			await _context.AddAsync(model);
			await _context.SaveChangesAsync();
            return Redirect("Index");
        }
	}
}
