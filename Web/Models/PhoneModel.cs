using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class PhoneModel : IWebModel
    {
        [StringLength(50, MinimumLength = 4, ErrorMessageResourceName = "BadLength", ErrorMessageResourceType = typeof(Resources.Controllers.PersonsController))]
        public string Number { get; set; }
        [Range(0, 1, ErrorMessageResourceName = "Range", ErrorMessageResourceType = typeof(Resources.Controllers.PersonsController))]
        public byte PhoneType { get; set; }
    }
}
