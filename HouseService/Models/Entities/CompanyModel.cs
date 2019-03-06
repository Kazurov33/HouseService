using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HouseService.Models.Entities
{
    public class CompanyModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Полное наименование")]
        public string Name { get; set; }

        [Display(Name = "Сокращенное наименование")]
        [Required(ErrorMessage = "Поле «Сокращенное наименование» обязательное")]
        public string ShortName { get; set; }

        [Display(Name = "Телефон")]
        [Required(ErrorMessage = "Поле «Телефон» обязательное")]
        public string Phone { get; set; }

        [Display(Name = "Юридический адрес")]
        public string LegalAddress { get; set; }

        [Display(Name = "Фактический адрес")]
        public string ActualAddress { get; set; }

        [Display(Name = "Почтовый адрес")]
        public string MailingAddress { get; set; }

        [Display(Name = "ОГРН")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Неверно введен ОГРН")]
        public string OGRN { get; set; }

        [Display(Name = "ИНН")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Неверно введен ИНН")]
        public string INN { get; set; }

        [Display(Name = "КПП")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Неверно введен КПП")]
        public string KPP { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }



    }
}