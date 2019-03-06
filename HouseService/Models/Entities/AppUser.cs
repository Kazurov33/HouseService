using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace HouseService.Models.Entities
{
    public class AppUser : IdentityUser
    {
        // Extended Properties
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        public ICollection<Request> Requests { get; set; }

        public AppUser()
        {
            Requests = new List<Request>();
        }
    }
}
