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
        private static readonly ItemFiltradoDAO dao = new ItemFiltradoDAO();

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

        public async Task<List<ItemFiltrado>> GetAll(int page = 1, int take = 30, Dictionary<string, string> filters = null)
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

        public async Task<List<ItemFiltrado>> GetAll(int processID, int page = 1, int take = 30)
        {
            try
            {
                int skip = (page - 1) * take;

                return await dao.GetAll(processID, skip, take);
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
                return await dao.Get(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ProductMedia>> GetSumarry(int processID)
        {
            try
            {
                var processService = new ProcessoService();

                var process = await processService.Get(processID);

                return await dao.GetSumarry(process);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Calc(string path, int processID, int? ncm = null)
        {
            try
            {
                return await dao.CalcItems(path, processID, ncm);
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

        public ItemFiltrado Insert(ItemFiltrado itemFiltrado)
        {
            try
            {
                return dao.Insert(itemFiltrado);
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
                return dao.Edit(itemFiltrado);
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
