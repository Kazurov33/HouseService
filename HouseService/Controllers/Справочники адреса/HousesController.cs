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
    public class HousesController : ControllerBase
    {
        private readonly ApplicationDbContext _appDbContext;
        public HousesController(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        /*-- Дом по улице --*/
        [Route("Houses/Street/{StreetId}")]
        [HttpGet]
        public IList<House> Get(int? StreetId)
        {
            if (StreetId != null)
            {
                var _house = _appDbContext.Houses.Where(p => p.StreetId == StreetId).ToList();
                return _house;
            }
            else
            {
                return _appDbContext.Houses.AsEnumerable().ToList();
            }
        }

        // GET: api/House
        [Route("Houses")]
        [HttpGet]
        public IActionResult GetAllHouse()
        {
            return Ok(_appDbContext.Houses.ToList());
        }

        // GET: api/House/5

        [Route("Houses/{id}")]

        [HttpGet]
        public IActionResult GetHouse(int id)
        {
            var house = _appDbContext.Houses.Find(id);
            if (house == null)
            {
                return NotFound();
            }

            return Ok(house);
        }

        // POST: api/House
        [HttpPost]
        [Route("Houses")]
        public IActionResult PostHouse(House house)
        {

            _appDbContext.Houses.Add(house);
            _appDbContext.SaveChanges();

            return CreatedAtAction("CreateHouse", new { id = house.Id}, house);
        }

        // PUT: api/House/5

        [Route("Houses/{id}")]

        [HttpPut]
        public IActionResult PutHouse(int id, House house)
        {
            if (id != house.Id)
            {
                return BadRequest();
            }

            _appDbContext.Entry(house).State = EntityState.Modified;
            _appDbContext.SaveChanges();

            return NoContent();
        }

        // DELETE: api/House/5

        [Route("Houses/{id}")]

        [HttpDelete]
        public IActionResult DeleteHouse(int id)
        {
            var house = _appDbContext.Houses.Find(id);
            if (house == null)
            {
                return NotFound();
            }

            _appDbContext.Houses.Remove(house);
            _appDbContext.SaveChanges();

            return Ok(house);
        }
    }
}
