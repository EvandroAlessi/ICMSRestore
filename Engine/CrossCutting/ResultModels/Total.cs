using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CrossCutting.ResultModels
{
    /// <summary>
    /// Apuração e encerramento do arquivo
    /// </summary>
    public partial class Total
    {
        /// <summary>
        /// Texto fixo contendo 9000
        /// </summary>
        [Required]
        [MaxLength(4)]
        [MinLength(4)]
        [DefaultValue("9000")]
        public string REG { get; set; }

        /// <summary>
        /// Valor a recuperar ou a ressarcir nas saídas para consumidor final
        /// Resultado positivo da diferença da somatória dos valores declarados no campo F07[APUR_ICMSST_RECUPERAR_RESSARCIR]
        /// pela somatória dos valores declarados no campo F08[APUR_ICMSST_COMPLEMENTAR] de todos os registros1200 [Totalizador das saidas para consumidor final]
        /// Observação: Preencher com zero quando o resultado da equação X02=∑[F07]-∑[F08] for negativo
        /// </summary>
        [Required]
        [RegularExpression(@"^(0|-?\d{0,2}(\.\d{0,2})?)$")]
        public double REG1200_ICMSST_RECUPERAR_RESSARCIR { get; set; }

        /// <summary>
        /// Valor a complementar nas saídas para consumidor final
        /// Resultado negativo da diferença da somatória dos valores declarados no campo F07[APUR_ICMSST_RECUPERAR_RESSARCIR] pela somatória
        /// dos valores declarados no campo F08[APUR_ICMSST_COMPLEMENTAR] de todos os registros 1200 [Totalizador das saidas para consumidor final]
        /// Observação: Preencher com zero quando o resultado da equação X03=∑[F07]-∑[F08] for positivo
        /// </summary>
        [Required]
        [RegularExpression(@"^(0|-?\d{0,2}(\.\d{0,2})?)$")]
        public double REG1200_ICMSST_COMPLEMENTAR { get; set; }

        /// <summary>
        /// Valor a recuperar ou a ressarcir nas saídas para outros estados
        /// Somatória dos valores declarados no campo H06[APUR_ICMSST_RECUPERAR_RESSARCIR] de todos os registros 1300 (Totalizador das saídas para outros estados)
        /// </summary>
        [Required]
        [RegularExpression(@"^(0|-?\d{0,2}(\.\d{0,2})?)$")]
        public double REG1300_ICMSST_RECUPERAR_RESSARCIR { get; set; }

        /// <summary>
        /// Valor a recuperar ou a ressarcir nas saidas de que trata o art. 119
        /// Somatória dos valores declarados no campo J05[APUR_ICMSST_RECUPERAR_RESSARCIR] de todos os registros 1400 (Totalizador das saídas que trata o art. 119)
        /// </summary>
        [Required]
        [RegularExpression(@"^(0|-?\d{0,2}(\.\d{0,2})?)$")]
        public double REG1400_ICMSST_RECUPERAR_RESSARCIR { get; set; }

        /// <summary>
        /// Somatória dos valores declarados no campo L04[APUR_ICMSST_RECUPERAR_RESSARCIR] de todos os registros 1500 (Totalizador das saídas para contribuinte do Simples Nacional)
        /// </summary>
        [Required]
        [RegularExpression(@"^(0|-?\d{0,2}(\.\d{0,2})?)$")]
        public double REG1500_ICMSST_RECUPERAR_RESSARCIR { get; set; }

        /// <summary>
        /// Valor a ressarcir do FECOP
        /// 
        /// </summary>
        [Required]
        [RegularExpression(@"^(0|-?\d{0,2}(\.\d{0,2})?)$")]
        public double REG9000_FECOP_RESSARCIR { get; set; }

        /// <summary>
        /// Valor a complementar do FECOP
        /// Resultado negativo da somatória dos valores declarados nos campos F09[APUR_FECOP_RESSARCIR] dos registros 1200 e
        /// H07[APUR_FECOP_RESSARCIR] dos registros 1300 menos a somatória dos valores declarados no campo F10[APUR_FECOP_COMPLEMENTAR] dos registros 1200
        /// Observação: Preencher com zero quando o resultado da equação X08 =∑[F09]+∑[H07]-∑[F10] for positivo
        /// </summary>
        [Required]
        [RegularExpression(@"^(0|-?\d{0,2}(\.\d{0,2})?)$")]
        public double REG9000_FECOP_COMPLEMENTAR { get; set; }
    }
}
