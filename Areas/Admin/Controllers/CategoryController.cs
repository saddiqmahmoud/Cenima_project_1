using Cenima_project.NewModels;
using Cenima_project.Repository;
using Cenima_project.Ulitirity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cenima_project.Areas.Admin.Controllers
{
    [Area(SD.AdminArea)]
    public class CategoryController : Controller
    {
   
        private Repository<Category> _CategoryRepository = new();
        public async Task<IActionResult> Index()
        {
            var categories = await _CategoryRepository.GetAsync();
            return View(categories);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            TempData["Sucess_notification"] = "Category Create Succeussfully";
            await _CategoryRepository.CreateAsync(category);
            await _CategoryRepository.Commit();
            return RedirectToAction(SD.IndexPage);
        }
        [HttpGet]
        public async Task<IActionResult> Edite(int Id)
        {
            var category = await _CategoryRepository.GetOneAsync(e => e.Id == Id);
            if (category is null)
                return RedirectToAction(SD.NotFoundPage,SD.HomeController);

            return View(category);
        }
        public async Task<IActionResult> Edite(Category category)
        {
            TempData["Sucess_notification"] = "Category Edite Succeussfully";
            _CategoryRepository.Update(category);
            await _CategoryRepository.Commit();
            return RedirectToAction(SD.IndexPage);
        }
        public async Task<IActionResult> Delete(int Id)
        {
            TempData["Sucess_notification"] = "Category Delete Succeussfully";
            var category = await _CategoryRepository.GetOneAsync(e => e.Id == Id);
            if(category is null)
            {
                return RedirectToAction(SD.NotFoundPage, SD.HomeController);
            }
            _CategoryRepository.Delete(category);
            await _CategoryRepository.Commit();
            return RedirectToAction(SD.IndexPage);
        }
    }
    
}
