using CrossCutting.ResultModels.Commom;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CrossCutting.ResultModels
{
    /// <summary>
    /// Este registro deve conter as notas fiscais de devolução de venda ocorridas no mesmo mês em que foi computada a saída da mesma mercadoria.
    /// </summary>
    public partial class NFeSaidaSimplesDevol : NFeDevolBase
    {
        /// <summary>
        /// Texto fixo contendo 1520
        /// </summary>
        [MaxLength(4)]
        [MinLength(4)]
        [DefaultValue("1520")]
        public string REG { get; set; }
    }
}
