using System;
using System.ComponentModel.DataAnnotations;

namespace Dominio
{
    public class ProcessoUpload
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int ProcessoID { get; set; }

        [Required]
        public string PastaZip { get; set; }

        [Required]
        public int QntArq { get; set; }

        [Required]
        public bool Ativo { get; set; }

        public DateTime? DataInicio { get; set; }

        public bool Entrada { get; set; }
    }
}
