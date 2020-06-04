using System;
using System.Collections.Generic;
using System.Text;

namespace CrossCutting.SerializationModels
{
    public class ICMS00
    {
        public int orig { get; set; }
        public string CST { get; set; }
        public int modBC { get; set; }
        public double vBC { get; set; }
        public double pICMS { get; set; }
        public double vICMS { get; set; }
    }
}
