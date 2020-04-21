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
    public class RelationModel : IWebModel
    {
        [Range(0, 3, ErrorMessageResourceName = "Range", ErrorMessageResourceType = typeof(Resources.Controllers.PersonsController))]
        public byte RelationType { get; set; }
        public int RelatedPerson1Id { get; set; }
        public int RelatedPerson2Id { get; set; }

    }
}
