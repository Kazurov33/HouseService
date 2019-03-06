using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseService.ViewModels
{
    public class CreateRequestModel
    {
        public string Description { get; set; }
        public int RequestTypeId { get; set; }

        public int RegionId { get; set; }
        public int CityId { get; set; }
        public int StreetId { get; set; }
        public int HouseId { get; set; }
        public int RoomId { get; set; }
        public int? ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Phone { get; set; }

        public int ContactType { get; set; }

    }

}
