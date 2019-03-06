using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HouseService.Data;
using HouseService.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HouseService.Controllers
{
    [Route("api")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly ApplicationDbContext _appDbContext;
        public RoomsController(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        /*-- Квартира по дому --*/
        [Route("[controller]/House/{HouseId}")]
        [HttpGet]
        public IList<Room> Get(int? HouseId)
        {
            if (HouseId != null)
            {
                var _room = _appDbContext.Rooms.Where(p => p.HouseId == HouseId).ToList();
                return _room;
            }
            else
            {
                return _appDbContext.Rooms.AsEnumerable().ToList();
            }
        }

        // GET: api/Rooms
        [Route("[controller]")]
        [HttpGet]
        public IActionResult GetAllRoom()
        {
            return Ok(_appDbContext.Rooms.ToList());
        }

        // GET: api/Rooms/5
        [Route("[controller]/{id}")]
        [HttpGet]
        public IActionResult GetRoom(int id)
        {
            var room = _appDbContext.Rooms.Find(id);
            if (room == null)
            {
                return NotFound();
            }

            return Ok(room);
        }

        // POST: api/Rooms
        [Route("[controller]")]
        [HttpPost]
        public IActionResult PostRoom(Room room)
        {

            _appDbContext.Rooms.Add(room);
            _appDbContext.SaveChanges();

            return CreatedAtAction("CreateRoom", new { id = room.Id}, room);
        }

        // PUT: api/Rooms/5
        [Route("[controller]/{id}")]
        [HttpPut]
        public IActionResult PutRoom(int id, Room room)
        {
            if (id != room.Id)
            {
                return BadRequest();
            }

            _appDbContext.Entry(room).State = EntityState.Modified;
            _appDbContext.SaveChanges();

            return NoContent();
        }

        // DELETE: api/Rooms/5
        [Route("[controller]/{id}")]
        [HttpDelete]
        public IActionResult DeleteRoom(int id)
        {
            var room = _appDbContext.Rooms.Find(id);
            if (room == null)
            {
                return NotFound();
            }

            _appDbContext.Rooms.Remove(room);
            _appDbContext.SaveChanges();

            return Ok(room);
        }
    }
}
