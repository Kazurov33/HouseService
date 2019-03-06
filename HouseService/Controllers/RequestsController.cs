using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HouseService.Data;
using HouseService.Models.Entities;
using Microsoft.EntityFrameworkCore;
using HouseService.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace HouseService.Controllers
{
    [Route("api")]
    [ApiController]
    public class RequestsController : Controller
    {
        private readonly ApplicationDbContext _appDbContext;
        private readonly UserManager<AppUser> _usermanager;
        public RequestsController(ApplicationDbContext appDbContext, UserManager<AppUser> usermanager)
        {
            _appDbContext = appDbContext;
            _usermanager = usermanager;
        }

        [Route("Requests/Short")]
        [HttpGet]
        public IList<RequestShortViewModel> GetShortView(
            [FromQuery]int? regionid,
            [FromQuery]int? cityid,
            [FromQuery]int? streetid,
            [FromQuery]int? houseid,
            [FromQuery]int? roomid,
            [FromQuery]int? statusid
            //[FromQuery]int? page
            )
        {
            //var _page = page ?? 0;
            int _statusId = statusid ?? 1;
            IQueryable<Request> req = _appDbContext.Requests
                .Include(r => r.Adress)
                .Include(r => r.Adress.Region)
                .Include(r => r.Adress.City)
                .Include(r => r.Adress.Street)
                .Include(r => r.Adress.House)
                .Include(r => r.Adress.Room)
                .Include(r => r.Adress.Company)
                .Include(r => r.Contact)
                .Include(r => r.RequestType)
                .Where(r => r.RequestStatusId == _statusId)
                .OrderByDescending(r=>r.StartDate)
                //.AsQueryable();
                ;
            if (regionid != null) req = req.Where(r => r.Adress.RegionId == regionid);
            if (cityid != null) req = req.Where(r => r.Adress.CityId == cityid);
            if (streetid != null) req = req.Where(r => r.Adress.StreetId == streetid);
            if (houseid != null) req = req.Where(r => r.Adress.HouseId == houseid);
            if (roomid != null) req = req.Where(r => r.Adress.RoomId == roomid);
            //req = req.Skip(_page * 10).Take(_page);
            var reqv = new List<RequestShortViewModel>();
            foreach (var r in req)
            {
                reqv.Add(new RequestShortViewModel
                {
                    id = r.Id,
                    ContactId = r.ContactId,
                    Description = r.Description,
                    RequestStatusId = r.RequestStatusId,
                    RequestType = r.RequestType.Name,
                    StartDate = r.StartDate,
                    Contact = r.Contact.FullName(),
                    ContactPhone = r.Contact.Phone,
                    CompanyId = r.Adress.CompanyId,
                    Company = r.Adress.Company.ShortName,
                    Address = r.Adress.City.Name + ", ул." + r.Adress.Street.Name + " д." + r.Adress.House.Name + (string.IsNullOrEmpty(r.Adress.Room.Name) ? "" : " кв." + r.Adress.Room.Name)
                });
            };
            return reqv;
        }
        /*-- Заявку по дате --*/
        //[Route("[controller]/StartDate/{StartDateId}")]
        //[HttpGet]
        //public IList<Request> GetByDate(DateTime? StartDate)
        //{
        //    if (StartDate != null)
        //    {
        //        var _request = _appDbContext.Requests.Where(p => p.StartDate == StartDate).ToList();
        //        return _request;
        //    }
        //    else
        //    {
        //        return _appDbContext.Requests.AsEnumerable().ToList();
        //    }
        //}

        /*-- Заявку по адресу --*/
        //[Route("[controller]/Adress/{AdressId}")]
        //[HttpGet]
        //public IList<Request> GetByAdress(int? AdressId)
        //{
        //    if (AdressId != null)
        //    {
        //        var _request = _appDbContext.Requests.Where(p => p.AdressId == AdressId).ToList();
        //        return _request;
        //    }
        //    else
        //    {
        //        return _appDbContext.Requests.AsEnumerable().ToList();
        //    }
        //}

        /*-- Заявку по типу --*/
        //[Route("[controller]/Type/{TypeId}")]
        //[HttpGet]
        //public IList<Request> GetByType(int? TypeId)
        //{
        //    if (TypeId != null)
        //    {
        //        var _request = _appDbContext.Requests.Where(p => p.RequestTypeId == TypeId).ToList();
        //        return _request;
        //    }
        //    else
        //    {
        //        return _appDbContext.Requests.AsEnumerable().ToList();
        //    }
        //}

        /*-- Заявку по статусу --*/
        [Route("[controller]/Status/{StatusId}")]
        [HttpGet]
        public IList<Request> GetByStatus(int? StatusId)
        {
            if (StatusId != null)
            {
                var _request = _appDbContext.Requests.Where(p => p.RequestStatusId == StatusId).ToList();
                return _request;
            }
            else
            {
                return _appDbContext.Requests.AsEnumerable().ToList();
            }
        }

        /*-- Заявку по пользователю --*/
        [Route("[controller]/User/{UserId}")]
        [HttpGet]
        public IList<Request> GetByUser(string UserId)
        {
            // TODO: получать userId из usermanager - текущий пользователь!
            if (!string.IsNullOrEmpty(UserId))
            {
                var _request = _appDbContext.Requests.Where(p => p.UserId == UserId).ToList();
                return _request;
            }
            else
            {
                return _appDbContext.Requests.AsEnumerable().ToList();
            }
        }

        // GET: api/Requests
        [Route("[controller]")]
        [HttpGet]
        public IActionResult GetAllRequest()
        {
            return Ok(_appDbContext.Requests);
        }

        // GET: api/Requests/5
        [Route("[controller]/{id}")]
        [HttpGet]
        public IActionResult GetRequest(int id)
        {
            var request = _appDbContext.Requests.Find(id);
            if (request == null)
            {
                return NotFound();
            }

            return Ok(request);
        }

        // POST: api/Requests
        [Route("[controller]")]
        [HttpPost]
        public async Task<IActionResult> PostRequest(CreateRequestModel r)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            int? addressId = _appDbContext.Adresses
                .Where(a => a.RegionId == r.RegionId && a.CityId == r.CityId && a.StreetId == r.StreetId && a.HouseId == r.HouseId && a.RoomId == r.RoomId)
                .Select(a => a.Id).SingleOrDefault();
            if(addressId == null)
            {
                return NotFound();
            }
            int contactId = 0;
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            if (r.ContactId == null)
            {
                var contact = new Contact
                {
                    FirstName = ti.ToTitleCase(r.FirstName),
                    LastName = ti.ToTitleCase(r.LastName),
                    MiddleName = ti.ToTitleCase(r.MiddleName),
                    ContactType = (Models.ContactType)r.ContactType,
                    Phone = r.Phone
                };
                var c = _appDbContext.Contacts.Add(contact);
                _appDbContext.SaveChanges();
                contactId = c.Entity.Id;
            }
            // TODO: GET current user
            var _manager = await _usermanager.FindByNameAsync("manager");
            var managerId = _manager.Id;
            var request = new Request
            {
                AdressId = addressId,
                ContactId = r.ContactId?? contactId,
                Description = r.Description,
                RequestTypeId = r.RequestTypeId,
                RequestStatusId = 1,
                StartDate = DateTime.Now,
                UserId = managerId
            };
            var rq = _appDbContext.Requests.Add(request);
            _appDbContext.SaveChanges();
            //
            Request req = _appDbContext.Requests
                .Include(x => x.Adress)
                .Include(x => x.Adress.Region)
                .Include(x => x.Adress.City)
                .Include(x => x.Adress.Street)
                .Include(x => x.Adress.House)
                .Include(x => x.Adress.Room)
                .Include(x => x.Adress.Company)
                .Include(x => x.Contact)
                .Include(x => x.RequestType)
                .Where(x => x.Id == rq.Entity.Id).SingleOrDefault();

            var rvm = new RequestShortViewModel
            {
                id = req.Id,
                ContactId = req.ContactId,
                Description = req.Description,
                RequestStatusId = req.RequestStatusId,
                RequestType = req.RequestType.Name,
                StartDate = req.StartDate,
                Contact = req.Contact.FullName(),
                ContactPhone = req.Contact.Phone,
                CompanyId = req.Adress.CompanyId,
                Company = req.Adress.Company.ShortName,
                Address = req.Adress.City.Name + ", ул." + req.Adress.Street.Name + " д." + req.Adress.House.Name + (string.IsNullOrEmpty(req.Adress.Room.Name) ? "" : " кв." + req.Adress.Room.Name)
            };
            return Ok(rvm);
                //CreatedAtAction("CreateCity", new { id = request.Id }, request);
        }

        // PUT: api/Requests/5
        [Route("[controller]/{id}")]
        [HttpPut]
        public IActionResult PutRequest(int id, Request request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            _appDbContext.Entry(request).State = EntityState.Modified;
            _appDbContext.SaveChanges();

            return NoContent();
        }

        // DELETE: api/Requests/5
        [Route("[controller]/{id}")]
        [HttpDelete]
        public IActionResult DeleteRequest(int id)
        {
            var request = _appDbContext.Requests.Find(id);
            if (request == null)
            {
                return NotFound();
            }

            _appDbContext.Requests.Remove(request);
            _appDbContext.SaveChanges();

            return Ok(request);
        }
    }
}