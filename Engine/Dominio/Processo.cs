using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using System.Text;

namespace Dominio
{
    public partial class Processo
    {
        [Key]
        public int ID { get; set; }

        public string Nome { get; set; }

        public DateTime DataCriacao { get; set; }

        [Required]
        public DateTime InicioPeriodo { get; set; }

        [Required]
        public DateTime FimPeriodo { get; set; }

        [Required]
        public Empresa Empresa { get; set; }
    }
}
