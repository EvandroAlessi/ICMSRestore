using CrossCutting.ResultModels.Commom;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CrossCutting.ResultModels
{
    /// <summary>
    /// Este registro deve ser informado para identificar a totalização das notas fiscais de venda emitidas em operações internas que trata o art. 119 do
    /// RICMS/2017 declaradas no registro 1410 do produto que foi identificado no registro 1000
    /// </summary>
    public partial class TotalSaidaArt119 : TotalBase
    {
        /// <summary>
        /// Texto fixo contendo 1400
        /// </summary>
        [MaxLength(4)]
        [MinLength(4)]
        [DefaultValue("1400")]
        public string REG { get; set; }

        /// <summary>
        /// Valor de confronto do ICMS das entradas 
        /// Obtido pela multiplicação do campo J02 [QTD_TOT_SAÍDA] pelo campo D06[VL_UNIT_MED_ICMS_SUPORT_ENTR] do registro 1100
        /// </summary>
        [Required]
        [RegularExpression(@"^(0|-?\d{0,9}(\.\d{0,2})?)$")]
        public double VL_CONFRONTO_ICMS_ENTRADA { get; set; }
    }
}
