using System.Threading.Tasks;
using Agency.DAL;
using Agency.Models;
using Agency.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Agency.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly AppDbcontext _context;

        public CategoryController(AppDbcontext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<GetCategoryVM> categoryVMs = await _context.Categories.Select(c => new GetCategoryVM
            {
                Id = c.Id,
                Name = c.Name,
                Projects = c.Projects,
            }).ToListAsync();
            return View(categoryVMs);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryVM categoryVM)
        {
            if (!ModelState.IsValid) return View();

            bool result = await _context.Categories.AnyAsync(c => c.Name == categoryVM.Name);
            if (result)
            {
                ModelState.AddModelError(nameof(Category.Name), "Category already exists!");
                return View();
            }

            Category category = new Category
            {
                Name = categoryVM.Name,
            };


            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id < 1) return BadRequest();
            
            Category? category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (category == null) return NotFound();

            UpdateCategoryVM categoryVM = new UpdateCategoryVM
            {
                Name = category.Name
            };

            return View(categoryVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateCategoryVM categoryVM)
        {
            if(!ModelState.IsValid) return View();

            bool result = await _context.Categories.AnyAsync(c=> c.Name == categoryVM.Name && c.Id != id);

            if (result)
            {
                ModelState.AddModelError(nameof(UpdateCategoryVM.Name), "Category already exists!");
                return View();
            }

            Category? existed = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (existed.Name == categoryVM.Name) return RedirectToAction(nameof(Index));
            existed.Name = categoryVM.Name;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null || id < 1) return BadRequest();

            Category? category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if(category is null) return NotFound();



             _context.Categories.Remove(category);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
