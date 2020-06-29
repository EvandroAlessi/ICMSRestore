using System.ComponentModel.DataAnnotations;

namespace CrossCutting.ResultModels.Commom
{
    public partial class NFeSaidaBase : InfoBase
    {
        /// <summary>
        /// Quantidade do produto na saida
        /// Informar a quantidade do item convertida na mesma unidade de medida declarada no campo B09[UNID_ITEM] do Registro 1000
        /// </summary>
        [Required]
        [RegularExpression(@"^(0|-?\d{0,9}(\.\d{0,3})?)$")]
        public double QTD_SAIDA { get; set; }

        /// <summary>
        /// Valor unitário do item
        /// Informar o valor unitário líquido do item
        /// </summary>
        [Required]
        [RegularExpression(@"^(0|-?\d{0,9}(\.\d{0,2})?)$")]
        public double VL_UNIT_ITEM { get; set; }

        /// <summary>
        /// Valor do ICMS efetivo na saida
        /// É o resultado da multiplicação da alíquota interna da
        /// mercadoria sobre o valor da operação de venda a consumidor final, ou na hipótese de operação 
        /// beneficiada com redução da base de cálculo, sobre a base de cálculo reduzida
        /// </summary>
        [Required]
        [RegularExpression(@"^(0|-?\d{0,9}(\.\d{0,2})?)$")]
        public double VL_ICMS_EFET { get; set; }
    }
}
