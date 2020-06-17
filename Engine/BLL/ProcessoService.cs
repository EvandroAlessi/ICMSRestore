using CrossCutting.Models;
using DAO;
using Dominio;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL
{
    public class ProcessoService
    {
        private static readonly ProcessoDAO processoDAO = new ProcessoDAO();

        public async Task<Pagination> GetPagination(int page = 1, int take = 30, Dictionary<string, string> filters = null)
        {
            try
            {
                int skip = (page - 1) * take;

                return await processoDAO.GetPagination(skip, take, filters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Processo>> GetAll(int page = 1, int take = 30, Dictionary<string, string> filters = null)
        {
            try
            {
                int skip = (page - 1) * take;

                return await processoDAO.GetAll(skip, take, filters);
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
