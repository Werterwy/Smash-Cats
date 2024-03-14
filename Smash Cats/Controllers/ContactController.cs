using Microsoft.AspNetCore.Mvc;

namespace Smash_Cats.Controllers
{
    public class ContactController : Controller
    {

        private readonly ILogger<ContactController> _logger;

        public ContactController(ILogger<ContactController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
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
