using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CrossCutting.ResultModels
{
    /// <summary>
    /// Este registro é obrigatório e corresponde ao primeiro registro do arquivo digital. Tem o objetivo de identificar o período de apuração do arquivo e o
    /// contribuinte substituído tributário que deseja recuperar/ressarcir ou complementar o ICMS ST ou o Fecop retidos na etapa anterior.Para tanto o contribuinte
    /// deverá elaborar um único registro 0000 por arquivo
    /// </summary>
    public partial class Contribuinte
    {
        /// <summary>
        /// Texto fixo contendo 0000
        /// </summary>
        [Required]
        [MaxLength(4)]
        [MinLength(4)]
        [DefaultValue("0000")]
        public string REG { get; set; }

        /// <summary>
        /// Código da versão do leiaute do arquivo
        /// </summary>
        [Required]
        [MaxLength(3)]
        [MinLength(3)]
        public string COD_VERSAO { get; set; }

        /// <summary>
        /// Informar o mês de referência e o ano de apuração do arquivo.
        /// Formato: MMAAAA
        /// </summary>
        [Required]
        [MaxLength(6)]
        [MinLength(6)]
        public string MES_ANO { get; set; }

        /// <summary>
        /// Informar os 14 dígitos do CNPJ sem traço e barra
        /// </summary>
        [Required]
        [MaxLength(14)]
        [MinLength(14)]
        public string CNPJ { get; set; }

        /// <summary>
        /// Informar os 10 dígitos do CAD/ICMS sem traço e barra
        /// </summary>
        [Required]
        [MaxLength(10)]
        [MinLength(10)]
        public string IE { get; set; }

        /// <summary>
        /// Nome empresarial
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string NOME { get; set; }

        /// <summary>
        /// 0 – Arquivo original
        /// 1 – Arquivo substituto
        /// </summary>
        [Required]
        [DefaultValue(0)]
        public int CD_FIN { get; set; }

        /// <summary>
        /// Número do regime especial
        /// </summary>
        [MaxLength(10)]
        public string N_REG_ESPECIAL { get; set; }

        /// <summary>
        /// Informar o CNPJ do estabelecimento que centraliza as aquisições dos
        /// produtos sujeitos à substituição tributária
        /// </summary>
        [MaxLength(14)]
        [MinLength(14)]
        public string CNPJ_CD { get; set; }

        /// <summary>
        /// Informar a Inscrição Estadual do estabelecimento que centraliza as
        /// aquisições dos produtos sujeitos à substituição tributária
        /// </summary>
        [MaxLength(10)]
        [MinLength(10)]
        public string IE_CD { get; set; }
    }
}
