using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CrossCutting.ResultModels
{
    public partial class FimArquivo
    {
        public Contribuinte Contribuinte { get; set; }

        public Produto Produto { get; set; }

        public Total Total { get; set; }
    }
}
