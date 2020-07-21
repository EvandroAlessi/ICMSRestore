using System;
using System.ComponentModel.DataAnnotations;

namespace Dominio
{
    public class NFeRepetida
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int ProcessoID { get; set; }

        [Required]
        public int NFeID { get; set; }

        [Required]
        public string Chave { get; set; }

        [Required]
        public string XML { get; set; }

        [Required]
        public DateTime? DataHora { get; set; }

        public string StackTrace { get; set; }

        public string MensagemErro { get; set; }
    }
}
