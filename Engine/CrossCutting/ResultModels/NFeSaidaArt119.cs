using CrossCutting.ResultModels.Commom;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CrossCutting.ResultModels
{
    /// <summary>
    /// Este registro deve conter as notas fiscais emitidas em operações internas do produto que foi identificado no registro 1000. 
    /// Este registro visa a atender às regras dispostas no art. 119 do Anexo IX do RICMS/17.
    /// </summary>
    public partial class NFeSaidaArt119 : NFeSaidaBase
    {
        /// <summary>
        /// Texto fixo contendo 1410
        /// </summary>
        [MaxLength(4)]
        [MinLength(4)]
        [DefaultValue("1410")]
        public string REG { get { return "1410"; } set { } }
    }
}
