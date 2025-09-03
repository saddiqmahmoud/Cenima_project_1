using Microsoft.AspNetCore.Mvc;
using NuGet.DependencyResolver;
using System.Threading.Tasks;

namespace Cenima_project.Areas.Admin.Controllers
{
    [Area(SD.AdminArea)]
    public class CenimaController : Controller
    {
        private Repository<Cinema> _CinemaRepositry = new();
        public async Task<IActionResult> Index()
        {
            var Cenima = await _CinemaRepositry.GetAsync();

            return View(Cenima);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Cinema Cinema,IFormFile cinemaLogo)
        {
            TempData["Sucess_notification"] = "Cinema Create Succeussfully";
            if (cinemaLogo is not null && cinemaLogo.Length>0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(cinemaLogo.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Photos", fileName);
                using (var stream = System.IO.File.Create(path))
                {
                    cinemaLogo.CopyTo(stream);
                };
                Cinema.CinemaLogo = fileName;
                await _CinemaRepositry.CreateAsync(Cinema);
                await _CinemaRepositry.Commit();
                return RedirectToAction(SD.IndexPage);
            }
            return BadRequest();
           
        }
        [HttpGet]
        public async Task<IActionResult> Edite(int Id)
        {
            var Cinema = await _CinemaRepositry.GetOneAsync(e => e.Id == Id);
            if (Cinema is null)
                return RedirectToAction(SD.NotFoundPage, SD.HomeController);
            return View(Cinema);
                
        }
        [HttpPost]
        public async Task<IActionResult> Edite(Cinema Cinema, IFormFile? cinemaLogo)
        {
            TempData["Sucess_notification"] = "Cinema Edite Succeussfully";
            var cinemadb = await _CinemaRepositry.GetOneAsync(e=>e.Id == Cinema.Id,Tracked:false);
            if(cinemaLogo is not null && cinemaLogo.Length>0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(cinemaLogo.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Photos", fileName);
                using (var stream = System.IO.File.Create(path))
                {
                    cinemaLogo.CopyTo(stream);
                }
                ;
                var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwwroot\\Photos", cinemadb.CinemaLogo);
                if(System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }
                Cinema.CinemaLogo = fileName;
            }
            else
            {
                Cinema.CinemaLogo= cinemadb.CinemaLogo;
            }
            _CinemaRepositry.Update(Cinema);
            await _CinemaRepositry.Commit();
            return RedirectToAction(SD.IndexPage);
        }
        public async Task<IActionResult> Delete(int Id)
        {
            TempData["Sucess_notification"] = "Cinema Delete Succeussfully";
            var Cinema = await _CinemaRepositry.GetOneAsync(e => e.Id == Id);
            if(Cinema is null)
            {
                return RedirectToAction(SD.NotFoundPage, SD.HomeController);
            } 
            else
            {

                var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Photos", Cinema.CinemaLogo);
                if(System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }
                _CinemaRepositry.Delete(Cinema);
                await _CinemaRepositry.Commit();
                return RedirectToAction(SD.IndexPage);

            }
        }
    }
}
