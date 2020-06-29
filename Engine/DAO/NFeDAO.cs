using CrossCutting;
using CrossCutting.Models;
using Dominio;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DAO
{
    public class NFeDAO : CommonDAO
    {
        public NFeDAO() => table = "\"NFe\"";

        public string BuildItensInsertQuery(List<Item> itens, int nfeID)
        {
            var query = "";

            for (int i = 0; i < itens.Count; i++)
            {
                if (i == 0)
                {
                    query = $@"INSERT INTO ""Itens"" (
                                ""nItem""
                                , ""cProd""
                                , ""cEAN""
                                , ""xProd""
                                , ""NCM""
                                , ""CFOP""
                                , ""uCom""
                                , ""qCom""
                                , ""vUnCom""
                                , ""orig""
                                , ""CST""
                                , ""modBC""
                                , ""vBC""
                                , ""pICMS""
                                , ""vICMS""
                                , ""cEnq""
                                , ""CST_IPI""
                                , ""CST_PIS""
                                , ""vBC_PIS""
                                , ""pPIS""
                                , ""vPIS""
                                , ""CST_COFINS""
                                , ""vBC_COFINS""
                                , ""pCOFINS""
                                , ""vCOFINS""
                                , ""NFeID""
                                , ""CSOSN""
                                , ""vProd""
                                , ""cEANTrib""
                                , ""uTrib""
                                , ""qTrib""
                                , ""vUnTrib""
                                , ""indTot""
                            ) VALUES (
                                { itens[i].nItem }
                                , '{ itens[i].cProd }'
                                , '{ itens[i].cEAN }'
                                , '{ itens[i].xProd }'
                                , { NullableUtils.TestValue(itens[i].NCM) }
                                , { NullableUtils.TestValue(itens[i].CFOP) }
                                , '{ itens[i].uCom }'
                                , { NullableUtils.TestValue(itens[i].qCom) }
                                , { NullableUtils.TestValue(itens[i].vUnCom) }
                                , { NullableUtils.TestValue(itens[i].orig) }
                                , { NullableUtils.TestValue(itens[i].CST) }
                                , { NullableUtils.TestValue(itens[i].modBC) }
                                , { NullableUtils.TestValue(itens[i].vBC) }
                                , { NullableUtils.TestValue(itens[i].pICMS) }
                                , { NullableUtils.TestValue(itens[i].vICMS) }
                                , { NullableUtils.TestValue(itens[i].cEnq) }
                                , '{ itens[i].CST_IPI }'
                                , '{ itens[i].CST_PIS }'
                                , { NullableUtils.TestValue(itens[i].vBC_PIS) }
                                , { NullableUtils.TestValue(itens[i].pPIS) }
                                , { NullableUtils.TestValue(itens[i].vPIS) }
                                , '{ itens[i].CST_COFINS }'
                                , { NullableUtils.TestValue(itens[i].vBC_COFINS) }
                                , { NullableUtils.TestValue(itens[i].pCOFINS) }
                                , { NullableUtils.TestValue(itens[i].vCOFINS) }
                                , { nfeID }
                                , { NullableUtils.TestValue(itens[i].CSOSN) }
                                , { NullableUtils.TestValue(itens[i].vProd) }
                                , '{ itens[i].cEANTrib }'
                                , '{ itens[i].uTrib }'
                                , { NullableUtils.TestValue(itens[i].qTrib) }
                                , { NullableUtils.TestValue(itens[i].vUnTrib) }
                                , { NullableUtils.TestValue(itens[i].indTot) }
                            )";
                }
                else
                {
                    query += $@", (
                                { itens[i].nItem }
                                , '{ itens[i].cProd }'
                                , '{ itens[i].cEAN }'
                                , '{ itens[i].xProd }'
                                , { NullableUtils.TestValue(itens[i].NCM) }
                                , { NullableUtils.TestValue(itens[i].CFOP) }
                                , '{ itens[i].uCom }'
                                , { NullableUtils.TestValue(itens[i].qCom) }
                                , { NullableUtils.TestValue(itens[i].vUnCom) }
                                , { NullableUtils.TestValue(itens[i].orig) }
                                , { NullableUtils.TestValue(itens[i].CST) }
                                , { NullableUtils.TestValue(itens[i].modBC) }
                                , { NullableUtils.TestValue(itens[i].vBC) }
                                , { NullableUtils.TestValue(itens[i].pICMS) }
                                , { NullableUtils.TestValue(itens[i].vICMS) }
                                , { NullableUtils.TestValue(itens[i].cEnq) }
                                , '{ itens[i].CST_IPI }'
                                , '{ itens[i].CST_PIS }'
                                , { NullableUtils.TestValue(itens[i].vBC_PIS) }
                                , { NullableUtils.TestValue(itens[i].pPIS) }
                                , { NullableUtils.TestValue(itens[i].vPIS) }
                                , '{ itens[i].CST_COFINS }'
                                , { NullableUtils.TestValue(itens[i].vBC_COFINS) }
                                , { NullableUtils.TestValue(itens[i].pCOFINS) }
                                , { NullableUtils.TestValue(itens[i].vCOFINS) }
                                , { nfeID }
                                , { NullableUtils.TestValue(itens[i].CSOSN) }
                                , { NullableUtils.TestValue(itens[i].vProd) }
                                , '{ itens[i].cEANTrib }'
                                , '{ itens[i].uTrib }'
                                , { NullableUtils.TestValue(itens[i].qTrib) }
                                , { NullableUtils.TestValue(itens[i].vUnTrib) }
                                , { NullableUtils.TestValue(itens[i].indTot) }
                            )";
                }

                if (i == itens.Count - 1)
                {
                    query += ";";
                }
            }

            return query.Replace("\' \'", "null").Replace("\'\'", "null");
        }

        public string BuildInsertQuery(NFe nfe, bool hasReturn = false)
        {
            var query = $@"INSERT INTO { table } (
                                ""ProcessoID""
                                , ""CEP""
                                , ""CEP_DEST""
                                , ""CNPJ""
                                , ""CNPJ_DEST""
                                , ""CPF_DEST""
                                , ""CRT""
                                , ""IE""
                                , ""IEST""
                                , ""UF""
                                , ""UF_DEST""
                                , ""cMun""
                                , ""cMun_DEST""
                                , ""cNF""
                                , ""cPais""
                                , ""cPais_DEST""
                                , ""cUF""
                                , ""dhEmi""
                                , ""dhSaiEnt""
                                , ""email_DEST""
                                , ""indPag""
                                , ""mod""
                                , ""nNF""
                                , ""natOp""
                                , ""nro""
                                , ""nro_DEST""
                                , ""serie""
                                , ""xBairro""
                                , ""xBairro_DEST""
                                , ""xFant""
                                , ""xLgr""
                                , ""xLgr_DEST""
                                , ""xMun""
                                , ""xMun_DEST""
                                , ""xNome""
                                , ""xNome_DEST""
                                , ""xPais""
                                , ""xPais_DEST""
                                , ""vBC_TOTAL""
                                , ""vICMS_TOTAL""
                                , ""vICMSDeson_TOTAL""
                                , ""vFCP_TOTAL""
                                , ""vBCST_TOTAL""
                                , ""vST_TOTAL""
                                , ""vFCPST_TOTAL""
                                , ""vFCPSTRet_TOTAL""
                                , ""vProd_TOTAL""
                                , ""vFrete_TOTAL""
                                , ""vSeg_TOTAL""
                                , ""vDesc_TOTAL""
                                , ""vII_TOTAL""
                                , ""vIPI_TOTAL""
                                , ""vIPIDevol_TOTAL""
                                , ""vPIS_TOTAL""
                                , ""vCOFINS_TOTAL""
                                , ""vOutro_TOTAL""
                                , ""vNF_TOTAL""
                                , ""Entrada""
                            ) VALUES (
                                { nfe.ProcessoID }
                                , '{ nfe.CEP }'
                                , '{ nfe.CEP_DEST }'
                                , '{ nfe.CNPJ }'
                                , '{ nfe.CNPJ_DEST }'
                                , '{ nfe.CPF_DEST }'
                                , { NullableUtils.TestValue(nfe.CRT) }
                                , '{ nfe.IE }'
                                , '{ nfe.IEST }'
                                , '{ nfe.UF }'
                                , '{ nfe.UF_DEST }'
                                , '{ nfe.cMun }'
                                , '{ nfe.cMun_DEST }'
                                , { NullableUtils.TestValue(nfe.cNF) }
                                , { NullableUtils.TestValue(nfe.cPais) }
                                , { NullableUtils.TestValue(nfe.cPais_DEST) }
                                , { NullableUtils.TestValue(nfe.cUF) }
                                , '{ nfe.dhEmi }'
                                , '{ nfe.dhSaiEnt?.ToString() }'
                                , '{ nfe.email_DEST }'
                                , { NullableUtils.TestValue(nfe.indPag) }
                                , '{ nfe.mod }'
                                , { NullableUtils.TestValue(nfe.nNF) }
                                , '{ nfe.natOp }'
                                , '{ nfe.nro }'
                                , '{ nfe.nro_DEST }'
                                , { NullableUtils.TestValue(nfe.serie) }
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
                                , '{ nfe.xPais_DEST }'
                                , { NullableUtils.TestValue(nfe.vBC_TOTAL) }
                                , { NullableUtils.TestValue(nfe.vICMS_TOTAL) }
                                , { NullableUtils.TestValue(nfe.vICMSDeson_TOTAL) }
                                , { NullableUtils.TestValue(nfe.vFCP_TOTAL) }
                                , { NullableUtils.TestValue(nfe.vBCST_TOTAL) }
                                , { NullableUtils.TestValue(nfe.vST_TOTAL) }
                                , { NullableUtils.TestValue(nfe.vFCPST_TOTAL) }
                                , { NullableUtils.TestValue(nfe.vFCPSTRet_TOTAL) }
                                , { NullableUtils.TestValue(nfe.vProd_TOTAL) }
                                , { NullableUtils.TestValue(nfe.vFrete_TOTAL) }
                                , { NullableUtils.TestValue(nfe.vSeg_TOTAL) }
                                , { NullableUtils.TestValue(nfe.vDesc_TOTAL) }
                                , { NullableUtils.TestValue(nfe.vII_TOTAL) }
                                , { NullableUtils.TestValue(nfe.vIPI_TOTAL) }
                                , { NullableUtils.TestValue(nfe.vIPIDevol_TOTAL) }
                                , { NullableUtils.TestValue(nfe.vPIS_TOTAL) }
                                , { NullableUtils.TestValue(nfe.vCOFINS_TOTAL) }
                                , { NullableUtils.TestValue(nfe.vOutro_TOTAL) }
                                , { NullableUtils.TestValue(nfe.vNF_TOTAL) }
                                , { nfe.Entrada }
                            )";

            if (hasReturn)
            {
                query += $@"RETURNING ""ID""";
            }

            query += ";";

            return query.Replace("\' \'", "null").Replace("\'\'", "null");
        }

        public NFe BuildObject(NpgsqlDataReader reader)
        {
            return new NFe
            {
                ID = Convert.ToInt32(reader["id"]),
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
                cPais_DEST = reader.GetFieldValue<int?>("cPais_DEST"),
                cUF = Convert.ToInt32(reader["cUF"]),
                dhEmi = Convert.ToDateTime(reader["dhEmi"]),
                dhSaiEnt = reader.GetFieldValue<DateTime?>("dhSaiEnt"),
                email_DEST = reader["email_DEST"]?.ToString(),
                indPag = Convert.ToInt32(reader["indPag"]),
                mod = reader["mod"]?.ToString(),
                nNF = Convert.ToInt32(reader["nNF"]),
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
                vBC_TOTAL = reader.GetFieldValue<double?>("vBC_TOTAL"),
                vICMS_TOTAL = reader.GetFieldValue<double?>("vICMS_TOTAL"),
                vICMSDeson_TOTAL = reader.GetFieldValue<double?>("vICMSDeson_TOTAL"),
                vFCP_TOTAL = reader.GetFieldValue<double?>("vFCP_TOTAL"),
                vBCST_TOTAL = reader.GetFieldValue<double?>("vBCST_TOTAL"),
                vST_TOTAL = reader.GetFieldValue<double?>("vST_TOTAL"),
                vFCPST_TOTAL = reader.GetFieldValue<double?>("vFCPST_TOTAL"),
                vFCPSTRet_TOTAL = reader.GetFieldValue<double?>("vFCPSTRet_TOTAL"),
                vProd_TOTAL = reader.GetFieldValue<double?>("vProd_TOTAL"),
                vFrete_TOTAL = reader.GetFieldValue<double?>("vFrete_TOTAL"),
                vSeg_TOTAL = reader.GetFieldValue<double?>("vSeg_TOTAL"),
                vDesc_TOTAL = reader.GetFieldValue<double?>("vDesc_TOTAL"),
                vII_TOTAL = reader.GetFieldValue<double?>("vII_TOTAL"),
                vIPI_TOTAL = reader.GetFieldValue<double?>("vIPI_TOTAL"),
                vIPIDevol_TOTAL = reader.GetFieldValue<double?>("vIPIDevol_TOTAL"),
                vPIS_TOTAL = reader.GetFieldValue<double?>("vPIS_TOTAL"),
                vCOFINS_TOTAL = reader.GetFieldValue<double?>("vCOFINS_TOTAL"),
                vOutro_TOTAL = reader.GetFieldValue<double?>("vOutro_TOTAL"),
                vNF_TOTAL = reader.GetFieldValue<double?>("vNF_TOTAL"),
                ProcessoID = Convert.ToInt32(reader["ProcessoID"])
            };
        }

        public SimplifiedInvoice BuildSimplifiedObject(NpgsqlDataReader reader)
        {
            return new SimplifiedInvoice
            {
                ID = Convert.ToInt32(reader["id"]),
                ProcessoID = Convert.ToInt32(reader["ProcessoID"]),
                cNF = Convert.ToInt32(reader["cNF"]),
                dhEmi = Convert.ToDateTime(reader["dhEmi"]),
                dhSaiEnt = reader.GetFieldValue<DateTime?>("dhSaiEnt"),
                nNF = Convert.ToInt32(reader["nNF"]),
                vNF_TOTAL = reader.GetFieldValue<double?>("vNF_TOTAL"),
                CNPJ = reader["CNPJ"]?.ToString()
            };
        }

        public async Task<List<NFe>> GetAll(int skip = 0, int take = 30, Dictionary<string, string> filters = null)
        {
            try
            {
                var list = new List<NFe>();

                using (var conn = new NpgsqlConnection(connString))
                {
                    await conn.OpenAsync();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $@"SELECT * FROM { table }
                                            { DynamicWhere.BuildFilters(filters) }
                                            ORDER BY ""ID"" desc
                                            LIMIT { take } 
                                            OFFSET { skip };";

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(BuildObject(reader));
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

        public async Task<List<SimplifiedInvoice>> GetAllSimplify(int skip = 0, int take = 30, Dictionary<string, string> filters = null)
        {
            try
            {
                var list = new List<SimplifiedInvoice>();

                using (var conn = new NpgsqlConnection(connString))
                {
                    await conn.OpenAsync();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $@"SELECT ""ID""
                                                , ""ProcessoID""
                                                , ""CNPJ""
                                                , ""Entrada""
                                                , ""cNF""
                                                , ""nNF""
                                                , ""dhEmi""
                                                , ""dhSaiEnt""
                                                , ""vNF_TOTAL""
                                            FROM { table }
                                            { DynamicWhere.BuildFilters(filters) }
                                            ORDER BY ""ID"" desc
                                            LIMIT { take } 
                                            OFFSET { skip };";

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(BuildSimplifiedObject(reader));
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

        public async Task<NFe> Get(int id)
        {
            try
            {
                NFe nfe = null;

                using (var conn = new NpgsqlConnection(connString))
                {
                    await conn.OpenAsync();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $@"SELECT * FROM { table } 
                                WHERE ""ID"" = { id };";

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                nfe = BuildObject(reader);
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

        public async Task<bool> Exists(int id)
        {
            try
            {
                bool exists = false;

                using (var conn = new NpgsqlConnection(connString))
                {
                    await conn.OpenAsync();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $@"SELECT ""ID"" FROM { table } 
                                WHERE ""ID"" = { id };";

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

        public async Task<bool> Exists(int cNF, int nNF, int processoID)
        {
            try
            {
                bool exists = false;

                using (var conn = new NpgsqlConnection(connString))
                {
                    await conn.OpenAsync();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $@"SELECT ""ID"" FROM { table } 
                                WHERE ""cNF"" = { cNF } 
                                    AND ""nNF"" = { nNF }
                                    AND ""ProcessoID"" = { processoID } ;";

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
                object id;

                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = BuildInsertQuery(nfe, true);

                        id = cmd.ExecuteScalar();
                    }

                    conn.Close();
                }

                if (id != null && id.GetType() != typeof(DBNull))
                {
                    nfe.ID = (int)id;

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

        public bool InsertNFeItensTransaction(NFe nfe, List<Item> itens)
        {
            try
            {
                int rows = 0;

                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();

                    using (var transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            using (var cmd = conn.CreateCommand())
                            {
                                cmd.Transaction = transaction;
                                cmd.CommandType = CommandType.Text;
                                cmd.CommandText = BuildInsertQuery(nfe, true);
                                cmd.Prepare();

                                var nfeID = (int)cmd.ExecuteScalar();

                                if (nfeID <= 0)
                                {
                                    transaction.Rollback();

                                    return false;
                                }

                                using (var cmd2 = conn.CreateCommand())
                                {
                                    cmd2.Transaction = transaction;
                                    cmd2.CommandType = CommandType.Text;
                                    cmd2.CommandText = BuildItensInsertQuery(itens, nfeID);
                                    cmd2.Prepare();

                                    rows = cmd2.ExecuteNonQuery();
                                }

                                if (rows > 0)
                                    transaction.Commit();
                                else
                                    transaction.Rollback();
                            }
                        }
                        catch (NpgsqlException ex)
                        {
                            transaction.Rollback();
                            throw ex;
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw ex;
                        }
                    }

                    conn.Close();
                }

                return rows > 0;
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
                        cmd.CommandText = BuildInsertQuery(nfe);

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

        /// <summary>
        /// Insert based on Deserialized xml objects
        /// </summary>
        /// <param name="nfe"></param>
        /// <param name="processoID"></param>
        /// <returns></returns>
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
                        cmd.CommandText = $@"INSERT INTO { table } (
                                ""ProcessoID""
                                , ""CEP""
                                , ""CEP_DEST""
                                , ""CNPJ""
                                , ""CNPJ_DEST""
                                , ""CPF_DEST""
                                , ""CRT""
                                , ""IE""
                                , ""IEST""
                                , ""UF""
                                , ""UF_DEST""
                                , ""cMun""
                                , ""cMun_DEST""
                                , ""cNF""
                                , ""cPais""
                                , ""cPais_DEST""
                                , ""cUF""
                                , ""dhEmi""
                                , ""dhSaiEnt""
                                , ""email_DEST""
                                , ""indPag""
                                , ""mod""
                                , ""nNF""
                                , ""natOp""
                                , ""nro""
                                , ""nro_DEST""
                                , ""serie""
                                , ""xBairro""
                                , ""xBairro_DEST""
                                , ""xFant""
                                , ""xLgr""
                                , ""xLgr_DEST""
                                , ""xMun""
                                , ""xMun_DEST""
                                , ""xNome""
                                , ""xNome_DEST""
                                , ""xPais""
                                , ""xPais_DEST""
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

        public NFe Edit(NFe nfe)
        {
            try
            {
                int rows = 0;

                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $@"UPDATE { table } SET
                                ""ProcessoID"" = { nfe.ProcessoID }
                                , ""CEP"" = '{ nfe.CEP }'
                                , ""CEP_DEST"" = '{ nfe.CEP_DEST }'
                                , ""CNPJ"" = '{ nfe.CNPJ }'
                                , ""CNPJ_DEST"" = '{ nfe.CNPJ_DEST }'
                                , ""CPF_DEST"" = '{ nfe.CPF_DEST }'
                                , ""CRT"" = { nfe.CRT }
                                , ""IE"" = '{ nfe.IE }'
                                , ""IEST"" = '{ nfe.IEST }'
                                , ""UF"" = '{ nfe.UF }'
                                , ""UF_DEST"" = '{ nfe.UF_DEST }'
                                , ""cMun"" = '{ nfe.cMun }'
                                , ""cMun_DEST"" = '{ nfe.cMun_DEST }'
                                , ""cNF"" = '{ nfe.cNF }'
                                , ""cPais"" = '{ nfe.cPais }'
                                , ""cPais_DEST"" = '{ nfe.cPais_DEST }'
                                , ""cUF"" = '{ nfe.cUF }'
                                , ""dhEmi"" = '{ nfe.dhEmi }'
                                , ""dhSaiEnt"" = '{ nfe.dhSaiEnt }'
                                , ""email_DEST"" = '{ nfe.email_DEST }'
                                , ""indPag"" = '{ nfe.indPag }'
                                , ""mod"" = '{ nfe.mod }'
                                , ""nNF"" = '{ nfe.nNF }'
                                , ""natOp"" = '{ nfe.natOp }'
                                , ""nro"" = '{ nfe.nro }'
                                , ""nro_DEST"" = '{ nfe.nro_DEST }'
                                , ""serie"" = '{ nfe.serie }'
                                , ""xBairro"" = '{ nfe.xBairro }'
                                , ""xBairro_DEST"" = '{ nfe.xBairro_DEST }'
                                , ""xFant"" = '{ nfe.xFant }'
                                , ""xLgr"" = '{ nfe.xLgr }'
                                , ""xLgr_DEST"" = '{ nfe.xLgr_DEST }'
                                , ""xMun"" = '{ nfe.xMun }'
                                , ""xMun_DEST"" = '{ nfe.xMun_DEST }'
                                , ""xNome"" = '{ nfe.xNome }'
                                , ""xNome_DEST"" = '{ nfe.xNome_DEST }'
                                , ""xPais"" = '{ nfe.xPais }'
                                , ""xPais_DEST"" = '{ nfe.xPais_DEST }'
                            WHERE ""ID"" = { nfe.ID };";

                        rows = cmd.ExecuteNonQuery();
                    }

                    conn.Close();
                }

                return rows > 0 ? nfe : null;
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

        public bool Delete(int id)
        {
            try
            {
                int rows = 0;

                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $@"DELETE FROM { table }
                            WHERE ""ID"" = { id };";

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
