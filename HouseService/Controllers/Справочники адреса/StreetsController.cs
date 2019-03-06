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
    public class StreetsController : ControllerBase
    {
        private readonly ApplicationDbContext _appDbContext;
        public StreetsController(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        /*-- Улица по городу --*/
        [Route("[controller]/City/{CityId}")]
        [HttpGet]
        public IList<Street> Get(int? CityId)
        {
            if (CityId != null)
            {
                var _street = _appDbContext.Streets.Where(p => p.CityId == CityId).ToList();
                return _street;
            }
            else
            {
                return _appDbContext.Streets.AsEnumerable().ToList();
            }
        }

        // GET: api/Street
        [Route("[controller]")]
        [HttpGet]
        public IActionResult GetAllStreet()
        {
            return Ok(_appDbContext.Streets.ToList());
        }

        // GET: api/Street/5
        [Route("[controller]/{id}")]
        [HttpGet]
        public IActionResult GetStreet(int id)
        {
            var street = _appDbContext.Streets.Find(id);
            if (street == null)
            {
                return NotFound();
            }

            return Ok(street);
        }

        // POST: api/Street
        [Route("[controller]")]
        [HttpPost]
        public IActionResult PostStreet(Street street)
        {

            _appDbContext.Streets.Add(street);
            _appDbContext.SaveChanges();

            return CreatedAtAction("CreateStreet", new { id = street.Id}, street);
        }

        // PUT: api/Street/5
        [Route("[controller]/{id}")]
        [HttpPut]
        public IActionResult PutStreet(int id, Street street)
        {
            if (id != street.Id)
            {
                return BadRequest();
            }

            _appDbContext.Entry(street).State = EntityState.Modified;
            _appDbContext.SaveChanges();

            return NoContent();
        }

        // DELETE: api/Street/5
        [Route("[controller]/{id}")]
        [HttpDelete]
        public IActionResult DeleteStreet(int id)
        {
            var street = _appDbContext.Cities.Find(id);
            if (street == null)
            {
                return NotFound();
            }

            _appDbContext.Cities.Remove(street);
            _appDbContext.SaveChanges();

            return Ok(street);
        }
    }
}
