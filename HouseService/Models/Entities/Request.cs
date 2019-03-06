using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HouseService.Models.Entities
{
    public class Request
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? FinishDate { get; set; }

        //[Display(Name = "Контакт для связи")]
        //public string Contact { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [ForeignKey("UserId")]
        public string UserId { get; set; }
        public AppUser User { get; set; }

        public int? RequestStatusId { get; set; }//!!
        public RequestStatusModel RequestStatus { get; set; }

        public int? RequestTypeId { get; set; }//!!
        public RequestTypeModel RequestType { get; set; }

        public int? AdressId { get; set; }//!!
        public AdressModel Adress { get; set; }

        [ForeignKey("ContactId")]
        public int ContactId { get; set; }
        public Contact Contact { get; set; }
    }
}
