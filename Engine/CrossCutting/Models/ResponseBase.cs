using System.Collections.Generic;

namespace CrossCutting.Models
{
    public class ResponseBase<T> where T : class
    {
        public List<T> List { get; set; }
        public Pagination Pagination { get; set; }
    }
}
