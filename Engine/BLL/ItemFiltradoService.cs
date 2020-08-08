using CrossCutting;
using CrossCutting.Models;
using DAO;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<ItemFiltrado>> GetAll(int processID, int page = 1, int take = 30, bool isLimited = true)
        {
            try
            {
                int skip = (page - 1) * take;

                return await dao.GetAll(processID, skip, take, isLimited);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="processID"></param>
        /// <returns></returns>
        public async Task<List<ProductMedia>> GetSummary(int processID)
        {
            try
            {
                var converterService = new ConversorService();

                var converters = await converterService.GetAllByCompany(processID);

                var list = await dao.GetAll(processID, isLimited: false);

                if (converters != null && converters.Count > 0)
                {
                    foreach (var item in list)
                    {
                        foreach (var converter in converters)
                        {
                            if (item.cProd == converter.cProd && item.NCM == converter.NCM)
                            {
                                list.First(x => x.ID == item.ID).vUnCom = item.vUnCom * converter.FatorConversao;

                                break;
                            }
                        }
                    }
                }

                var result = SummaryBuilder.GetFilteredItems(list);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task BuildResult(string path, int processID, int? ncm = null)
        {
            try
            {
                var items = await dao.BuildResult(processID, ncm);

                var documents = DocumentBuilder.BuildDocument(items);

                DocumentBuilder.WriteDocument(path, documents);
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
