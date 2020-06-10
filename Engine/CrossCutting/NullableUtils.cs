using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CrossCutting
{
    public class NullableUtils
    {
        public static dynamic TestValue(dynamic value)
        {
            return value is null ? "null" : value;
        }

        public static double DoubleTreatment(double? value)
        {
            return Double.Parse(value?.ToString(), CultureInfo.CreateSpecificCulture("en-US"));
        }
    }
}
