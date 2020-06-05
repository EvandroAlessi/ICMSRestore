using DAO;
using Dominio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ProcessoService
    {
        private static readonly ProcessoDAO processoDAO = new ProcessoDAO();

        public async Task<List<Processo>> GetAll()
        {
            try
            {
                return await processoDAO.GetAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Processo> Get(int id)
        {
            try
            {
                return await processoDAO.Get(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Exists(int id)
        {
            try
            {
                return await processoDAO.Exists(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Processo Insert(Processo processo)
        {
            try
            {
                return processoDAO.Insert(processo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Edit(Processo processo)
        {
            try
            {
                return processoDAO.Edit(processo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                return processoDAO.Delete(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
