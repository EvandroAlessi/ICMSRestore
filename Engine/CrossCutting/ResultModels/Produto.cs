using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CrossCutting.ResultModels
{
    /// <summary>
    /// Registro 1000 – Identificação analítica do produto.
    /// Este registro deve conter os códigos das mercadorias e as respectivas descrições atribuídas pelo contribuinte para a identificação da mercadoria que
    /// integra o ciclo de aquisição e comercialização do estabelecimento.
    /// </summary>
    public partial class Produto
    {
        /// <summary>
        /// Texto fixo contendo 1000
        /// </summary>
        [Required]
        [MaxLength(4)]
        [MinLength(4)]
        [DefaultValue("1000")]
        public string REG { get { return "1000"; } set { } }

        /// <summary>
        /// Indicador de produto sujeito ao Fundo de Combate à Pobreza
        /// 0 – Produto não está sujeito ao FECOP
        /// 1 – Produto está sujeito ao FECOP
        /// </summary>
        [Required]
        [DefaultValue(0)]
        public int IND_FECOP { get; set; }

        /// <summary>
        /// Sequência de números e/ou letras atribuída pelo contribuinte para a identificação da mercadoria que integra o ciclo de aquisição,
        /// produção e venda do estabelecimento.Deve corresponder ao código do item declarado no campo 2 do registro 0200 da EFD.É vedada a reutilização do código
        /// de item.O código de item não pode ser alterado, caso ocorra, o contribuinte deverá informar o registro 0205 da EFD. No caso de contribuinte que não declara
        /// a EFD, informar o código do item utilizado para identificar o produto nas suas operações.
        /// </summary>
        [Required]
        [MaxLength(60)]
        public string COD_ITEM { get; set; }

        /// <summary>
        /// Corresponde ao código GTIN/EAN da menor unidade do produto
        /// </summary>
        [MaxLength(14)]
        public string COD_BARRAS { get; set; }

        /// <summary>
        /// Código do produto conforme tabela da Agência Nacional de Petróleo - ANP
        /// </summary>
        [MaxLength(9)]
        [MinLength(9)]
        public string COD_ANP { get; set; }

        /// <summary>
        /// Informar a NCM da mercadoria com 8 dígitos
        /// </summary>
        [Required]
        [MaxLength(8)]
        [MinLength(8)]
        public string NCM { get; set; }

        /// <summary>
        /// Informar o código CEST específico para cada item de
        /// mercadoria comercializada sujeita à substituição tributária
        /// </summary>
        [Required]
        [MaxLength(7)]
        [MinLength(7)]
        public string CEST { get; set; }

        /// <summary>
        /// Compreende: nome, marca, tipo, modelo, série, espécie e
        /// demais elementos que permitam a perfeita identificação da mercadoria
        /// </summary>
        [Required]
        [MaxLength(150)]
        public string DESCR_ITEM { get; set; }

        /// <summary>
        /// Deve corresponder à unidade de medida utilizada na quantificação do estoque
        /// </summary>
        [Required]
        [MaxLength(10)]
        public string UNID_ITEM { get; set; }

        /// <summary>
        /// Corresponde à alíquota da mercadoria prevista para as
        /// operações internas, incluído o FECOP.Caso a mercadoria
        /// seja beneficiada com redução da base de cálculo, adotar a
        /// carga tributária efetiva
        /// </summary>
        [Required]
        [RegularExpression(@"^(0|-?\d{0,2}(\.\d{0,2})?)$")]
        public double ALIQ_ICMS_ITEM { get; set; }

        /// <summary>
        /// Informar o percentual da aliquota do ICMS destinada ao FECOP
        /// </summary>
        [Required]
        [RegularExpression(@"^(0|-?\d{0,1}(\.\d{0,2})?)$")]
        public double ALIQ_FECOP { get; set; }

        /// <summary>
        /// Quantidade total do item adquirido no período
        /// Informar o mesmo valor do campo TotalEntrada.QTD_TOT_ENTRADA do registro 1100
        /// </summary>
        [Required]
        [RegularExpression(@"^(0|-?\d{0,9}(\.\d{0,3})?)$")]
        public double QTD_TOT_ENTRADA { get; set; }

        /// <summary>
        /// Quantidade total de saídas do item no período 
        /// Equação: QTD_TOT_SAIDA = TotalSaida.QTD_TOT_SAIDA + TotalSaidaOutroEstado.QTD_TOT_SAIDA + TotalSaidaArt119.QTD_TOT_SAIDA + TotalSaidaSimples.QTD_TOT_SAIDA
        /// </summary>
        [Required]
        [RegularExpression(@"^(0|-?\d{0,9}(\.\d{0,3})?)$")]
        public double QTD_TOT_SAIDA { get; set; }
    }
}
