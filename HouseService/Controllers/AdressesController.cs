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
    public class AdressesController : ControllerBase
    {
        private readonly ApplicationDbContext _appDbContext;
        public AdressesController(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        /*-- Адресс по компании --*/
        [Route("[controller]/Company/{CompanyId}")]
        [HttpGet]
        public IList<AdressModel> GetByCompany(int? CompanyId)
        {
            if (CompanyId != null)
            {
                var _adress = _appDbContext.Adresses.Where(p => p.CompanyId == CompanyId).ToList();
                return _adress;
            }
            else
            {
                return _appDbContext.Adresses.AsEnumerable().ToList();
            }
        }

    }
}
