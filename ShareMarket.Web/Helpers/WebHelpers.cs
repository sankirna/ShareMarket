using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ShareMarket.Web.Helpers
{
    public static class WebHelpers
    {

        public static SelectList ToSelectList(this List<string> enumObj)
        {
            var values = from enumValue in enumObj
                         select new { Id = enumObj.IndexOf(enumValue) + 1, Name = enumValue };
            return new SelectList(values, "Name", "Name");
        }

    }
}