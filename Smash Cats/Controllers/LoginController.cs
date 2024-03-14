using Microsoft.AspNetCore.Mvc;

namespace Smash_Cats.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
