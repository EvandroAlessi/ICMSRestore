using System.ComponentModel.DataAnnotations;

namespace Dominio
{
    public partial class Usuario
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Senha { get; set; }

        public string Cargo { get; set; }
    }
}
