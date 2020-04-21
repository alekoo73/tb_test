using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Classes
{
    public static class Statics
    {
        public static readonly List<CultureInfo> SupportedCultures = new List<CultureInfo>
            {
        new CultureInfo("en-US"),
        new CultureInfo("ka-GE")
            };
    }
}
