using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CrossCutting.ResultModels
{
    /// <summary>
    /// Este registro deve ser informado para identificar a totalização das notas fiscais de saída interna destinadas a contribuinte do Simples Nacional
    /// declaradas nos registros 1510 do produto que foi identificado no registro 1000
    /// </summary>
    public partial class TotalSaidaSimples 
    {
        /// <summary>
        /// Texto fixo contendo 1500
        /// </summary>
        [MaxLength(4)]
        [MinLength(4)]
        [DefaultValue("1500")]
        public string REG { get; set; }

        /// <summary>
        /// Quantidade total de saídas destinadas a contribuintes do Simples Nacional
        /// Somatória dos campos M13 [QTD_SAÍDA], menos a somatória dos campos M13d[QTD_DEVOLVIDA], 
        /// dos produtos comercializados nas operações de saídas para contribuinte do simples nacional
        /// </summary>
        [Required]
        [RegularExpression(@"^(0|-?\d{0,9}(\.\d{0,2})?)$")]
        public double QTD_TOT_SAIDA { get; set; }

        /// <summary>
        /// É o valor do ICMS ST a recuperar por unidade
        /// Valor obtido a partir do seguinte cálculo:
        /// (D04 /(1+MVA)) x(MVA/Coeficiente) x(B10)
        /// Onde: 
        ///     D04 é o campo VL_BC_ICMSST_UNIT_MED
        ///     MVA é a MVA utilizada para retenção do ICMS ST
        ///     Coeficiente é o percentual de redução a ser aplicado sobre a MVA (70% se alíquota de 18%, 50% se alíquota de 12%
        ///     B10 é campo ALIQ_INTERNA
        /// </summary>
        [Required]
        [RegularExpression(@"^(0|-?\d{0,9}(\.\d{0,2})?)$")]
        public double VL_ICMSST_UNIT_ENTR { get; set; }

        /// <summary>
        /// Apuração do ICMS ST a recuperar ou a ressarcir
        /// Valor obtido pela multiplicação do campo L02 [TOT_QTD_SAÍDA] pelo campo L03[VL_ICMSST_UNIT_ENTR]
        /// </summary>
        [Required]
        [RegularExpression(@"^(0|-?\d{0,9}(\.\d{0,2})?)$")]
        public double APUR_ICMSST_RECUPERAR_RESSARCIR { get; set; }
    }
}
