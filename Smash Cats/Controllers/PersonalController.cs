﻿using Microsoft.AspNetCore.Mvc;
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

    


    }
}
