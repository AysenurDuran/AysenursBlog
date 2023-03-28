
using AysenursBlog.Filter;
using AysenursBlog.Models;
using Microsoft.AspNetCore.Mvc;

namespace AysenursBlog.Controllers
{
    [UserFilter]
	
	public class AuthorController : Controller
    {
        private readonly BlogContext _context;
        public AuthorController(BlogContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> AddAuthor(Author author)
        {
            if (author.Id == 0)
            {
                await _context.AddAsync(author);
            }
            else
            {
                _context.Update(author);
            }
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Author));
        }
        public async Task<IActionResult> AuthorDetails(int Id)
        {
            var author = await _context.Author.FindAsync(Id);
            return Json(author);
        }
        public IActionResult Author()
        {
            List<Author> list = _context.Author.ToList();
            return View(list);
        }
        public async Task<IActionResult> DeleteAuthor(int? Id)
        {
            var author = await _context.Author.FindAsync(Id);
            _context.Remove(author);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Author));
        }
    }
}
