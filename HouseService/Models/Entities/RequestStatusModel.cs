using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HouseService.Models.Entities
{
    public class RequestStatusModel /*---------- Статус заявки ----------*/
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Статус заявки")]
        [Required]
        public string Name { get; set; }

        public ICollection<Request> Requests { get; set; }
    }
}
