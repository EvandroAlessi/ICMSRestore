using System;
using System.ComponentModel.DataAnnotations;

namespace Dominio
{
    public class MVA
    {
        [Key]
        public int ID { get; set; }

        public int CEST { get; set; }

        public long NCM_SH { get; set; }

        public string Descricao { get; set; }

        public double MVA_ST { get; set; }

        public DateTime DataInicial { get; set; }

        public DateTime DataFinal { get; set; }
    }
}
