using CrossCutting.ResultModels.Commom;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CrossCutting.ResultModels
{
    /// <summary>
    /// Este registro deve conter as notas fiscais de devoluções de vendas ocorridas no mesmo mês em que foi computada a saída da mesma mercadoria.
    /// Devoluções de vendas são entradas que têm por objeto anular os efeitos da operação original da qual resultou a saída da mercadoria.
    /// </summary>
    public partial class NFeDevolOutroEstado : NFeDevolBase
    {
        /// <summary>
        /// Texto fixo contendo 1320
        /// </summary>
        [MaxLength(4)]
        [MinLength(4)]
        [DefaultValue("1320")]
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
