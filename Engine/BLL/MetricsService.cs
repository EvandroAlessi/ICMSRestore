using DAO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class MetricsService
    {
        public async Task<dynamic> GetCounts()
        {
            try
            {
                var processDAO = new ProcessoDAO();

                dynamic counts = new
                {
                    Companies = await processDAO.GetCount(),
                    Processes = await new ProcessoDAO().GetCount(),
                    FinishedProcesses = await processDAO.GetFinishedCount(),
                    Invoices = await new NFeDAO().GetCount(),
                };

                return counts;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
