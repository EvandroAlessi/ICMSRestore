using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

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
    }
}
