using System.Collections.Generic;

namespace CrossCutting.ResultModels
{
    public partial class ArquivoFinal
    {
        public Contribuinte Contribuinte { get; set; }

        public Produto Produto { get; set; }

        //public InventarioProduto InventarioProduto { get; set; }

        public TotalEntrada TotalEntrada { get; set; }

        public List<NFeEntrada> NFesEntrada { get; set; }

        public List<NFeEntradaDevol> NFesEntradaDevol { get; set; }

        public TotalSaida TotalSaida { get; set; }

        public List<NFeSaida> NFesSaida { get; set; }

        public List<NFeSaidaDevol> NFesSaidaDevol { get; set; }

        public FimInfoBase FimInfoBase { get; set; }

        public Total Total { get; set; }

        public FimArquivo FimArquivo { get; set; }
    }
}
