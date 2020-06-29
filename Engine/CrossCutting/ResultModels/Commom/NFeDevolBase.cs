using System.ComponentModel.DataAnnotations;

namespace CrossCutting.ResultModels.Commom
{
    public partial class NFeDevolBase : InfoBase
    {
        /// <summary>
        /// Quantidade do item devolvida 
        /// Informar a quantidade do item devolvido convertido na mesma unidade de medida declarada no campo B09[UNID_ITEM] do Registro 1000
        /// </summary>
        [Required]
        [RegularExpression(@"^(0|-?\d{0,9}(\.\d{0,3})?)$")]
        public double QTD_DEVOLVIDA { get; set; }

        /// <summary>
        /// Valor unitário do item
        /// Informar o valor unitário líquido do item
        /// </summary>
        [Required]
        [RegularExpression(@"^(0|-?\d{0,9}(\.\d{0,2})?)$")]
        public double VL_UNIT_ITEM { get; set; }
        /// <summary>
        /// Chave de acesso do documento fiscal referenciado
        /// Corresponde à chave de acesso do documento fiscal que acobertou a mercadoria que está sendo devolvida
        /// </summary>
        [Required]
        [MaxLength(44)]
        [MinLength(44)]
        public string CHAVE_REF { get; set; }

        /// <summary>
        /// Número do item no documento fiscal referenciado
        /// </summary>
        [Required]
        [MaxLength(3)]
        public string N_ITEM_REF { get; set; }
    }
}
