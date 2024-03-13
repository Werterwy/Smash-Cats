using Microsoft.AspNetCore.Mvc;
using Smash_Cats.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Diagnostics;
using System.Reflection.PortableExecutable;
using System.Globalization;
using System.Linq;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Smash_Cats.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUrlHelper _urlHelper;

        public HomeController(ILogger<HomeController> logger/*, IUrlHelper urlHelper*/)
        {
            _logger = logger;
            /*_urlHelper = urlHelper;*/
        }

        public IActionResult Index()
        {

            HttpContext.Session.SetString("DB", "27112003");
            //var data2 = _httpContext.HttpContext.Request.Cookies["Iin"];


            var sessionData = HttpContext.Session.GetString("DB");


            CookieOptions options = new CookieOptions();

            options.Expires = DateTime.Now.AddSeconds(100);
            Response.Cookies.Append("DB", "27112003", options);

            string value = Request.Cookies["DB"];

            string email = "ok@ok.com";

            _logger.LogError("У пользователя {email} возникла ошибка {errorMessage}", email, "Ошибка пользователя");


            Stopwatch sw = new Stopwatch();

            sw.Start();
            /// вызов чужого сервиса
            Thread.Sleep(1000);

            sw.Stop();

            //var data = sw.ElapsedMilliseconds;

            _logger.LogInformation("Сервис отработал за {ElapsedMilliseconds}", sw.ElapsedMilliseconds);

            _logger.LogInformation("logging Information");
            _logger.LogCritical("Logging Critical");
            _logger.LogDebug("Logging Debug");
            _logger.LogError("Logging Error");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Contact()
        {
            /*var checkoutUrl = _urlHelper.Page("/Checkout");*/
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}