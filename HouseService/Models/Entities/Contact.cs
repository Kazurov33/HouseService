using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HouseService.Models;
namespace HouseService.Models.Entities
{
    public class Contact
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Phone { get; set; }
        public ContactType ContactType { get; set; }
        public List<Request> Requests { get; set; }
        public string FullName()
        {
            return LastName +" " + FirstName + " " + MiddleName + " ";
        }
    }
}
