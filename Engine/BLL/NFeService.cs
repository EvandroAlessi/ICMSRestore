using Dominio;
using DAO;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public class NFeService
    {
        private readonly NFeDAO nfeDAO = new NFeDAO(); 

        public async Task<List<NFe>> GetAll()
        {
            return await nfeDAO.GetAll();
        }

        public async Task<bool> Exists(int cNF)
        {
            try
            {
                return await nfeDAO.Exists(cNF);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool Insert(CrossCutting.SerializationModels.NFeProc nFeProc, int processoID)
        {
            try
            {
                NFe nFe = new NFe
                {
                    CEP = nFeProc.NotaFiscalEletronica.InformacoesNFe.Emitente?.Endereco?.CEP,
                    CEP_DEST = nFeProc.NotaFiscalEletronica.InformacoesNFe.Destinatario?.Endereco?.CEP,
                    CNPJ = nFeProc.NotaFiscalEletronica.InformacoesNFe.Emitente?.CNPJ,
                    CNPJ_DEST = nFeProc.NotaFiscalEletronica.InformacoesNFe.Destinatario?.CNPJ,
                    CPF_DEST = nFeProc.NotaFiscalEletronica.InformacoesNFe.Destinatario?.CPF,
                    CRT = nFeProc.NotaFiscalEletronica.InformacoesNFe.Emitente.CRT,
                    IE = nFeProc.NotaFiscalEletronica.InformacoesNFe.Emitente?.IE,
                    IEST = nFeProc.NotaFiscalEletronica.InformacoesNFe.Emitente?.IEST,
                    UF = nFeProc.NotaFiscalEletronica.InformacoesNFe.Emitente?.Endereco?.UF,
                    UF_DEST = nFeProc.NotaFiscalEletronica.InformacoesNFe.Destinatario?.Endereco?.UF,
                    cMun = nFeProc.NotaFiscalEletronica.InformacoesNFe.Emitente?.Endereco?.cMun,
                    cMun_DEST = nFeProc.NotaFiscalEletronica.InformacoesNFe.Destinatario?.Endereco?.cMun,
                    cNF = nFeProc.NotaFiscalEletronica.InformacoesNFe.Identificacao.cNF,
                    cPais = nFeProc.NotaFiscalEletronica.InformacoesNFe.Emitente.Endereco.cPais,
                    cPais_DEST = nFeProc.NotaFiscalEletronica.InformacoesNFe.Destinatario.Endereco.cPais,
                    cUF = nFeProc.NotaFiscalEletronica.InformacoesNFe.Identificacao.cUF,
                    dhEmi = nFeProc.NotaFiscalEletronica.InformacoesNFe.Identificacao.dhEmi,
                    dhSaiEnt = nFeProc.NotaFiscalEletronica.InformacoesNFe.Identificacao.dhSaiEnt,
                    email_DEST = nFeProc.NotaFiscalEletronica.InformacoesNFe.Destinatario.email,
                    indPag = nFeProc.NotaFiscalEletronica.InformacoesNFe.Identificacao.indPag,
                    mod = nFeProc.NotaFiscalEletronica.InformacoesNFe.Identificacao?.mod,
                    nNF = nFeProc.NotaFiscalEletronica.InformacoesNFe.Identificacao?.nNF,
                    natOp = nFeProc.NotaFiscalEletronica.InformacoesNFe.Identificacao?.natOp,
                    nro = nFeProc.NotaFiscalEletronica.InformacoesNFe.Emitente?.Endereco?.nro,
                    nro_DEST = nFeProc.NotaFiscalEletronica.InformacoesNFe.Destinatario?.Endereco?.nro,
                    serie = nFeProc.NotaFiscalEletronica.InformacoesNFe.Identificacao.serie,
                    xBairro = nFeProc.NotaFiscalEletronica.InformacoesNFe.Emitente?.Endereco?.xBairro,
                    xBairro_DEST = nFeProc.NotaFiscalEletronica.InformacoesNFe.Destinatario?.Endereco?.xBairro,
                    xFant = nFeProc.NotaFiscalEletronica.InformacoesNFe.Emitente?.xFant,
                    xLgr = nFeProc.NotaFiscalEletronica.InformacoesNFe.Emitente?.Endereco?.xLgr,
                    xLgr_DEST = nFeProc.NotaFiscalEletronica.InformacoesNFe.Destinatario?.Endereco?.xLgr,
                    xMun = nFeProc.NotaFiscalEletronica.InformacoesNFe.Emitente?.Endereco?.xMun,
                    xMun_DEST = nFeProc.NotaFiscalEletronica.InformacoesNFe.Destinatario?.Endereco?.xMun,
                    xNome = nFeProc.NotaFiscalEletronica.InformacoesNFe.Emitente?.xNome,
                    xNome_DEST = nFeProc.NotaFiscalEletronica.InformacoesNFe.Destinatario?.xNome,
                    xPais = nFeProc.NotaFiscalEletronica.InformacoesNFe.Emitente?.Endereco?.xPais,
                    xPais_DEST = nFeProc.NotaFiscalEletronica.InformacoesNFe.Destinatario?.Endereco?.xPais
                };

                return nfeDAO.Insert(nFe, processoID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
