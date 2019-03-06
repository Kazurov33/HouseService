using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HouseService.Data;
using HouseService.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace HouseService.Controllers
{
    [Route("api")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ApplicationDbContext _appDbContext;
        public ContactsController(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_appDbContext.Contacts);
        }
        [HttpGet]
        [Route("Contacts/ByPhone")]
        public IActionResult FindByPhone([FromQuery]string phone)
        {
            if (!string.IsNullOrEmpty(phone))
            {
                return Ok(_appDbContext.Contacts.Where(c => c.Phone.Contains(phone)).ToList());
            }
            return BadRequest();
        }
    }
}