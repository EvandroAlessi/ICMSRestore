using Dominio;
using System.ComponentModel.DataAnnotations;

namespace CrossCutting.Models
{
    public partial class UserPUT : Usuario
    {
        [Required]
        public string SenhaNova { get; set; }
    }
}
