using System;

namespace CrossCutting.SerializationModels
{
    public class Identificacao
    {
        public int cUF { get; set; }
        public int cNF { get; set; }
        public string natOp { get; set; }
        public int indPag { get; set; }
        public string mod { get; set; }
        public int serie { get; set; }
        public int nNF { get; set; }
        public DateTime dhEmi { get; set; }
        public DateTime? dhSaiEnt { get; set; }

    }
}
