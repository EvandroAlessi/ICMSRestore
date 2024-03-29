﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CrossCutting.ResultModels
{
    /// <summary>
    /// Este registro destina-se a identificar o encerramento do arquivo digital e a informar a quantidade de linhas (registros) existentes no arquivo
    /// </summary>
    public partial class FimArquivo
    {
        /// <summary>
        /// Texto fixo contendo 9999
        /// </summary>
        [MaxLength(4)]
        [MinLength(4)]
        [DefaultValue("9999")]
        public string REG { get { return "9999"; } set { } }

        /// <summary>
        /// A quantidade de linhas a ser informada deve considerar também o próprio registro 9999
        /// </summary>
        [MaxLength(9)]
        public int QTD_LIN { get; set; }
    }
}
