using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

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

        [NotMapped]
        [JsonIgnore]
        public List<Processo> Processos { get; set; }
    }
}
