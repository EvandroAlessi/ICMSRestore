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
    public class NFeDAO : PaginationBuilder
    {
        static string connString = AppSettings.ConnectionString;
        const string quote = "\"";

        public string BuildInsertQuery(NFe nfe, bool hasReturn = false)
        {
            var query = $@"INSERT INTO {quote}NFe{ quote} (
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
                                , {quote}vBC_TOTAL{quote}
                                , {quote}vICMS_TOTAL{quote}
                                , {quote}vICMSDeson_TOTAL{quote}
                                , {quote}vFCP_TOTAL{quote}
                                , {quote}vBCST_TOTAL{quote}
                                , {quote}vST_TOTAL{quote}
                                , {quote}vFCPST_TOTAL{quote}
                                , {quote}vFCPSTRet_TOTAL{quote}
                                , {quote}vProd_TOTAL{quote}
                                , {quote}vFrete_TOTAL{quote}
                                , {quote}vSeg_TOTAL{quote}
                                , {quote}vDesc_TOTAL{quote}
                                , {quote}vII_TOTAL{quote}
                                , {quote}vIPI_TOTAL{quote}
                                , {quote}vIPIDevol_TOTAL{quote}
                                , {quote}vPIS_TOTAL{quote}
                                , {quote}vCOFINS_TOTAL{quote}
                                , {quote}vOutro_TOTAL{quote}
                                , {quote}vNF_TOTAL{quote}
                                , {quote}Entrada{quote}
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
                                , '{ nfe.dhSaiEnt }'
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
                query += $"RETURNING {quote}ID{ quote}";
            }

            query += ";";

            return query;
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
                        cmd.CommandText = $@"SELECT * FROM {quote}NFe{ quote}
                                            { DynamicWhere.BuildFilters(filters) }
                                            ORDER BY {quote}ID{ quote} desc
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
                        cmd.CommandText = $@"SELECT {quote}ID{quote}
                                                , {quote}ProcessoID{quote}
                                                , {quote}CNPJ{quote}
                                                , {quote}Entrada{quote}
                                                , {quote}cNF{quote}
                                                , {quote}nNF{quote}
                                                , {quote}dhEmi{quote}
                                                , {quote}dhSaiEnt{quote}
                                                , {quote}vNF_TOTAL{quote}
                                            FROM {quote}NFe{quote}
                                            { DynamicWhere.BuildFilters(filters) }
                                            ORDER BY {quote}ID{quote} desc
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
                        cmd.CommandText = $@"SELECT * FROM {quote}NFe{quote} 
                                WHERE {quote}ID{quote} = { id };";

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
                        cmd.CommandText = $@"SELECT {quote}ID{quote} FROM {quote}NFe{quote} 
                                WHERE {quote}ID{quote} = { id };";

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
                        cmd.CommandText = $@"SELECT {quote}ID{quote} FROM {quote}NFe{quote} 
                                WHERE {quote}cNF{quote} = { cNF } 
                                    AND {quote}nNF{quote} = { nNF }
                                    AND {quote}ProcessoID{quote} = { processoID } ;";

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

                                var nfeID = (int)cmd.ExecuteScalar();

                                if (nfeID <= 0)
                                {
                                    transaction.Rollback();

                                    return false;
                                }

                                using (var cmd2 = conn.CreateCommand())
                                {
                                    var query2 = "";

                                    for (int i = 0; i < itens.Count; i++)
                                    {
                                        if (i == 0)
                                        {
                                            query2 = $@"INSERT INTO {quote}Itens{ quote} (
                                                {quote}nItem{quote}
                                                , {quote}cProd{quote}
                                                , {quote}cEAN{quote}
                                                , {quote}xProd{quote}
                                                , {quote}NCM{quote}
                                                , {quote}CFOP{quote}
                                                , {quote}uCom{quote}
                                                , {quote}qCom{quote}
                                                , {quote}vUnCom{quote}
                                                , {quote}orig{quote}
                                                , {quote}CST{quote}
                                                , {quote}modBC{quote}
                                                , {quote}vBC{quote}
                                                , {quote}pICMS{quote}
                                                , {quote}vICMS{quote}
                                                , {quote}cEnq{quote}
                                                , {quote}CST_IPI{quote}
                                                , {quote}CST_PIS{quote}
                                                , {quote}vBC_PIS{quote}
                                                , {quote}pPIS{quote}
                                                , {quote}vPIS{quote}
                                                , {quote}CST_COFINS{quote}
                                                , {quote}vBC_COFINS{quote}
                                                , {quote}pCOFINS{quote}
                                                , {quote}vCOFINS{quote}
                                                , {quote}NFeID{quote}
                                                , {quote}CSOSN{quote}
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
                                                , { NullableUtils.TestValue(itens[i].CSOSN) })";
                                        }
                                        else
                                        {
                                            query2 += $@", (
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
                                                , { NullableUtils.TestValue(itens[i].CSOSN) })";
                                        }

                                        if (i == itens.Count - 1)
                                        {
                                            query2 += ";";
                                        }
                                    }

                                    cmd2.Transaction = transaction;
                                    cmd2.CommandType = CommandType.Text;
                                    cmd2.CommandText = query2;

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
                            WHERE {quote}ID{quote} = { nfe.ID };";

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
                        cmd.CommandText = $@"DELETE FROM {quote}NFe{quote}
                            WHERE {quote}ID{quote} = { id };";

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
