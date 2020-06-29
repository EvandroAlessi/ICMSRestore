using CrossCutting.ResultModels.Commom;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CrossCutting.ResultModels
{
    /// <summary>
    /// Este registro deve conter todas as notas fiscais de saída emitidas para outros estados, do produto declarado no registro 1000. Este registro visa a
    /// atender à regra disposta no art. 6º do Anexo IX do RICMS/17
    /// </summary>
    public partial class NFeOutroEstado : NFeSaidaBase
    {
        /// <summary>
        /// Texto fixo contendo 1310
        /// </summary>
        [MaxLength(4)]
        [MinLength(4)]
        [DefaultValue("1310")]
        public string REG { get; set; }
    }
}
