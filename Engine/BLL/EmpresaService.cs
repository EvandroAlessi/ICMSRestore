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
        private static readonly EmpresaDAO empresaDAO = new EmpresaDAO();

        public async Task<Pagination> GetPagination(int page, int take, Dictionary<string, string> filters)
        {
            try
            {
                return await empresaDAO.GetPagination("Empresas", page, take, filters);
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

                return await empresaDAO.GetAll(skip, take, filters);
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
                return await empresaDAO.Get(id);
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
                return await empresaDAO.Exists(id);
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
                return empresaDAO.Insert(empresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Edit(Empresa empresa)
        {
            try
            {
                return empresaDAO.Edit(empresa);
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
                return empresaDAO.Delete(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
