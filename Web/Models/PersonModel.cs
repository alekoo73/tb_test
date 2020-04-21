using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Web.Attributes;
//using Web.Resources;

namespace Web.Models
{
    public class PersonModel: IWebModel
    {
        public int Id { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.Controllers.PersonsController))]
        [StringLength(50, MinimumLength = 2, ErrorMessageResourceName = "BadLength",ErrorMessageResourceType =typeof(Resources.Controllers.PersonsController))]
        [RegularExpression("^(([a-z]{2,})|([ა-ჰ]{2,}))$", ErrorMessageResourceName = "GeoOrLatin", ErrorMessageResourceType = typeof(Resources.Controllers.PersonsController))]
        public string FirstName { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.Controllers.PersonsController))]
        [StringLength(50, MinimumLength = 2, ErrorMessageResourceName = "BadLength", ErrorMessageResourceType = typeof(Resources.Controllers.PersonsController))]
        [RegularExpression("^(([a-z]{2,})|([ა-ჰ]{2,}))$", ErrorMessageResourceName = "GeoOrLatin", ErrorMessageResourceType = typeof(Resources.Controllers.PersonsController))]
        public string LastName { get; set; }
        [Range(0,1, ErrorMessageResourceName = "Range", ErrorMessageResourceType = typeof(Resources.Controllers.PersonsController))]
        public byte Sex { get; set; }
        [StringLength(11, ErrorMessageResourceName = "BadLength", ErrorMessageResourceType = typeof(Resources.Controllers.PersonsController))]
        public string IdNumber { get; set; }
        [BirthDate]
        public DateTime BirthDate { get; set; }
        [Required]
        public int SettlementId { get; set; }

        public PhoneModel[] Phones { get; set; }


    }
}
