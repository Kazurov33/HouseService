using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HouseService.Models.Entities
{
    public class AdressModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("CompanyId")]
        public int CompanyId { get; set; }
        public CompanyModel Company { get; set; }

        [Required]
        [ForeignKey("RegionId")]
        public int RegionId { get; set; }
        public Region Region { get; set; }

        [Required]
        [ForeignKey("CityId")]
        public int CityId { get; set; }
        public City City { get; set; }

        [Required]
        [ForeignKey("StreetId")]
        public int StreetId { get; set; }
        public Street Street { get; set; }

        [Required]
        [ForeignKey("HouseId")]
        public int HouseId { get; set; }
        public House House { get; set; }

        [ForeignKey("RoomId")]
        public int RoomId { get; set; }
        public Room Room { get; set; }
    }
}
