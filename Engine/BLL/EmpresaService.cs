using CrossCutting.Models;
using DAO;
using Dominio;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL
{
    public class EmpresaService
    {
        private static readonly EmpresaDAO dao = new EmpresaDAO();

        public async Task<long> GetCount()
        {
            try
            {
                return await dao.GetCount();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Pagination> GetPagination(int page, int take, Dictionary<string, string> filters)
        {
            try
            {
                return await dao.GetPagination(page, take, filters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Empresa>> GetAll(int page = 1, int take = 30, Dictionary<string, string> filters = null)
        {
            try
            {
                int skip = (page - 1) * take;

                return await dao.GetAll(skip, take, filters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Empresa> Get(int id)
        {
            try
            {
                return await dao.Get(id);
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
                return await dao.Exists(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Empresa Insert(Empresa empresa)
        {
            try
            {
                return dao.Insert(empresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Empresa Edit(Empresa empresa)
        {
            try
            {
                return dao.Edit(empresa);
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
                return dao.Delete(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
