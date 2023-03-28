using AysenursBlog.Models;
using AysenursBlog.Filter;

using Microsoft.AspNetCore.Mvc;

namespace AysenursBlog.Areas.Admin.Controllers
{
    [UserFilter]
	[Area("Admin")]
	public class CategoryController : Controller
    {
        private readonly BlogContext _context;
        public CategoryController(BlogContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> AddCategory(Category category)
        {
            if (category.Id == 0)
            {
                await _context.AddAsync(category);
            }
            else
            {
                _context.Update(category);
            }
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Category));
        }
        public async Task<IActionResult> CategoryDetails(int Id)
        {
            var category = await _context.Category.FindAsync(Id);
            return Json(category);
        }
        public IActionResult Category()
        {
            List<Category> list = _context.Category.ToList();
            return View(list);
        }
        public async Task<IActionResult> DeleteCategory(int? Id)
        {
			Category category = await _context.Category.FindAsync(Id);
            _context.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Category));
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
