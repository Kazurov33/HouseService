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
    public class CitiesController : ControllerBase
    {
        private readonly ApplicationDbContext _appDbContext;
        public CitiesController (ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        /*-- Город по региону --*/
        [Route ("Cities/Region/{RegionId}")]
        [HttpGet]
        public IList<City> Get(int? RegionId)
        {
            if (RegionId != null)
            {
                var _city =  _appDbContext.Cities.Where(p => p.RegionId == RegionId).ToList();
                return _city;
            }
            else
            {
                return _appDbContext.Cities.AsEnumerable().ToList();
            }
        }

        // GET: api/City
        [Route("Cities")]
        [HttpGet]
        public IActionResult GetAllCity()
        {
            return Ok(_appDbContext.Cities.ToList());
        }

        // GET: api/City/5
        [Route("Cities/{id}")]
        [HttpGet]
        public IActionResult GetCity(int id)
        {
            var city = _appDbContext.Cities.Find(id);
            if (city == null)
            {
                return NotFound();
            }

            return Ok(city);
        }

        // POST: api/City
        [Route("Cities")]
        [HttpPost]
        public IActionResult PostCity(City city)
        {

            _appDbContext.Cities.Add(city);
            _appDbContext.SaveChanges();

            return CreatedAtAction("CreateCity", new { id = city.Id}, city);
        }

        // PUT: api/City/5
        [Route("Cities/{id}")]
        [HttpPut]
        public IActionResult PutCity(int id, City city)
        {
            if (id != city.Id)
            {
                return BadRequest();
            }

            _appDbContext.Entry(city).State = EntityState.Modified;
            _appDbContext.SaveChanges();

            return NoContent();
        }

        // DELETE: api/City/5
        [Route("[controller]/{id}")]
        [HttpDelete]
        public IActionResult DeleteCity(int id)
        {
            var city = _appDbContext.Cities.Find(id);
            if (city == null)
            {
                return NotFound();
            }

            _appDbContext.Cities.Remove(city);
            _appDbContext.SaveChanges();

            return Ok(city);
        }
    }
}
