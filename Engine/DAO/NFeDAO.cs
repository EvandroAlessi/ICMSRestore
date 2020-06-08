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
                                    indPag = Convert.ToInt32(reader["indPag"]),
                                    mod = reader["mod"]?.ToString(),
                                    nNF = reader["nNF"]?.ToString(),
                                    natOp = reader["natOp"]?.ToString(),
                                    nro = reader["nro"]?.ToString(),
                                    nro_DEST = reader["nro_DEST"]?.ToString(),
                                    serie = Convert.ToInt32(reader["serie"]),
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
                                    xPais_DEST = reader["xPais_DEST"]?.ToString(),
                                    ProcessoID = Convert.ToInt32(reader["ProcessoID"])
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

        public async Task<NFe> Get(int cNF)
        {
            try
            {
                NFe nfe = null;

                using (var conn = new NpgsqlConnection(connString))
                {
                    await conn.OpenAsync();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $@"SELECT * FROM {quote}NFe{quote} 
                                WHERE {quote}cNF{quote} = { cNF };";

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                nfe = new NFe
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
                                    indPag = Convert.ToInt32(reader["indPag"]),
                                    mod = reader["mod"]?.ToString(),
                                    nNF = reader["nNF"]?.ToString(),
                                    natOp = reader["natOp"]?.ToString(),
                                    nro = reader["nro"]?.ToString(),
                                    nro_DEST = reader["nro_DEST"]?.ToString(),
                                    serie = Convert.ToInt32(reader["serie"]),
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
                                    xPais_DEST = reader["xPais_DEST"]?.ToString(),
                                    ProcessoID = Convert.ToInt32(reader["ProcessoID"])
                                };
                            }
                        }
                    }

                    await conn.CloseAsync();
                }

                return nfe;
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

        public NFe Insert(NFe nfe)
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
                                { nfe.ProcessoID }
                                , '{ nfe.CEP }'
                                , '{ nfe.CEP_DEST }'
                                , '{ nfe.CNPJ }'
                                , '{ nfe.CNPJ_DEST }'
                                , '{ nfe.CPF_DEST }'
                                , { nfe.CRT }
                                , '{ nfe.IE }'
                                , '{ nfe.IEST }'
                                , '{ nfe.UF }'
                                , '{ nfe.UF_DEST }'
                                , '{ nfe.cMun }'
                                , '{ nfe.cMun_DEST }'
                                , { nfe.cNF }
                                , { nfe.cPais }
                                , { nfe.cPais_DEST }
                                , { nfe.cUF }
                                , '{ nfe.dhEmi }'
                                , '{ nfe.dhSaiEnt }'
                                , '{ nfe.email_DEST }'
                                , { nfe.indPag }
                                , '{ nfe.mod }'
                                , '{ nfe.nNF }'
                                , '{ nfe.natOp }'
                                , '{ nfe.nro }'
                                , '{ nfe.nro_DEST }'
                                , { nfe.serie }
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

                if (rows > 0)
                {
                    return nfe;
                }
                else
                {
                    return null;
                }
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

        public bool InserWithoutObjReturn(NFe nfe)
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
                                { nfe.ProcessoID }
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

        public bool Insert(CrossCutting.SerializationModels.NFeProc nfe, int processoID)
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
                                , '{ nfe.NotaFiscalEletronica.InformacoesNFe.Emitente?.Endereco.CEP }'
                                , '{ nfe.NotaFiscalEletronica.InformacoesNFe.Destinatario?.Endereco.CEP }'
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

        public bool Edit(NFe nfe)
        {
            try
            {
                int rows = 0;

                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $@"UPDATE {quote}NFe{quote} SET
                                {quote}ProcessoID{quote} = { nfe.ProcessoID }
                                , {quote}CEP{quote} = '{ nfe.CEP }'
                                , {quote}CEP_DEST{quote} = '{ nfe.CEP_DEST }'
                                , {quote}CNPJ{quote} = '{ nfe.CNPJ }'
                                , {quote}CNPJ_DEST{quote} = '{ nfe.CNPJ_DEST }'
                                , {quote}CPF_DEST{quote} = '{ nfe.CPF_DEST }'
                                , {quote}CRT{quote} = { nfe.CRT }
                                , {quote}IE{quote} = '{ nfe.IE }'
                                , {quote}IEST{quote} = '{ nfe.IEST }'
                                , {quote}UF{quote} = '{ nfe.UF }'
                                , {quote}UF_DEST{quote} = '{ nfe.UF_DEST }'
                                , {quote}cMun{quote} = '{ nfe.cMun }'
                                , {quote}cMun_DEST{quote} = '{ nfe.cMun_DEST }'
                                , {quote}cNF{quote} = '{ nfe.cNF }'
                                , {quote}cPais{quote} = '{ nfe.cPais }'
                                , {quote}cPais_DEST{quote} = '{ nfe.cPais_DEST }'
                                , {quote}cUF{quote} = '{ nfe.cUF }'
                                , {quote}dhEmi{quote} = '{ nfe.dhEmi }'
                                , {quote}dhSaiEnt{quote} = '{ nfe.dhSaiEnt }'
                                , {quote}email_DEST{quote} = '{ nfe.email_DEST }'
                                , {quote}indPag{quote} = '{ nfe.indPag }'
                                , {quote}mod{quote} = '{ nfe.mod }'
                                , {quote}nNF{quote} = '{ nfe.nNF }'
                                , {quote}natOp{quote} = '{ nfe.natOp }'
                                , {quote}nro{quote} = '{ nfe.nro }'
                                , {quote}nro_DEST{quote} = '{ nfe.nro_DEST }'
                                , {quote}serie{quote} = '{ nfe.serie }'
                                , {quote}xBairro{quote} = '{ nfe.xBairro }'
                                , {quote}xBairro_DEST{quote} = '{ nfe.xBairro_DEST }'
                                , {quote}xFant{quote} = '{ nfe.xFant }'
                                , {quote}xLgr{quote} = '{ nfe.xLgr }'
                                , {quote}xLgr_DEST{quote} = '{ nfe.xLgr_DEST }'
                                , {quote}xMun{quote} = '{ nfe.xMun }'
                                , {quote}xMun_DEST{quote} = '{ nfe.xMun_DEST }'
                                , {quote}xNome{quote} = '{ nfe.xNome }'
                                , {quote}xNome_DEST{quote} = '{ nfe.xNome_DEST }'
                                , {quote}xPais{quote} = '{ nfe.xPais }'
                                , {quote}xPais_DEST{quote} = '{ nfe.xPais_DEST }'
                            WHERE {quote}cNF{quote} = { nfe.cNF };";

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

        public bool Delete(int cNF)
        {
            try
            {
                int rows = 0;

                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $@"DELETE FROM {quote}NFe{quote}
                            WHERE {quote}cNF{quote} = { cNF };";

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
