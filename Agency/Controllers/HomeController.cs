using Agency.DAL;
using Agency.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Agency.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbcontext _context;

        public HomeController(AppDbcontext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM
            {
                Projects = _context.Projects.ToList(),
                Categories = _context.Categories.ToList()
            };
            return View(homeVM);
        }
    }
}
