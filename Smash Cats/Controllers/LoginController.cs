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
using Smash_Cat;

namespace Smash_Cats.Controllers
{
	public class LoginController : Controller
	{
		private readonly ILogger<LoginController> _logger;
		private IWebHostEnvironment webHost;

		public LoginController(ILogger<LoginController> logger, IWebHostEnvironment webHost)
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
		public async Task<IActionResult> Login(string login, string password, string email, string ReturnUrl)
		{

			var user = new User
			{
				Name = login,
				Email = email,
				Password = password
			};

			using (var httpClient = new HttpClient())
			{
				var jsonUser = JsonConvert.SerializeObject(user);

				var response = await httpClient.PostAsync("http://localhost:5235/api/Login", new StringContent(jsonUser, Encoding.UTF8, "application/json"));

				if (response.IsSuccessStatusCode)
				{
					_logger.LogInformation("Данные пользователя успешно отправлены на базу.");

					return RedirectToAction("Index", "Home");
				}
				else
				{
					_logger.LogError("Ошибка при отправке данных пользователя на API. Код ошибки: {StatusCode}", response.StatusCode);
				}
			}

			if (string.IsNullOrEmpty(ReturnUrl))
			{
				return RedirectToAction("Index", "Home");
			}
			else
			{
				return Redirect(ReturnUrl);
			}
		}

		public IActionResult Logout()
		{

			HttpContext.SignOutAsync();
			return RedirectToAction("Index", "Login");
		}



		[HttpPost]
		public async Task<IActionResult> SignIn(string login, string password, string ReturnUrl)
		{
			var user = new User
			{
				Name = login,
				Password = password
			};
			user.Id = 0;
			user.Email = "";

			using (var httpClient = new HttpClient())
			{
				var jsonUser = JsonConvert.SerializeObject(user);
				var response = await httpClient.PostAsync("http://localhost:5235/api/Login/Authenticate", new StringContent(jsonUser, Encoding.UTF8, "application/json"));

				if (response.IsSuccessStatusCode)
				{
                    var content = await response.Content.ReadAsStringAsync();
                    var authenticatedUser = JsonConvert.DeserializeObject<User>(content);

                    var userJson = JsonConvert.SerializeObject(authenticatedUser);
                    HttpContext.Session.SetString("User", userJson);


                    return RedirectToAction("Index", "Personal"/*, authenticatedUser*/ /*new { id = authenticatedUser.Id }*/);
                }
				else
				{
					_logger.LogError("Ошибка входа: Неверное имя пользователя или пароль.");
					return RedirectToAction("Index", "Login");
				}
			}
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
