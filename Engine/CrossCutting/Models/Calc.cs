using System;
using System.Collections.Generic;
using System.Text;

namespace CrossCutting.Models
{
    public class SimpleResponse
    {
        public int Month { get; set; }

        public int Year { get; set; }

        public string NCM { get; set; }

        public double vProd { get; set; }
    }

    public class ProductMedia
    {
        public string Name { get; set; }

        public int? NCM { get; set; }

        public string MonthYear { get; set; }

        public int? TotalResults { get; set; }

        public double? LowerValue { get; set; }

        public double? HighestValue { get; set; }

        public double? Media { get; set; }

        public double? TotalValue { get; set; }

        public double? MediaEntry { get; set; }
    }
}
