using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;
using System.Text;
using System.Text.Json.Serialization;

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
        public int EmpresaID { get; set; }

        [NotMapped]
        [JsonIgnore]
        public List<NFe> NFes { get; set; }

        [NotMapped]
        [JsonIgnore]
        public List<ProcessoUpload> ProcessosUpload { get; set; }
    }
}
