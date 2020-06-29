using CrossCutting.ResultModels.Commom;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CrossCutting.ResultModels
{
    /// <summary>
    /// Este registro deve conter as notas fiscais de devolução de venda ocorridas no mesmo mês em que foi computada a saída da mesma mercadoria.
    /// </summary>
    public partial class NFeDevolSaida : NFeDevolBase
    {
        /// <summary>
        /// Texto fixo contendo 1220
        /// </summary>
        [MaxLength(4)]
        [MinLength(4)]
        [DefaultValue("1220")]
        public string REG { get; set; }

        /// <summary>
        /// Valor do ICMS efetivo na saida
        /// É o resultado da multiplicação da alíquota vigente para as operações internas sobre o valor da operação de venda a consumidor final
        /// </summary>
        [Required]
        [RegularExpression(@"^(0|-?\d{0,9}(\.\d{0,2})?)$")]
        public double VL_ICMS_EFETIVO { get; set; }
    }
}
