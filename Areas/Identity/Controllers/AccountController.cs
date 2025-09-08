using Microsoft.AspNetCore.Mvc;

namespace Cenima_project.Areas.Identity.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
    }
}
