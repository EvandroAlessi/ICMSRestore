using System.ComponentModel.DataAnnotations;

namespace CrossCutting.Models
{
    public partial class UserChangeRole
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
