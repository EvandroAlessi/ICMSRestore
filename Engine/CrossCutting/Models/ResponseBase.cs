using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrossCutting.Models
{
    public class ResponseBase<T> where T : class
    {
        public List<T> List { get; set; }
        public Pagination Pagination { get; set; }
    }
}
