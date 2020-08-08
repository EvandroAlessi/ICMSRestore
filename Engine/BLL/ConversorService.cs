using CrossCutting.Models;
using DAO;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ConversorService
    {
        private static readonly ConversorDAO dao = new ConversorDAO();

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

        public async Task<List<Conversor>> GetAll(int page = 1, int take = 30, Dictionary<string, string> filters = null)
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="processID"></param>
        /// <returns></returns>
        public async Task<List<Conversor>> GetUnitDifferences(int processID)
        {
            try
            {
                var requiredConversions = new List<Conversor>();
                var filteredItemService = new ItemFiltradoService();
                var processoService = new ProcessoService();

                var process = await processoService.Get(processID);

                var converters = await dao.GetAllByCompany(process.EmpresaID);

                var list = await filteredItemService.GetAll(processID, isLimited: false);

                foreach (var item in list.GroupBy(x => new { x.cProd, x.NCM }))
                {
                    var entries = item.Where(x => x.Entrada);
                    var outputs = item.Where(x => !x.Entrada);

                    var diffEntry = entries.FirstOrDefault(x => outputs.Any(y => y.uCom != x.uCom));

                    if (diffEntry != null)
                    {
                        var firstOut = outputs.First(y => y.uCom != diffEntry.uCom);

                        if (!converters.Any(x => x.cProd.Equals(diffEntry.cProd) && x.NCM.Equals(diffEntry.NCM)))
                        {
                            requiredConversions.Add(new Conversor
                            {
                                EmpresaID = process.EmpresaID,
                                NCM = diffEntry.NCM,
                                cProd = diffEntry.cProd,
                                Unidade = diffEntry.uCom,
                                UnidadeResultante = firstOut.uCom
                            });

                            continue;
                        }
                    }
                }

                return requiredConversions;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Conversor>> GetAllByCompany(int companyID)
        {
            try
            {
                return await dao.GetAllByCompany(companyID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Conversor> Get(int id)
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

        public Conversor Insert(Conversor conversor)
        {
            try
            {
                return dao.Insert(conversor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Conversor Edit(Conversor conversor)
        {
            try
            {
                return dao.Edit(conversor);
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
