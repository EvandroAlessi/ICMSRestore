using System;
using System.Collections.Generic;
using System.Text;

namespace CrossCutting
{
    public class NullableUtils
    {
        public static dynamic TestValue(dynamic value)
        {
            return value is null ? "null" : value;
        }
    }
}
