using CrossCutting.ResultModels.Commom;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CrossCutting.ResultModels
{
    /// <summary>
    /// Este registro deve conter as notas fiscais de devoluções de compras ocorridas no mesmo mês em que foi computada a entrada da mesma mercadoria.
    /// Devoluções de compras são saídas que têm por objeto anular os efeitos da operação de entrada original da qual resultou o recebimento da mercadoria.
    /// </summary>
    public partial class NFeEntradaDevol : NFeDevolBase
    {
        /// <summary>
        /// Texto fixo contendo 1120
        /// </summary>
        [MaxLength(4)]
        [MinLength(4)]
        [DefaultValue("1120")]
        public string REG { get { return "1120"; } set { } }

        /// <summary>
        /// Base de cálculo do ICMS ST
        /// Informar o valor da base de cálculo utilizada para o cálculo do ICMS ST
        /// </summary>
        [Required]
        [RegularExpression(@"^(0|-?\d{0,9}(\.\d{0,2})?)$")]
        public double VL_BC_ICMS_ST { get; set; }

        /// <summary>
        /// Valor do ICMS do item suportado na entrada
        /// Corresponde ao valor total do imposto suportado pelo
        /// contribuinte substituído, abrangendo o imposto incidente
        /// na operação própria do substituto e o retido por ST, incluída a parcela do FECOP, se houver.
        /// </summary>
        [Required]
        [RegularExpression(@"^(0|-?\d{0,9}(\.\d{0,2})?)$")]
        public double VL_ICMS_SUPORT_ENTR { get; set; }
    }
}
