using DAO;
using Dominio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class EmpresaService
    {
        private static readonly EmpresaDAO empresaDAO = new EmpresaDAO();

        public async Task<List<Empresa>> GetAll()
        {
            try
            {
                return await empresaDAO.GetAll();
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
