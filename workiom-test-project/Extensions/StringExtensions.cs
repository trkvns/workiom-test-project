using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace workiom_test_project.Extensions
{
    public static class StringExtensions
    {
        public static dynamic ToDefaultValue(this string value)
        {
            if (!String.IsNullOrEmpty(value))
            {
                value = value.ToLower();

                if (value == "text")
                    return String.Empty;

                if (value == "number")
                    return -1;

                if (value == "date")
                    return DateTime.MinValue;
            }

            return String.Empty;
        }
    }
}
