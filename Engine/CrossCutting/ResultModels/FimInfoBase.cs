using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CrossCutting.ResultModels
{
    /// <summary>
    /// Este registro destina-se a identificar o encerramento do Bloco 1 e a quantidade de linhas (registros) existentes no bloco
    /// </summary>
    public partial class FimInfoBase
    {
        /// <summary>
        /// Texto fixo contendo 1999
        /// </summary>
        [MaxLength(4)]
        [MinLength(4)]
        [DefaultValue("1999")]
        public string REG { get; set; }

        /// <summary>
        /// A quantidade de linhas a ser informada deve considerar
        /// também o próprio registro 1999
        /// </summary>
        [MaxLength(9)]
        public string QTD_LIN { get; set; }
    }
}
