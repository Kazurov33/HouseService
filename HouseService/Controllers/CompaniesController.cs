using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HouseService.Data;
using HouseService.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace HouseService.Controllers
{
    [Route("api")]
    public class CompaniesController : Controller
    {
        private readonly ApplicationDbContext _appDbContext;
        public CompaniesController(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public IActionResult GetAllCompany()
        {
            return Ok(_appDbContext.Companies);
        }

        // GET: api/Company/5
        [HttpGet]
        [Route("Companies/{id}")]
        public IActionResult GetCompany(int id)
        {
            var company = _appDbContext.Companies.Find(id);
            if (company == null)
            {
                return NotFound();
            }

            return Ok(company);
        }
        [HttpGet]
        [Route("Companies/ByAddress")]
        public IActionResult FindByAddress(
            [FromQuery]int? regionid,
            [FromQuery]int? cityid,
            [FromQuery]int? streetid,
            [FromQuery]int? houseid
            )
        {
            var address = _appDbContext
                .Adresses.Include(a => a.Company)
                .Where(a => a.RegionId == regionid && a.CityId == cityid && a.StreetId == streetid && a.HouseId == houseid)
                .GroupBy(a=>a.Company)
                .SingleOrDefault();
            if (address != null)
            {
                return Ok(address.Key);
            }
            else
                return NotFound();
        }
        // POST: api/Company
        [HttpPost]
        public IActionResult PostCompany(CompanyModel company)
        {

            _appDbContext.Companies.Add(company);
            _appDbContext.SaveChanges();

            return CreatedAtAction("CreateCity", new { id = company.Id }, company);
        }

        // PUT: api/Company/5
        [HttpPut("{id}")]
        public IActionResult PutCompany(int id, CompanyModel company)
        {
            if (id != company.Id)
            {
                return BadRequest();
            }

            _appDbContext.Entry(company).State = EntityState.Modified;
            _appDbContext.SaveChanges();

            return NoContent();
        }

        // DELETE: api/Company/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCompany(int id)
        {
            var company = _appDbContext.Companies.Find(id);
            if (company == null)
            {
                return NotFound();
            }

            _appDbContext.Companies.Remove(company);
            _appDbContext.SaveChanges();

            return Ok(company);
        }
    }
}