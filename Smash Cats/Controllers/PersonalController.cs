using Microsoft.AspNetCore.Mvc;
using Smash_Cat;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Smash_Cats.Models;
using System.Text;
using Smash.Cats.API.Models;
using User = Smash_Cats.Models.User;

namespace Smash_Cats.Controllers
{
    public class PersonalController : Controller
    {
        private readonly ILogger<PersonalController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public PersonalController(IHttpClientFactory httpClientFactory, ILogger<PersonalController> logger)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        /*   // [HttpGet]
           public IActionResult Index()
           {
               return View();
           }

           // [HttpPost]
           public async Task<IActionResult> Index(User user)
           {
               if (user == null)
               {
                   // Обработка случая, когда пользовательские данные не переданы
                   user = new User(); // или перенаправьте на другую страницу, если необходимо
               }
               return View(user);
           }*/

        public async Task<IActionResult> Index()
        {
            var userJson = HttpContext.Session.GetString("User");
            if (!string.IsNullOrEmpty(userJson))
            {
                var user = JsonConvert.DeserializeObject<User>(userJson);
                return View(user);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SetUser()
        {
            User user = null;
            var userJson = JsonConvert.SerializeObject(user);
            TempData["User"] = userJson;
            return RedirectToAction("Index");
        }

        /*public async Task<IActionResult> Index(*//*int id*//*  User user)
        {
            *//* var httpClient = _httpClientFactory.CreateClient();
             var response = await httpClient.GetAsync($"http://localhost:5235/api/Login/{id}");

             if (response.IsSuccessStatusCode)
             {
                 var content = await response.Content.ReadAsStringAsync();
                 var user = JsonConvert.DeserializeObject<User>(content);

                 return View(user);
             }
             else
             {
                 return View("Error");
             }*//*

            return View(user);
        }*/

        /*
                [HttpGet]
                public async Task<IActionResult> Index()
                {
                    var httpClient = _httpClientFactory.CreateClient();

                    // запрос к API для получения данных пользователя
                    var response = await httpClient.GetAsync("http://localhost:5235/api/Login");

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var users = JsonConvert.DeserializeObject<IEnumerable<User>>(content);

                        // В этом примере я верну первого пользователя из коллекции
                        var currentUser = users.FirstOrDefault();
                        return View(currentUser);
                    }
                    else
                    {
                        _logger.LogError("Ошибка при получении данных пользователя с API. Код ошибки: {StatusCode}", response.StatusCode);

                        // Пользователь не аутентифицирован, перенаправляем на страницу входа
                        return RedirectToAction("Index", "Login");
                    }
                }*/


    }
}
