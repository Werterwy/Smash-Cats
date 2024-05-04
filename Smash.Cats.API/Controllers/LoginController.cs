using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Smash.Cats.API.Data;
using Smash.Cats.API.Models;
using System.Text;
using System.Security.Cryptography;

namespace Smash.Cats.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    // без него не работало
	[Consumes("application/json")]
	public class LoginController : ControllerBase
    {
        private SmashContext _db;

        public LoginController(SmashContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IEnumerable<User> Get()
        {
            var users = _db.Users;

            return users;
        }

        [HttpGet("{Id}")]
        public User Get(int id)
        {
            var users = _db.Users.FirstOrDefault(r => r.Id == id);

            return users;
        }

        [HttpGet("GetAvilibleUsers")]
        public IEnumerable<User> GetAvilibleRoom(int id)
        {
            var users = _db.Users;

            return users;
        }


        [HttpPost]
        public User Post([FromBody] User user)
        {
            user.Id = 0;
            // Хэширование пароля (экперимент)
			user.Password = HashPassword(user.Password); 


			_db.Users.Add(user);
            
            _db.SaveChanges();
            return user;
        }

		[HttpPost("Authenticate")]
		public IActionResult Authenticate(User user)
		{
			var existingUser = _db.Users.FirstOrDefault(u => u.Name == user.Name && u.Password == user.Password);
			if (existingUser != null)
			{
				return Ok(existingUser);
			}

			return NotFound("Invalid username or password");
		}

		[HttpPut]
        public StatusCodeResult Put([FromForm] User user)
        {
            var data = _db.Users.FirstOrDefault(f => f.Id == user.Id);

            if (data != null)
            {
                data.Name = user.Name;
                data.Id = user.Id;
                data.Email = user.Email;
                data.Password = user.Password;

                _db.Users.Update(data);
                _db.SaveChanges();
                return Ok();
            }

            return NotFound();
        }

        [HttpDelete("{Id}")]
        public StatusCodeResult Delete(int Id)
        {

            var data = _db.Users.Find(Id);
            if (data != null)
            {
                _db.Users.Remove(data);
                _db.SaveChanges();

                return Ok();
            }
            return BadRequest();

        }

		public string HashPassword(string password)
		{
			using (var sha256 = SHA256.Create())
			{
				var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
				var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
				return hash;
			}
		}

	}
}
