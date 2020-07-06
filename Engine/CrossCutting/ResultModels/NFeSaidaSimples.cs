using CrossCutting.ResultModels.Commom;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CrossCutting.ResultModels
{
    /// <summary>
    /// Este registro deve conter as notas fiscais de saída interna destinadas a contribuinte do Simples Nacional do produto declarado no registro 1000, no
    /// caso de aquisição de mercadorias a que se referem as Seções VI, VII, XVIII e XXII, do Anexo IX do RICMS/17, com imposto retido calculado com a aplicação do
    /// percentual integral da MVA.
    /// </summary>
    public partial class NFeSaidaSimples : InfoBase
    {
        /// <summary>
        /// Texto fixo contendo 1510
        /// </summary>
        [MaxLength(4)]
        [MinLength(4)]
        [DefaultValue("1510")]
        public string REG { get { return "1510"; } set { } }

        /// <summary>
        /// Quantidade do produto na saída 
        /// Informar a quantidade do item convertido na mesma unidade de
        /// medida declarada no campo B09[UNID_ITEM] do Registro 1000
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
    }
}
