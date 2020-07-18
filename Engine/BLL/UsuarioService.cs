using CrossCutting.Models;
using DAO;
using Dominio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UsuarioService
    {
        private static readonly UsuarioDAO dao = new UsuarioDAO();

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

        public async Task<List<Usuario>> GetAll(int page = 1, int take = 30, Dictionary<string, string> filters = null)
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

        public async Task<Usuario> Get(int id)
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

        public async Task<Usuario> Get(string nome, string senha)
        {
            try
            {
                return await dao.Get(nome, senha);
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

        public async Task<bool> Exists(int id, string senha)
        {
            try
            {
                return await dao.Exists(id, senha);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Usuario Insert(Usuario user)
        {
            try
            {
                return dao.Insert(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Usuario Edit(Usuario user)
         {
            try
            {
                return dao.Edit(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Edit(int id, string role)
        {
            try
            {
                return dao.Edit(id, role);
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
