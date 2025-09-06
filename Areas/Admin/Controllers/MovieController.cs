using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cenima_project.Areas.Admin.Controllers
{
    [Area(SD.AdminArea)]
    public class MovieController : Controller
    {
        private IRepositores<Movie> _MovieRepository;// = new Repository<Movie>();
        private IRepositores<Category> _CategoryRepository;// = new Repository<Category>();
        private IRepositores<Cinema> _CinemaRepository;// = new Repository<Cinema>();
        public MovieController(IRepositores<Movie> MovieRepository, IRepositores<Category> CategoryRepository, IRepositores<Cinema> CinemaRepository)
        {
            _MovieRepository = MovieRepository;
            _CategoryRepository = CategoryRepository;
            _CinemaRepository = CinemaRepository;
        }
        public async Task<IActionResult> Index()
        {
            var movies = await _MovieRepository.GetAsync(includes: [e => e.Category, e => e.Cinema]);
            return View(movies);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var category = await _CategoryRepository.GetAsync();
            var cinema = await _CinemaRepository.GetAsync();
            CategoryWithCinemaVM categoryWithCinema = new()
            {
                Categories = category,
                Cinemas = cinema
            };

            return View(categoryWithCinema);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Movie movie,IFormFile MinImg)
        {
            if(!ModelState.IsValid)
            {
                return View(movie);
            }
            TempData["Sucess_notification"] = "Movie Create Succeussfully";
            if (MinImg is not null && MinImg.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(MinImg.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Photos", fileName);
                using (var stream = System.IO.File.Create(path))
                {
                    MinImg.CopyTo(stream);
                }
                ;
                movie.ImgUrl = fileName;
                await _MovieRepository.CreateAsync(movie);
                await _MovieRepository.Commit();
                return RedirectToAction(SD.IndexPage);
            }
            return BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> Edite(int Id)
        {
            var movie = await _MovieRepository.GetOneAsync(e => e.Id == Id);
            var category = await _CategoryRepository.GetAsync();
            var cinema = await _CinemaRepository.GetAsync();
            if (movie is null)
                return RedirectToAction(SD.NotFoundPage, SD.HomeController);

            CategoryWithCinemaVM categoryWithCinema = new()
            {
                Categories = category,
                Cinemas = cinema,
                movie= movie
            };

            return View(categoryWithCinema);

        }
        [HttpPost]
        public async Task<IActionResult> Edite(Movie movie, IFormFile? MainImg)
        {
            if(!ModelState.IsValid)
            {
                return View(movie);
            }
            TempData["Sucess_notification"] = "Movie Edite Succeussfully";
            var moviedb = await _MovieRepository.GetOneAsync(e => e.Id == movie.Id, Tracked: false);
            if (MainImg is not null && MainImg.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(MainImg.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Photos", fileName);
                using (var stream = System.IO.File.Create(path))
                {
                    MainImg.CopyTo(stream);
                }
                ;
                var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwwroot\\Photos", moviedb.ImgUrl);
                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }
                movie.ImgUrl = fileName;
            }
            else
            {
                movie.ImgUrl = moviedb.ImgUrl;
            }
            _MovieRepository.Update(movie);
            await _MovieRepository.Commit();
            return RedirectToAction(SD.IndexPage);
        }
        public async Task<IActionResult> Delete(int id)
        {
            TempData["Sucess_notification"] = "Movie Delete Succeussfully";
            var movie = await _MovieRepository.GetOneAsync(e => e.Id == id);
            if (movie is null)
            {
                return RedirectToAction(SD.NotFoundPage, SD.HomeController);
            }
            else
            {
                var oldpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Photos", movie.ImgUrl);
                if (System.IO.File.Exists(oldpath))
                {
                    System.IO.File.Delete(oldpath);
                }
                _MovieRepository.Delete(movie);
                await _MovieRepository.Commit();
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
