using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Newtonsoft.Json;
using Smash_Cats.Models;

namespace Smash_Cats.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IWebHostEnvironment webHost;
        public LoginController(ILogger<HomeController> logger, IWebHostEnvironment webHost)
        {
            _logger = logger;
            this.webHost = webHost;
        }

        public async Task<IActionResult> Index()
        {

            List<User> rooms = new List<User>();

            string Jwt = GenerateJSONWebToken();


            using (var httpClient = new HttpClient())
            {

                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Jwt);
                using (var responce = await httpClient
                    .GetAsync("http://localhost:5235/api/Login"))
                {
                    string apiResponce = await responce.Content.ReadAsStringAsync();

                    rooms = JsonConvert.DeserializeObject<List<User>>(apiResponce);
                }
            }
            return View();
        }

        public IActionResult Login(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string login, string password, string ReturnUrl)
        {

            if (login == "admin" && password == "admin")
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, login)
                };
                var claimsIdentity = new ClaimsIdentity(claims, "Login");

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                     new ClaimsPrincipal(claimsIdentity));

                // HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                if (string.IsNullOrEmpty(ReturnUrl))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return Redirect(ReturnUrl);
                }

                /* Task.Delay(100).Wait();
                 return Redirect(ReturnUrl);*/
            }
            return View();
        }
        public IActionResult Logout()
        {

            HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public IActionResult SubcribeNewsletter(IFormFile userFile)
        {
            var data = Request.Form["email"];

            string path = Path.Combine(webHost.WebRootPath, userFile.FileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                userFile.CopyTo(stream);

            }
            // return View("Index");

            return RedirectToAction("Index");

        }

        private string GenerateJSONWebToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("4c53ce9de0ab7c9ce2f72f2b1447aa73"));

            var credential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
            (
                issuer: "John Doc",
                audience: "1516239022",
                expires: DateTime.Now.AddHours(3),
                signingCredentials: credential);
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
