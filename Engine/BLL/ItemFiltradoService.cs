using CrossCutting.Models;
using DAO;
using Dominio;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL
{
    public class ItemFiltradoService
    {
        private static readonly ItemFiltradoDAO itemFiltradoDAO = new ItemFiltradoDAO();

        public async Task<Pagination> GetPagination(int page, int take, Dictionary<string, string> filters)
        {
            try
            {
                return await itemFiltradoDAO.GetPagination("ItensFiltrados", page, take, filters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ItemFiltrado>> GetAll(int page = 1, int take = 30, Dictionary<string, string> filters = null)
        {
            try
            {
                int skip = (page - 1) * take;

                return await itemFiltradoDAO.GetAll(skip, take, filters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ItemFiltrado>> GetAll(int processID, int page = 1, int take = 30)
        {
            try
            {
                int skip = (page - 1) * take;

                return await itemFiltradoDAO.GetAll(processID, skip, take);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ItemFiltrado> Get(int id)
        {
            try
            {
                return await itemFiltradoDAO.Get(id);
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
                return await itemFiltradoDAO.Exists(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ItemFiltrado Insert(ItemFiltrado itemFiltrado)
        {
            try
            {
                return itemFiltradoDAO.Insert(itemFiltrado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ItemFiltrado Edit(ItemFiltrado itemFiltrado)
        {
            try
            {
                return itemFiltradoDAO.Edit(itemFiltrado);
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
                return itemFiltradoDAO.Delete(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
