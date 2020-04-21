using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Attributes
{
    public class BirthDateAttribute : ValidationAttribute
    {
        public BirthDateAttribute()
        {
        }

        public override bool IsValid(object value)
        {
            
                var dt = (DateTime)value;
            if (dt <=DateTime.Today.AddYears(-18))
            {
                ErrorMessageResourceType = typeof(Resources.Controllers.PersonsController);
                ErrorMessageResourceName = "InvalidBirthDate";
                return true;
            }
            return false;
        }
    }
}