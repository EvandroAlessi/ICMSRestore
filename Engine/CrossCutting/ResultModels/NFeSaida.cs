using CrossCutting.ResultModels.Commom;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CrossCutting.ResultModels
{
    /// <summary>
    /// Este registro deve conter todas as notas fiscais de saída emitidas em operação interna de venda a consumidor final do produto declarado no registro
    /// 1000. Deverá conter a totalidade das operações de saídas realizadas no período de apuração, para cada produto comercializado sujeito a substituição
    /// tributária, ainda que não exista valor a recuperar, a ressarcir ou a complementar.Este registro visa a atender ao disposto no art. 6º-A do Anexo IX do RICMS/17
    /// </summary>
    public partial class NFeSaida : NFeSaidaBase
    {
        /// <summary>
        /// Texto fixo contendo 1210
        /// </summary>
        [MaxLength(4)]
        [MinLength(4)]
        [DefaultValue("1210")]
        public string REG { get; set; }
    }
}
