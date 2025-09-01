using Microsoft.AspNetCore.Mvc;
using NuGet.DependencyResolver;
using System.Threading.Tasks;

namespace Cenima_project.Areas.Admin.Controllers
{
    [Area(SD.AdminArea)]
    public class ActorsController : Controller
    {
        private Repository<Actor> _ActorRepositry = new();
        public async Task<IActionResult> Index()
        {
            var actors = await _ActorRepositry.GetAsync();

            return View(actors);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Actor actor,IFormFile profileImag)
        {
            if(profileImag is not null && profileImag.Length>0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(profileImag.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Photos", fileName);
                using (var stream = System.IO.File.Create(path))
                {
                    profileImag.CopyTo(stream);
                };
                actor.ProfilePicture = fileName;
                await _ActorRepositry.CreateAsync(actor);
                await _ActorRepositry.Commit();
                return RedirectToAction(SD.IndexPage);
            }
            return BadRequest();
           
        }
        [HttpGet]
        public async Task<IActionResult> Edite(int Id)
        {
            var actor = await _ActorRepositry.GetOneAsync(e => e.Id == Id);
            if (actor is null)
                return RedirectToAction(SD.NotFoundPage, SD.HomeController);
            return View(actor);
                
        }
        [HttpPost]
        public async Task<IActionResult> Edite(Actor actor, IFormFile? profileImag)
        {
            var actrodb = await _ActorRepositry.GetOneAsync(e=>e.Id == actor.Id,Tracked:false);
            if(profileImag is not null && profileImag.Length>0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(profileImag.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Photos", fileName);
                using (var stream = System.IO.File.Create(path))
                {
                    profileImag.CopyTo(stream);
                }
                ;
                var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwwroot\\Photos", actrodb.ProfilePicture);
                if(System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }
                actor.ProfilePicture = fileName;
            }
            else
            {
                actor.ProfilePicture = actrodb.ProfilePicture;
            }
            _ActorRepositry.Update(actor);
            await _ActorRepositry.Commit();
            return RedirectToAction(SD.IndexPage);
        }
        public async Task<IActionResult> Delete(int Id)
        {
            var actor = await _ActorRepositry.GetOneAsync(e => e.Id == Id);
            if(actor is null)
                return RedirectToAction(SD.NotFoundPage, SD.HomeController);
            else
            {

                var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Photos", actor.ProfilePicture);
                if(System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }
                _ActorRepositry.Delete(actor);
                await _ActorRepositry.Commit();
                return RedirectToAction(SD.IndexPage);

            }
        }
    }
}
