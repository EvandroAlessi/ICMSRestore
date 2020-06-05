using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dominio
{
    public partial class Empresa
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string CNPJ { get; set; }

        public string Nome { get; set; }

        public string Cidade { get; set; }

        public string UF { get; set; }
    }
}
