using System.Threading.Tasks;
using Agency.DAL;
using Agency.Models;
using Agency.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Agency.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProjectController : Controller
    {
        private readonly AppDbcontext _context;

        public ProjectController(AppDbcontext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<GetProjectVM> projectVMs = await _context.Projects.Select(p => new GetProjectVM
            {
                Id = p.Id,
                Name = p.Name,
                Image = p.Image,
                CategoryId = p.CategoryId
            }).ToListAsync();
            return View(projectVMs);
        }
        public async Task<IActionResult> Create()
        {
            CreateProjectVM projectVM = new CreateProjectVM
            {
                Categories = await _context.Categories.ToListAsync()
            };
            return View(projectVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProjectVM projectVM)
        {
            if (ModelState.IsValid) return View(projectVM);

            bool result = await _context.Categories.AnyAsync(c => c.Id == projectVM.CategoryId);
            if (!result)
            {
                ModelState.AddModelError(nameof(CreateProjectVM.CategoryId), "Category doesnt exist!");
                return View(projectVM);
            }

            bool nameResult = await _context.Projects.AnyAsync(p => p.Name == projectVM.Name);
            if (nameResult)
            {
                ModelState.AddModelError(nameof(CreateProjectVM.Name), "Name already exists!");
                return View(projectVM);
            }

            Project project = new Project
            {
                Name = projectVM.Name,
                Image = projectVM.Image,

            }

            return View();
        }

    }
}
