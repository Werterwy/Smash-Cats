using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Smash_Cats.Models;
using Smash_Cat;

namespace Smash.Cats.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private SmashCatsContext _db;

        public RoomController(SmashCatsContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IEnumerable<Room> Get()
        {
            var rooms = _db.Rooms;

            return rooms;
        }

        [HttpGet("{Id}")]
        public Room Get(int id)
        {
            var rooms = _db.Rooms.FirstOrDefault(r => r.Id == id);

            return rooms;
        }

        [HttpGet("GetAvilibleRoom")]
        public IEnumerable<Room> GetAvilibleRoom(int id)
        {
            var rooms = _db.Rooms;

            return rooms;
        }


        [HttpPost]
        public Room Post([FromBody] Room room)
        {
            _db.Rooms.Add(room);
            _db.SaveChanges();
            return room;
        }

        [HttpPut]
        public StatusCodeResult Put([FromForm] Room room)
        {
            var data = _db.Rooms.FirstOrDefault(f => f.Id == room.Id);

            if (data != null)
            {
                data.Name = room.Name;
                data.Description = room.Description;
                data.Price = room.Price;
                data.Id = room.Id;
                data.pathIMG = room.pathIMG;
                data.DateTime = room.DateTime;

                _db.Rooms.Update(data);
                _db.SaveChanges();
                return Ok();
            }

            return NotFound();
        }

        [HttpDelete("{Id}")]
        public StatusCodeResult Delete(int Id)
        {

            var data = _db.Rooms.Find(Id);
            if (data != null)
            {
                _db.Rooms.Remove(data);
                _db.SaveChanges();

                return Ok();
            }
            return BadRequest();

        }

    }
}

