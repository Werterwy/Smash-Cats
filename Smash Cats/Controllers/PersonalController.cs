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

        /* [HttpGet]
         public async Task<IActionResult> Index()
         {
             var userJson = HttpContext.Session.GetString("User");
             if (!string.IsNullOrEmpty(userJson))
             {
                 var user = JsonConvert.DeserializeObject<User>(userJson);
                 var client = _httpClientFactory.CreateClient();

                 var response = await client.GetAsync($"http://localhost:5235/api/Login/{user.Id}");
                 if (response.IsSuccessStatusCode)
                 {
                     var userFromDb = JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
                     return View(userFromDb);
                 }
             }
             return View();
         }

         [HttpPost]
         public async Task<IActionResult> Index(User user, List<string> achievements)
         {
             var userJson = JsonConvert.SerializeObject(user);
             HttpContext.Session.SetString("User", userJson);

             var client = _httpClientFactory.CreateClient();
             var content = new MultipartFormDataContent
         {
             { new StringContent(user.Id.ToString()), "Id" },
             { new StringContent(user.Name), "Name" },
             { new StringContent(user.Email), "Email" },
             { new StringContent(user.Password), "Password" }
         };

             foreach (var achievement in achievements)
             {
                 content.Add(new StringContent(achievement), "achievements");
             }

             var response = await client.PutAsync("http://localhost:5235/api/Login", content);
             if (response.IsSuccessStatusCode)
             {
                 return RedirectToAction("Index");
             }

             return View("Error");
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



    }
}
