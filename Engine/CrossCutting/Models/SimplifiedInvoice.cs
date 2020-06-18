using System;

namespace CrossCutting.Models
{
    public class SimplifiedInvoice
    {
        public int ID { get; set; }

        public int ProcessoID { get; set; }

        public string CNPJ { get; set; }
        public bool Entrada { get; set; }

        public int cNF { get; set; }
        public int nNF { get; set; }

        public DateTime dhEmi { get; set; }

        public DateTime? dhSaiEnt { get; set; }

        public double? vNF_TOTAL { get; set; }
    }
}
