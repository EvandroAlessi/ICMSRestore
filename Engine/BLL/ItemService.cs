using CrossCutting.Models;
using DAO;
using Dominio;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL
{
    public class ItemService
    {
        private readonly ItemDAO itemDAO = new ItemDAO();

        public async Task<Pagination> GetPagination(int page = 1, int take = 30, Dictionary<string, string> filters = null)
        {
            try
            {
                int skip = (page - 1) * take;

                return await itemDAO.GetPagination(skip, take, filters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Item>> GetAll(int page = 1, int take = 30, Dictionary<string, string> filters = null)
        {
            try
            {
                int skip = (page - 1) * take;

                return await itemDAO.GetAll(skip, take, filters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Item>> GetAll(int nfeID)
        {
            try
            {
                return await itemDAO.GetAll(nfeID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Item> Get(int id)
        {
            try
            {
                return await itemDAO.Get(id);
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
                return await itemDAO.Exists(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Item Insert(Item item)
        {
            try
            {
                return itemDAO.Insert(item);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Insert(CrossCutting.SerializationModels.Detalhe detalhe, int nfeID)
        {
            try
            {
                var item = new Item
                {
                    nItem = detalhe.nItem,
                    cProd = detalhe.Produto.cProd,
                    cEAN = detalhe.Produto.cEAN,
                    xProd = detalhe.Produto.xProd,
                    NCM = detalhe.Produto.NCM,
                    CFOP = detalhe.Produto.CFOP,
                    uCom = detalhe.Produto.uCom,
                    qCom = detalhe.Produto.qCom,
                    vUnCom = detalhe.Produto.vUnCom,
                    orig = detalhe.Imposto.ICMS.ICMS00?.orig,
                    CST = detalhe.Imposto.ICMS.ICMS00?.CST,
                    modBC = detalhe.Imposto.ICMS.ICMS00?.modBC,
                    vBC = detalhe.Imposto.ICMS.ICMS00?.vBC,
                    pICMS = detalhe.Imposto.ICMS.ICMS00?.pICMS,
                    vICMS = detalhe.Imposto.ICMS.ICMS00?.vICMS,
                    cEnq = detalhe.Imposto.IPI?.cEnq,
                    CST_IPI = detalhe.Imposto.IPI?.IPINT?.CST,
                    CST_PIS = detalhe.Imposto.PIS.PISAliq?.CST,
                    vBC_PIS = detalhe.Imposto.PIS.PISAliq?.vBC,
                    pPIS = detalhe.Imposto.PIS.PISAliq?.pPIS,
                    vPIS = detalhe.Imposto.PIS.PISAliq?.vPIS,
                    CST_COFINS = detalhe.Imposto.COFINS.COFINSAliq?.CST,
                    vBC_COFINS = detalhe.Imposto.COFINS.COFINSAliq?.vBC,
                    pCOFINS = detalhe.Imposto.COFINS.COFINSAliq?.pCOFINS,
                    vCOFINS = detalhe.Imposto.COFINS.COFINSAliq?.vCOFINS,
                    NFeID = nfeID
                };

                return itemDAO.InserWithoutObjReturn(item);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Edit(Item item)
        {
            try
            {
                return itemDAO.Edit(item);
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
                return itemDAO.Delete(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
