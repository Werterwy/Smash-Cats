using Microsoft.AspNetCore.Mvc;
using Smash_Cat;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Smash_Cats.Models;
using System.Text;

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

        public async Task<IActionResult> Index(int id)
        {
            var httpClient = _httpClientFactory.CreateClient();
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
            }
        }
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
