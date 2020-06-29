using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CrossCutting.ResultModels
{
    /// <summary>
    /// Este registro deve ser preenchido pelos contribuintes enquadrados no regime do Simples Nacional, no caso de pedido de ressarcimento ou de
    /// complementação do imposto previsto no registro 1200 deste Manual.Deverão identificar, para cada item de mercadoria identificada no registro 1000, o
    /// estoque existente no último dia do mês anterior ao do mês de referência do arquivo
    /// </summary>
    public partial class InventarioProduto
    {
        /// <summary>
        /// Texto fixo contendo 1010
        /// </summary>
        [Required]
        [MaxLength(4)]
        [MinLength(4)]
        [DefaultValue("1010")]
        public string REG { get; set; }

        /// <summary>
        /// Código do item 
        /// Deve ser informado o mesmo código do item declarado no campo B03
        /// </summary>
        [Required]
        [MaxLength(60)]
        public string COD_ITEM { get; set; }

        /// <summary>
        /// Informar a unidade de medida utilizada na quantificação do estoque
        /// Deve ser a mesma unidade de medida utilizada no campo B09 do registro 1000
        /// </summary>
        [Required]
        [MaxLength(10)]
        public string UNID_ITEM { get; set; }

        /// <summary>
        /// Quantidade do produto no estoque 
        /// Quantidade do produto existente no estoque no último dia do mês anterior ao do mês de referência do arquivo
        /// </summary>
        [Required]
        [RegularExpression(@"^(0|-?\d{0,9}(\.\d{0,3})?)$")]
        public double QTD { get; set; }

        /// <summary>
        /// Valor total do produto
        /// Valor total do produto existente no estoque no último dia do mês anterior ao do mês de referência do arquivo
        /// </summary>
        [Required]
        [RegularExpression(@"^(0|-?\d{0,9}(\.\d{0,2})?)$")]
        public double VL_TOT_ITEM { get; set; }

        /// <summary>
        /// Descrição complementar 
        /// </summary>
        [MaxLength(100)]
        public string TXT_COMPL { get; set; }
    }
}
