using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrossCutting.Models
{
    public class Pagination
    {
        public int Count { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
    }
}
