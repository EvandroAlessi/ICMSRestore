using CrossCutting.Models;
using DAO;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public class NFeService
    {
        private static readonly NFeDAO nfeDAO = new NFeDAO();

        public async Task<Pagination> GetPagination(int page = 1, int take = 30, Dictionary<string, string> filters = null)
        {
            try
            {
                int skip = (page - 1) * take;

                return await nfeDAO.GetPagination(skip, take, filters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<NFe>> GetAll(int page = 1, int take = 30, Dictionary<string, string> filters = null)
        {
            try
            {
                int skip = (page - 1) * take;

                return await nfeDAO.GetAll(skip, take, filters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<NFe> Get(int id)
        {
            try
            {
                return await nfeDAO.Get(id);
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
                return await nfeDAO.Exists(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Exists(int cNF, int nNF, int processoID)
        {
            try
            {
                return await nfeDAO.Exists(cNF, nNF, processoID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public NFe Insert(NFe nfe)
        {
            try
            {
                return nfeDAO.Insert(nfe);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Insert(CrossCutting.SerializationModels.NFe nFeProc, int processoID, bool entrada = false)
        {
            try
            {
                NFe nFe = new NFe
                {
                    CEP = nFeProc.InformacoesNFe.Emitente.Endereco?.CEP,
                    CEP_DEST = nFeProc.InformacoesNFe.Destinatario?.Endereco?.CEP,
                    CNPJ = nFeProc.InformacoesNFe.Emitente.CNPJ,
                    CNPJ_DEST = nFeProc.InformacoesNFe.Destinatario?.CNPJ,
                    CPF_DEST = nFeProc.InformacoesNFe.Destinatario?.CPF,
                    CRT = nFeProc.InformacoesNFe.Emitente.CRT,
                    IE = nFeProc.InformacoesNFe.Emitente.IE,
                    IEST = nFeProc.InformacoesNFe.Emitente.IEST,
                    UF = nFeProc.InformacoesNFe.Emitente.Endereco?.UF,
                    UF_DEST = nFeProc.InformacoesNFe.Destinatario?.Endereco?.UF,
                    cMun = nFeProc.InformacoesNFe.Emitente.Endereco?.cMun,
                    cMun_DEST = nFeProc.InformacoesNFe.Destinatario?.Endereco?.cMun,
                    cNF = nFeProc.InformacoesNFe.Identificacao.cNF,
                    cPais = nFeProc.InformacoesNFe.Emitente.Endereco.cPais,
                    cPais_DEST = nFeProc.InformacoesNFe.Destinatario?.Endereco?.cPais,
                    cUF = nFeProc.InformacoesNFe.Identificacao.cUF,
                    dhEmi = nFeProc.InformacoesNFe.Identificacao.dhEmi,
                    dhSaiEnt = nFeProc.InformacoesNFe.Identificacao.dhSaiEnt,
                    email_DEST = nFeProc.InformacoesNFe.Destinatario?.email,
                    indPag = nFeProc.InformacoesNFe.Identificacao.indPag,
                    mod = nFeProc.InformacoesNFe.Identificacao?.mod,
                    nNF = nFeProc.InformacoesNFe.Identificacao.nNF,
                    natOp = nFeProc.InformacoesNFe.Identificacao?.natOp,
                    nro = nFeProc.InformacoesNFe.Emitente.Endereco?.nro,
                    nro_DEST = nFeProc.InformacoesNFe.Destinatario?.Endereco?.nro,
                    serie = nFeProc.InformacoesNFe.Identificacao.serie,
                    xBairro = nFeProc.InformacoesNFe.Emitente.Endereco?.xBairro,
                    xBairro_DEST = nFeProc.InformacoesNFe.Destinatario?.Endereco?.xBairro,
                    xFant = nFeProc.InformacoesNFe.Emitente.xFant,
                    xLgr = nFeProc.InformacoesNFe.Emitente.Endereco?.xLgr,
                    xLgr_DEST = nFeProc.InformacoesNFe.Destinatario?.Endereco?.xLgr,
                    xMun = nFeProc.InformacoesNFe.Emitente.Endereco?.xMun,
                    xMun_DEST = nFeProc.InformacoesNFe.Destinatario?.Endereco?.xMun,
                    xNome = nFeProc.InformacoesNFe.Emitente.xNome,
                    xNome_DEST = nFeProc.InformacoesNFe.Destinatario?.xNome,
                    xPais = nFeProc.InformacoesNFe.Emitente.Endereco?.xPais,
                    xPais_DEST = nFeProc.InformacoesNFe.Destinatario?.Endereco?.xPais,
                    vBC_TOTAL = nFeProc.InformacoesNFe.Total?.ICMSTot?.vBC,
                    vICMS_TOTAL = nFeProc.InformacoesNFe.Total?.ICMSTot?.vICMS,
                    vICMSDeson_TOTAL = nFeProc.InformacoesNFe.Total?.ICMSTot?.vICMSDeson,
                    vFCP_TOTAL = nFeProc.InformacoesNFe.Total?.ICMSTot?.vFCP,
                    vBCST_TOTAL = nFeProc.InformacoesNFe.Total?.ICMSTot?.vBCST,
                    vST_TOTAL = nFeProc.InformacoesNFe.Total?.ICMSTot?.vST,
                    vFCPST_TOTAL = nFeProc.InformacoesNFe.Total?.ICMSTot?.vFCPST,
                    vFCPSTRet_TOTAL = nFeProc.InformacoesNFe?.Total?.ICMSTot?.vFCPSTRet,
                    vProd_TOTAL = nFeProc.InformacoesNFe.Total?.ICMSTot?.vProd,
                    vFrete_TOTAL = nFeProc.InformacoesNFe.Total?.ICMSTot?.vFrete,
                    vSeg_TOTAL = nFeProc.InformacoesNFe.Total?.ICMSTot?.vSeg,
                    vDesc_TOTAL = nFeProc.InformacoesNFe.Total?.ICMSTot?.vDesc,
                    vII_TOTAL = nFeProc.InformacoesNFe.Total?.ICMSTot?.vII,
                    vIPI_TOTAL = nFeProc.InformacoesNFe.Total?.ICMSTot?.vIPI,
                    vIPIDevol_TOTAL = nFeProc.InformacoesNFe.Total?.ICMSTot?.vIPIDevol,
                    vPIS_TOTAL = nFeProc.InformacoesNFe.Total?.ICMSTot?.vPIS,
                    vCOFINS_TOTAL = nFeProc.InformacoesNFe.Total?.ICMSTot?.vCOFINS,
                    vOutro_TOTAL = nFeProc.InformacoesNFe.Total?.ICMSTot?.vOutro,
                    vNF_TOTAL = nFeProc.InformacoesNFe.Total?.ICMSTot?.vNF,
                    ProcessoID = processoID,
                    Entrada = entrada
                };

                List<Item> itens = new List<Item>();

                foreach (var detalhe in nFeProc.InformacoesNFe.Detalhe)
                {
                    itens.Add(MakeItem(detalhe, nFe.ID));
                }

                return nfeDAO.InsertNFeItensTransaction(nFe, itens);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Item MakeItem(CrossCutting.SerializationModels.Detalhe detalhe, int nfeID)
        {
            var aux = detalhe?.Imposto?.ICMS?
                .GetType()
                .GetProperties()
                .FirstOrDefault(x => !(x.GetValue(detalhe?.Imposto?.ICMS) is null))
                ?.GetValue(detalhe?.Imposto?.ICMS);

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
                cEnq = detalhe.Imposto.IPI?.cEnq,
                CST_IPI = detalhe.Imposto.IPI?.IPINT?.CST,
                CST_PIS = detalhe.Imposto.PIS?.PISAliq?.CST,
                vBC_PIS = detalhe.Imposto.PIS?.PISAliq?.vBC,
                pPIS = detalhe.Imposto.PIS?.PISAliq?.pPIS,
                vPIS = detalhe.Imposto.PIS?.PISAliq?.vPIS,
                CST_COFINS = detalhe.Imposto.COFINS?.COFINSAliq?.CST,
                vBC_COFINS = detalhe.Imposto.COFINS?.COFINSAliq?.vBC,
                pCOFINS = detalhe.Imposto.COFINS?.COFINSAliq?.pCOFINS,
                vCOFINS = detalhe.Imposto.COFINS?.COFINSAliq?.vCOFINS,
                NFeID = nfeID
            };

            switch (aux)
            {
                case CrossCutting.SerializationModels.ICMS00 icms:
                    item.orig = detalhe.Imposto.ICMS?.ICMS00?.orig;
                    item.CST = detalhe.Imposto.ICMS?.ICMS00?.CST;
                    item.modBC = detalhe.Imposto.ICMS?.ICMS00?.modBC;
                    item.vBC = detalhe.Imposto.ICMS?.ICMS00?.vBC;
                    item.pICMS = detalhe.Imposto.ICMS?.ICMS00?.pICMS;
                    item.vICMS = detalhe.Imposto.ICMS?.ICMS00?.vICMS;

                    break;
                case CrossCutting.SerializationModels.ICMSSN101 icms:

                    item.orig = detalhe.Imposto.ICMS?.ICMSSN101?.orig;
                    item.CSOSN = detalhe.Imposto.ICMS?.ICMSSN101?.CSOSN;
                    break;
                case CrossCutting.SerializationModels.ICMSSN102 icms:

                    item.orig = detalhe.Imposto.ICMS?.ICMSSN102?.orig;
                    item.CSOSN = detalhe.Imposto.ICMS?.ICMSSN102?.CSOSN;
                    break;
                case CrossCutting.SerializationModels.ICMSSN103 icms:

                    item.orig = detalhe.Imposto.ICMS?.ICMSSN103?.orig;
                    item.CSOSN = detalhe.Imposto.ICMS?.ICMSSN103?.CSOSN;
                    break;
                case CrossCutting.SerializationModels.ICMSSN201 icms:

                    item.orig = detalhe.Imposto.ICMS?.ICMSSN201?.orig;
                    item.CSOSN = detalhe.Imposto.ICMS?.ICMSSN201?.CSOSN;
                    break;
                case CrossCutting.SerializationModels.ICMSSN202 icms:

                    item.orig = detalhe.Imposto.ICMS?.ICMSSN202?.orig;
                    item.CSOSN = detalhe.Imposto.ICMS?.ICMSSN202?.CSOSN;
                    break;
                case CrossCutting.SerializationModels.ICMSSN203 icms:

                    item.orig = detalhe.Imposto.ICMS?.ICMSSN203?.orig;
                    item.CSOSN = detalhe.Imposto.ICMS?.ICMSSN203?.CSOSN;
                    break;
                case CrossCutting.SerializationModels.ICMSSN300 icms:

                    item.orig = detalhe.Imposto.ICMS?.ICMSSN300?.orig;
                    item.CSOSN = detalhe.Imposto.ICMS?.ICMSSN300?.CSOSN;
                    break;
                case CrossCutting.SerializationModels.ICMSSN400 icms:

                    item.orig = detalhe.Imposto.ICMS?.ICMSSN400?.orig;
                    item.CSOSN = detalhe.Imposto.ICMS?.ICMSSN400?.CSOSN;
                    break;
                case CrossCutting.SerializationModels.ICMSSN500 icms:

                    item.orig = detalhe.Imposto.ICMS?.ICMSSN500?.orig;
                    item.CSOSN = detalhe.Imposto.ICMS?.ICMSSN500?.CSOSN;
                    break;
                case CrossCutting.SerializationModels.ICMSSN900 icms:

                    item.orig = detalhe.Imposto.ICMS?.ICMSSN900?.orig;
                    item.CSOSN = detalhe.Imposto.ICMS?.ICMSSN900?.CSOSN;
                    break;
                default:
                    break;
            }

            return item;
        }


        public bool Edit(NFe nfe)
        {
            try
            {
                return nfeDAO.Edit(nfe);
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
                return nfeDAO.Delete(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
