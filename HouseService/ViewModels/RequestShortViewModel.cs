using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HouseService.Models.Entities;
namespace HouseService.ViewModels
{
    public class RequestShortViewModel
    {
        public int id { get; set; }
        public DateTime StartDate { get; set; }
        public string Description { get; set; }
        public int? RequestStatusId { get; set; }

        public string ContactPhone { get; set; }

        public string RequestType { get; set; }
        public string Address { get; set; }
        public int ContactId { get; set; }
        public string Contact { get; set; }
        public int CompanyId { get; set; }
        public string Company { get; set; }
        //public static RequestShortViewModel FromRequest(Request request)
        //{

        //}
    }
}
