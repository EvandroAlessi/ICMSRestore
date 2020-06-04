using System;
using System.Collections.Generic;
using System.Text;
using DAO;
using Dominio;

namespace BLL
{
    public class ItemService
    {
        private readonly ItemDAO itemDAO = new ItemDAO();

        public bool Insert(CrossCutting.SerializationModels.Detalhe detalhe, int cNFe)
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
                    cNF = cNFe
                };

                return itemDAO.Insert(item);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
