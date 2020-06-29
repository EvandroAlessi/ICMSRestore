using System.ComponentModel.DataAnnotations;

namespace CrossCutting.ResultModels.Commom
{
    public partial class InfoBase
    {
        /// <summary>
        /// Data de Emissão do documento fiscal 
        /// Informar a mesma data de emissão declarada no documento fiscal
        /// Formato: DDMMAAAA
        /// </summary>
        [Required]
        [MaxLength(8)]
        public virtual string DT_DOC { get; set; }

        /// <summary>
        /// Código da Situação Tributária
        /// </summary>
        [Required]
        [MaxLength(3)]
        public virtual string CST_CSOSN { get; set; }

        /// <summary>
        /// Chave de acesso do documento fiscal eletrônico
        /// </summary>
        [Required]
        [MaxLength(44)]
        [MinLength(44)]
        public virtual string CHAVE { get; set; }

        /// <summary>
        /// Número do documento fiscal
        /// </summary>
        [Required]
        [MaxLength(9)]
        public virtual string N_NF { get; set; }

        /// <summary>
        /// CNPJ Emitente 
        /// 
        /// </summary>
        [Required]
        [MaxLength(14)]
        [MinLength(14)]
        public virtual string CNPJ_EMIT { get; set; }

        /// <summary>
        /// UF emitente 
        /// </summary>
        [Required]
        [MaxLength(2)]
        [MinLength(2)]
        public virtual string UF_EMIT { get; set; }

        /// <summary>
        /// CNPJ do destinatário
        /// Informar o CNPJ do destinatário declarado no documento fiscal
        /// </summary>
        [Required]
        [MaxLength(14)]
        [MinLength(14)]
        public virtual string CNPJ_DEST { get; set; }

        /// <summary>
        /// UF do Destinatário
        /// </summary>
        [Required]
        [MaxLength(2)]
        [MinLength(2)]
        public virtual string UF_DEST { get; set; }

        /// <summary>
        /// Código fiscal de operação e prestação
        /// </summary>
        [Required]
        [MaxLength(4)]
        [MinLength(4)]
        public virtual string CFOP { get; set; }

        /// <summary>
        /// Número do item do produto no documento fiscal
        /// </summary>
        [Required]
        [MaxLength(3)]
        public virtual string N_ITEM { get; set; }

        /// <summary>
        /// Unidade de medida do item 
        /// Deve ser informada a mesma unidade de medida utilizada
        /// para quantificação do estoque declarada no campo B09 do registro 1000
        /// </summary>
        [Required]
        [MaxLength(10)]
        public virtual string UNID_ITEM { get; set; }
    }

    public partial class BaseDevolution : InfoBase
    {
        /// <summary>
        /// Quantidade do item devolvida 
        /// Informar a quantidade do item devolvido convertido na mesma unidade de medida declarada no campo B09[UNID_ITEM] do Registro 1000
        /// </summary>
        [Required]
        [RegularExpression(@"^(0|-?\d{0,9}(\.\d{0,3})?)$")]
        public virtual double QTD_DEVOLVIDA { get; set; }

        /// <summary>
        /// Valor unitário do item
        /// Informar o valor unitário líquido do item
        /// </summary>
        [Required]
        [RegularExpression(@"^(0|-?\d{0,9}(\.\d{0,2})?)$")]
        public virtual double VL_UNIT_ITEM { get; set; }

        /// <summary>
        /// Valor do ICMS efetivo na saida
        /// É o resultado da multiplicação da alíquota vigente para as operações internas sobre o valor da operação de venda a consumidor final
        /// </summary>
        [Required]
        [RegularExpression(@"^(0|-?\d{0,9}(\.\d{0,2})?)$")]
        public virtual double VL_ICMS_EFETIVO { get; set; }

        /// <summary>
        /// Chave de acesso do documento fiscal referenciado
        /// Corresponde à chave de acesso do documento fiscal que acobertou a mercadoria que está sendo devolvida
        /// </summary>
        [Required]
        [MaxLength(44)]
        [MinLength(44)]
        public virtual string CHAVE_REF { get; set; }

        /// <summary>
        /// Número do item no documento fiscal referenciado
        /// </summary>
        [Required]
        [MaxLength(3)]
        public virtual string N_ITEM_REF { get; set; }
    }
}
