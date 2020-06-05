using CrossCutting;
using Dominio;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAO
{
    public class NFeDAO
    {
        static string connString = AppSettings.ConnectionString;
        const string quote = "\"";

        public async Task<List<NFe>> GetAll()
        {
            try
            {
                var list = new List<NFe>();

                using (var conn = new NpgsqlConnection(connString))
                {
                    await conn.OpenAsync();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $@"SELECT * FROM {quote}NFe{ quote};";

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var nfe = new NFe
                                {
                                    CEP = reader["CEP"]?.ToString(),
                                    CEP_DEST = reader["CEP_DEST"]?.ToString(),
                                    CNPJ = reader["CNPJ"]?.ToString(),
                                    CNPJ_DEST = reader["CNPJ_DEST"]?.ToString(),
                                    CPF_DEST = reader["CPF_DEST"]?.ToString(),
                                    CRT = Convert.ToInt32(reader["CRT"]),
                                    IE = reader["IE"]?.ToString(),
                                    IEST = reader["IEST"]?.ToString(),
                                    UF = reader["UF"]?.ToString(),
                                    UF_DEST = reader["UF_DEST"]?.ToString(),
                                    cMun = reader["cMun"]?.ToString(),
                                    cMun_DEST = reader["cMun_DEST"]?.ToString(),
                                    cNF = Convert.ToInt32(reader["cNF"]),
                                    cPais = Convert.ToInt32(reader["cPais"]),
                                    cPais_DEST = Convert.ToInt32(reader["cPais_DEST"]),
                                    cUF = Convert.ToInt32(reader["cUF"]),
                                    dhEmi = Convert.ToDateTime(reader["dhEmi"]),
                                    dhSaiEnt = Convert.ToDateTime(reader["dhSaiEnt"]),
                                    email_DEST = reader["email_DEST"]?.ToString(),
                                    indPag = reader["indPag"] is null ? 0 : Convert.ToInt32(reader["indPag"]),
                                    mod = reader["mod"]?.ToString(),
                                    nNF = reader["nNF"]?.ToString(),
                                    natOp = reader["natOp"]?.ToString(),
                                    nro = reader["nro"]?.ToString(),
                                    nro_DEST = reader["nro_DEST"]?.ToString(),
                                    serie = reader["serie"] is null ? 0 : Convert.ToInt32(reader["serie"]),
                                    xBairro = reader["xBairro"]?.ToString(),
                                    xBairro_DEST = reader["xBairro_DEST"]?.ToString(),
                                    xFant = reader["xFant"]?.ToString(),
                                    xLgr = reader["xLgr"]?.ToString(),
                                    xLgr_DEST = reader["xLgr_DEST"]?.ToString(),
                                    xMun = reader["xMun"]?.ToString(),
                                    xMun_DEST = reader["xMun_DEST"]?.ToString(),
                                    xNome = reader["xNome"]?.ToString(),
                                    xNome_DEST = reader["xNome_DEST"]?.ToString(),
                                    xPais = reader["xPais"]?.ToString(),
                                    xPais_DEST = reader["xPais_DEST"]?.ToString()
                                };

                                list.Add(nfe);
                            }
                        }
                    }

                    await conn.CloseAsync();
                }

                return list;
            }
            catch (NpgsqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Exists(int cNF)
        {
            try
            {
                bool exists = false;

                using (var conn = new NpgsqlConnection(connString))
                {
                    await conn.OpenAsync();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $@"SELECT {quote}cNF{quote}  FROM {quote}NFe{quote} 
                                WHERE {quote}cNF{quote} = { cNF };";

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                exists = true;
                                break;
                            }
                        }
                    }

                    await conn.CloseAsync();
                }

                return exists;
            }
            catch (NpgsqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Insert(NFe nfe, int processoID)
        {
            try
            {
                int rows = 0;

                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $@"INSERT INTO {quote}NFe{ quote} (
                                {quote}ProcessoID{quote}
                                , {quote}CEP{quote}
                                , {quote}CEP_DEST{quote}
                                , {quote}CNPJ{quote}
                                , {quote}CNPJ_DEST{quote}
                                , {quote}CPF_DEST{quote}
                                , {quote}CRT{quote}
                                , {quote}IE{quote}
                                , {quote}IEST{quote}
                                , {quote}UF{quote}
                                , {quote}UF_DEST{quote}
                                , {quote}cMun{quote}
                                , {quote}cMun_DEST{quote}
                                , {quote}cNF{quote}
                                , {quote}cPais{quote}
                                , {quote}cPais_DEST{quote}
                                , {quote}cUF{quote}
                                , {quote}dhEmi{quote}
                                , {quote}dhSaiEnt{quote}
                                , {quote}email_DEST{quote}
                                , {quote}indPag{quote}
                                , {quote}mod{quote}
                                , {quote}nNF{quote}
                                , {quote}natOp{quote}
                                , {quote}nro{quote}
                                , {quote}nro_DEST{quote}
                                , {quote}serie{quote}
                                , {quote}xBairro{quote}
                                , {quote}xBairro_DEST{quote}
                                , {quote}xFant{quote}
                                , {quote}xLgr{quote}
                                , {quote}xLgr_DEST{quote}
                                , {quote}xMun{quote}
                                , {quote}xMun_DEST{quote}
                                , {quote}xNome{quote}
                                , {quote}xNome_DEST{quote}
                                , {quote}xPais{quote}
                                , {quote}xPais_DEST{quote}
                            ) VALUES (
                                { processoID }
                                , '{ nfe.CEP }'
                                , '{ nfe.CEP_DEST }'
                                , '{ nfe.CNPJ }'
                                , '{ nfe.CNPJ_DEST }'
                                , '{ nfe.CPF_DEST }'
                                , '{ nfe.CRT }'
                                , '{ nfe.IE }'
                                , '{ nfe.IEST }'
                                , '{ nfe.UF }'
                                , '{ nfe.UF_DEST }'
                                , '{ nfe.cMun }'
                                , '{ nfe.cMun_DEST }'
                                , '{ nfe.cNF }'
                                , '{ nfe.cPais }'
                                , '{ nfe.cPais_DEST }'
                                , '{ nfe.cUF }'
                                , '{ nfe.dhEmi }'
                                , '{ nfe.dhSaiEnt }'
                                , '{ nfe.email_DEST }'
                                , '{ nfe.indPag }'
                                , '{ nfe.mod }'
                                , '{ nfe.nNF }'
                                , '{ nfe.natOp }'
                                , '{ nfe.nro }'
                                , '{ nfe.nro_DEST }'
                                , '{ nfe.serie }'
                                , '{ nfe.xBairro }'
                                , '{ nfe.xBairro_DEST }'
                                , '{ nfe.xFant }'
                                , '{ nfe.xLgr }'
                                , '{ nfe.xLgr_DEST }'
                                , '{ nfe.xMun }'
                                , '{ nfe.xMun_DEST }'
                                , '{ nfe.xNome }'
                                , '{ nfe.xNome_DEST }'
                                , '{ nfe.xPais }'
                                , '{ nfe.xPais_DEST }');";

                        rows = cmd.ExecuteNonQuery();
                    }

                    conn.Close();
                }

                return rows > 0;
            }
            catch (NpgsqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Insert(CrossCutting.SerializationModels.NFeProc nfe)
        {
            try
            {
                int rows = 0;

                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $@"INSERT INTO {quote}NFe{ quote} (
                                {quote}CEP{quote}
                                , {quote}CEP_DEST{quote}
                                , {quote}CNPJ{quote}
                                , {quote}CNPJ_DEST{quote}
                                , {quote}CPF_DEST{quote}
                                , {quote}CRT{quote}
                                , {quote}IE{quote}
                                , {quote}IEST{quote}
                                , {quote}UF{quote}
                                , {quote}UF_DEST{quote}
                                , {quote}cMun{quote}
                                , {quote}cMun_DEST{quote}
                                , {quote}cNF{quote}
                                , {quote}cPais{quote}
                                , {quote}cPais_DEST{quote}
                                , {quote}cUF{quote}
                                , {quote}dhEmi{quote}
                                , {quote}dhSaiEnt{quote}
                                , {quote}email_DEST{quote}
                                , {quote}indPag{quote}
                                , {quote}mod{quote}
                                , {quote}nNF{quote}
                                , {quote}natOp{quote}
                                , {quote}nro{quote}
                                , {quote}nro_DEST{quote}
                                , {quote}serie{quote}
                                , {quote}xBairro{quote}
                                , {quote}xBairro_DEST{quote}
                                , {quote}xFant{quote}
                                , {quote}xLgr{quote}
                                , {quote}xLgr_DEST{quote}
                                , {quote}xMun{quote}
                                , {quote}xMun_DEST{quote}
                                , {quote}xNome{quote}
                                , {quote}xNome_DEST{quote}
                                , {quote}xPais{quote}
                                , {quote}xPais_DEST{quote}
                            ) VALUES (
                                '{ nfe.NotaFiscalEletronica.InformacoesNFe.Emitente?.Endereco?.CEP }'
                                , '{ nfe.NotaFiscalEletronica.InformacoesNFe.Destinatario?.Endereco?.CEP }'
                                , '{ nfe.NotaFiscalEletronica.InformacoesNFe.Emitente?.CNPJ }'
                                , '{ nfe.NotaFiscalEletronica.InformacoesNFe.Destinatario?.CNPJ }'
                                , '{ nfe.NotaFiscalEletronica.InformacoesNFe.Destinatario?.CPF }'
                                , '{ nfe.NotaFiscalEletronica.InformacoesNFe.Emitente?.CRT }'
                                , '{ nfe.NotaFiscalEletronica.InformacoesNFe.Emitente?.IE }'
                                , '{ nfe.NotaFiscalEletronica.InformacoesNFe.Emitente?.IEST }'
                                , '{ nfe.NotaFiscalEletronica.InformacoesNFe.Emitente?.Endereco?.UF }'
                                , '{ nfe.NotaFiscalEletronica.InformacoesNFe.Destinatario?.Endereco?.UF }'
                                , '{ nfe.NotaFiscalEletronica.InformacoesNFe.Emitente?.Endereco?.cMun }'
                                , '{ nfe.NotaFiscalEletronica.InformacoesNFe.Destinatario?.Endereco?.cMun }'
                                , '{ nfe.NotaFiscalEletronica.InformacoesNFe.Identificacao?.cNF }'
                                , '{ nfe.NotaFiscalEletronica.InformacoesNFe.Emitente?.Endereco?.cPais }'
                                , '{ nfe.NotaFiscalEletronica.InformacoesNFe.Destinatario?.Endereco?.cPais }'
                                , '{ nfe.NotaFiscalEletronica.InformacoesNFe.Identificacao?.cUF }'
                                , '{ nfe.NotaFiscalEletronica.InformacoesNFe.Identificacao?.dhEmi }'
                                , '{ nfe.NotaFiscalEletronica.InformacoesNFe.Identificacao?.dhSaiEnt }'
                                , '{ nfe.NotaFiscalEletronica.InformacoesNFe.Destinatario?.email }'
                                , '{ nfe.NotaFiscalEletronica.InformacoesNFe.Identificacao?.indPag }'
                                , '{ nfe.NotaFiscalEletronica.InformacoesNFe.Identificacao?.mod }'
                                , '{ nfe.NotaFiscalEletronica.InformacoesNFe.Identificacao?.nNF }'
                                , '{ nfe.NotaFiscalEletronica.InformacoesNFe.Identificacao?.natOp }'
                                , '{ nfe.NotaFiscalEletronica.InformacoesNFe.Emitente?.Endereco?.nro }'
                                , '{ nfe.NotaFiscalEletronica.InformacoesNFe.Destinatario?.Endereco?.nro }'
                                , '{ nfe.NotaFiscalEletronica.InformacoesNFe.Identificacao?.serie }'
                                , '{ nfe.NotaFiscalEletronica.InformacoesNFe.Emitente?.Endereco?.xBairro }'
                                , '{ nfe.NotaFiscalEletronica.InformacoesNFe.Destinatario?.Endereco?.xBairro }'
                                , '{ nfe.NotaFiscalEletronica.InformacoesNFe.Emitente?.xFant }'
                                , '{ nfe.NotaFiscalEletronica.InformacoesNFe.Emitente?.Endereco?.xLgr }'
                                , '{ nfe.NotaFiscalEletronica.InformacoesNFe.Destinatario?.Endereco?.xLgr }'
                                , '{ nfe.NotaFiscalEletronica.InformacoesNFe.Emitente?.Endereco?.xMun }'
                                , '{ nfe.NotaFiscalEletronica.InformacoesNFe.Destinatario?.Endereco?.xMun }'
                                , '{ nfe.NotaFiscalEletronica.InformacoesNFe.Emitente?.xNome }'
                                , '{ nfe.NotaFiscalEletronica.InformacoesNFe.Destinatario?.xNome }'
                                , '{ nfe.NotaFiscalEletronica.InformacoesNFe.Emitente?.Endereco?.xPais }'
                                , '{ nfe.NotaFiscalEletronica.InformacoesNFe.Destinatario?.Endereco?.xPais }');";

                        rows = cmd.ExecuteNonQuery();
                    }

                    conn.Close();
                }

                return rows > 0;
            }
            catch (NpgsqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
