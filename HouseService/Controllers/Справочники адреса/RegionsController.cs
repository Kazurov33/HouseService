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
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly ApplicationDbContext _appDbContext;
        public RegionsController(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        // GET: api/Region
        [HttpGet]
        public IActionResult GetAllRegions()
        {
            return Ok(_appDbContext.Regions.ToList());
        }

        // GET: api/Region/5
        [HttpGet("{id}", Name = "GetRegion")]
        public IActionResult GetRegion(int id)
        {
            var region = _appDbContext.Regions.Find(id);
            if (region == null)
            {
                return NotFound();
            }

            return Ok(region);
        }

        // POST: api/Region
        [HttpPost]
        public IActionResult PostRegion(Region region)
        {

            _appDbContext.Regions.Add(region);
            _appDbContext.SaveChanges();

            return CreatedAtAction("CreateRegion", new { id = region.Id}, region);
        }

        // PUT: api/Region/5
        [HttpPut("{id}")]
        public IActionResult PutRegion(int id, Region region)
        {
            if (id != region.Id)
            {
                return BadRequest();
            }

            _appDbContext.Entry(region).State = EntityState.Modified;
            _appDbContext.SaveChanges();

            return NoContent();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult DeleteRegion(int id)
        {
            var region = _appDbContext.Regions.Find(id);
            if (region == null)
            {
                return NotFound();
            }

            _appDbContext.Regions.Remove(region);
            _appDbContext.SaveChanges();

            return Ok(region);
        }
    }
}
