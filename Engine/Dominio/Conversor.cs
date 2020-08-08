using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Conversor
    {
        public int ID { get; set; }

        public int EmpresaID { get; set; }

        public string Unidade { get; set; }

        public string UnidadeResultante { get; set; }

        public double FatorConversao { get; set; }

        public string cProd { get; set; }

        public int NCM { get; set; }
    }
}
