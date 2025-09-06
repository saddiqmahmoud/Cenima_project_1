using Cenima_project.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cenima_project.NewModels;

namespace Cenima_project.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        ApplicationdbContext _context = new ApplicationdbContext();


        public IActionResult Index(Search Search, [FromQuery] int Pagenum = 1)
        {
            var film = _context.Movies.Include(e => e.Cinema).Include(e => e.Category).AsQueryable();
            var category = _context.Categories.ToList();

            if (Search.Name is not null)
            {
                film = film.Where(e => e.Name.Contains(Search.Name));
            }

            if (Search.CategpryID is not null)
            {
                film = film.Where(e => e.CategoryId == Search.CategpryID);
            }

            ViewBag.Pagenum = Pagenum;
            var pagination = Math.Ceiling(film.Count() / 4.0);
            ViewBag.Pagination = pagination;

            film = film.Skip((Pagenum - 1) * 4).Take(4);

            Group group = new Group()
            {
                Movies = film.ToList(),
                Categories = category
            };

            return View(group);
        }

        public IActionResult Categories()
        {
            var items = _context.Categories.AsQueryable();


            return View(items.ToList());
        }

        public IActionResult Cinema()
        {
            var items = _context.Cinemas.AsQueryable();


            return View(items.ToList());
        }


        public IActionResult Cinemainfo([FromRoute] int id)
        {
            var details = _context.Movies.Include(e => e.Category).Include(e => e.Cinema).Where(e => e.CinemaId == id);


            return View(details.ToList());
        }


        public IActionResult Info([FromRoute]int id)
        {
            var details = _context.Movies.Include(e => e.Category).Include(e => e.Cinema).Include(e => e.ActorMovie).FirstOrDefault(e => e.Id == id);
          
            var items = _context.ActorMovies.Include(e => e.Actor).Where(e => e.MovieId == id).ToList();

            var input = new Actor();

            AdditionalActors Actorandmovie = new()
            {
                Movies = details,
                ActorMovies = items,
                Actor = input

            };


            return View(Actorandmovie);
        }
        public IActionResult Categoriesinfo([FromRoute] int id)
        {
            var details = _context.Movies.Include(e => e.Category).Include(e => e.Cinema).Where(e=>e.CategoryId == id);
            

            return View(details.ToList());
        }

    }
}
