using CrossCutting.Models;
using DAO;
using Dominio;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL
{
    public class MVAService
    {
        private static readonly MVADAO mvaDAO = new MVADAO();

        public async Task<Pagination> GetPagination(int page, int take, Dictionary<string, string> filters)
        {
            try
            {
                return await mvaDAO.GetPagination("MVA", page, take, filters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<MVA>> GetAll(int page = 1, int take = 30, Dictionary<string, string> filters = null)
        {
            try
            {
                int skip = (page - 1) * take;

                return await mvaDAO.GetAll(skip, take, filters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<MVA> Get(int id)
        {
            try
            {
                return await mvaDAO.Get(id);
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
                return await mvaDAO.Exists(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MVA Insert(MVA mva)
        {
            try
            {
                return mvaDAO.Insert(mva);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Edit(MVA mva)
        {
            try
            {
                return mvaDAO.Edit(mva);
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
                return mvaDAO.Delete(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
