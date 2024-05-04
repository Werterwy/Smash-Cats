using Microsoft.AspNetCore.Mvc;
using Smash_Cats.Models;
using Newtonsoft.Json;

namespace Smash_Cats.Controllers
{
    public class ContactController : Controller
    {

        private readonly ILogger<ContactController> _logger;

        public ContactController(ILogger<ContactController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
			_logger.LogInformation("logging Information");
            _logger.LogCritical("Logging Critical");
            _logger.LogDebug("Logging Debug");
            _logger.LogError("Logging Error");

            return View();
        }

        public IActionResult RenderPartial()
        {
            return View();
        }
    }
}
