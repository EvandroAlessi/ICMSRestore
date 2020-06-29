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
        public string NCM { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public double ProductValue { get; set; }

        public double LowerValue { get; set; }

        public double HighestValue { get; set; }

        public double Media { get; set; }
    }
}
