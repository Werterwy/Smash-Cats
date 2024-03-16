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
using Microsoft.Extensions.Localization;

namespace Smash_Cats.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUrlHelper _urlHelper;
        private readonly IStringLocalizer<HomeController> _local;

        public HomeController(ILogger<HomeController> logger, IStringLocalizer<HomeController> local)
        {
            _logger = logger;
            _local = local;
            /*_urlHelper = urlHelper;*/
        }

        public IActionResult Index(string culture, string cultureIU)
        {

            ViewBag.AboutUs = _local["aboutus"];

            GetCulture(culture);

            if (!string.IsNullOrWhiteSpace(culture))
            {
                CultureInfo.CurrentCulture = new CultureInfo(culture);
                CultureInfo.CurrentUICulture = new CultureInfo(culture);

            }

            _logger.LogInformation("TEST Message");
            HttpContext.Session.SetString("DB", "27112003");

            var sessionData = HttpContext.Session.GetString("DB");


            CookieOptions options = new CookieOptions();

            options.Expires = DateTime.Now.AddSeconds(100);
            Response.Cookies.Append("DB", "27112003", options);

            string value = Request.Cookies["DB"];

            string email = "ok@ok.com";

            _logger.LogError("У пользователя {email} возникла ошибка {errorMessage}", email, "Ошибка пользователя");


         /*   Stopwatch sw = new Stopwatch();

            sw.Start();
            /// вызов чужого сервиса
            Thread.Sleep(1000);

            sw.Stop();

            //var data = sw.ElapsedMilliseconds;

            _logger.LogInformation("Сервис отработал за {ElapsedMilliseconds}", sw.ElapsedMilliseconds);*/

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

        public string GetCulture(string code = "")
        {
            if (!string.IsNullOrWhiteSpace(code))
            {
                CultureInfo.CurrentCulture = new CultureInfo(code);
                CultureInfo.CurrentUICulture = new CultureInfo(code);

                ViewBag.Culture = string.Format("CurrentCulture: {0}, CurrentUICulture: {1}", CultureInfo.CurrentCulture,
                    CultureInfo.CurrentUICulture);
            }
            return "";
        }

        public IActionResult AboutUs()
        {

            string key = "IIN";
            string value = "880111300392";

            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(1);

            Response.Cookies.Append(key, value);
            Response.Cookies.Append("key_2", value);
            Response.Cookies.Append("key_3", value);



            return View();
        }

        public IActionResult Contact()
        {
            //var checkoutUrl = _urlHelper.Page("/Checkout");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}