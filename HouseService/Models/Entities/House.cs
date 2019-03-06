using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HouseService.Models.Entities
{
    public class House
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public int StreetId { get; set; }
        public Street Street { get; set; }
    }
}
