using Microsoft.AspNetCore.Mvc;

namespace Smash_Cats.Controllers
{
	public class PersonalController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
