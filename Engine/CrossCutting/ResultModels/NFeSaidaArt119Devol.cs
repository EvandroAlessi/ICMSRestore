using CrossCutting.ResultModels.Commom;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CrossCutting.ResultModels
{
    /// <summary>
    /// Este registro deve conter as notas fiscais de devoluções de vendas ocorridas no mesmo mês em que foi computada a saída da mesma mercadoria.
    /// </summary>
    public partial class NFeSaidaArt119Devol : NFeDevolBase
    {
        /// <summary>
        /// Texto fixo contendo 1420
        /// </summary>
        [MaxLength(4)]
        [MinLength(4)]
        [DefaultValue("1420")]
        public string REG { get; set; }

        /// <summary>
        /// Valor do ICMS efetivo na saida
        /// É o valor do imposto da própria operação interestadual destacado na nota fiscal.
        /// </summary>
        [Required]
        [RegularExpression(@"^(0|-?\d{0,9}(\.\d{0,2})?)$")]
        public double VL_ICMS_EFET { get; set; }
    }
}
