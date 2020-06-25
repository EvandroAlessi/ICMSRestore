using CrossCutting;
using CrossCutting.Models;
using Dominio;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace DAO
{
    public class ItemDAO : CommonDAO
    {
        public ItemDAO() => table = "\"Itens\"";

        public Item BuildObject(NpgsqlDataReader reader)
        {
            return new Item
            {
                nItem = reader.GetFieldValue<int>("nItem"),
                cProd = reader["cProd"]?.ToString(),
                cEAN = reader["cEAN"]?.ToString(),
                xProd = reader["xProd"]?.ToString(),
                NCM = reader.GetFieldValue<int>("NCM"),
                CFOP = reader.GetFieldValue<int>("CFOP"),
                uCom = reader["uCom"]?.ToString(),
                qCom = reader.GetFieldValue<double?>("qCom"),
                vUnCom = reader.GetFieldValue<double?>("vUnCom"),
                orig = reader.GetFieldValue<int?>("orig"),
                CST = reader.GetFieldValue<int?>("CST"),
                modBC = reader.GetFieldValue<int?>("modBC"),
                vBC = reader.GetFieldValue<double?>("vBC"),
                pICMS = reader.GetFieldValue<double?>("pICMS"),
                vICMS = reader.GetFieldValue<double?>("vICMS"),
                cEnq = reader.GetFieldValue<int?>("cEnq"),
                CST_IPI = reader["CST_IPI"]?.ToString(),
                CST_PIS = reader["CST_PIS"]?.ToString(),
                vBC_PIS = reader.GetFieldValue<double?>("vBC_PIS"),
                pPIS = reader.GetFieldValue<double?>("pPIS"),
                vPIS = reader.GetFieldValue<double?>("vPIS"),
                CST_COFINS = reader["CST_COFINS"]?.ToString(),
                vBC_COFINS = reader.GetFieldValue<double?>("vBC_COFINS"),
                pCOFINS = reader.GetFieldValue<double?>("pCOFINS"),
                vCOFINS = reader.GetFieldValue<double?>("vCOFINS"),
                NFeID = reader.GetFieldValue<int>("NFeID"),
                ID = reader.GetFieldValue<int>("ID"),
                vProd = reader.GetFieldValue<double?>("vProd"),
                cEANTrib = reader["cEANTrib"]?.ToString(),
                uTrib = reader["uTrib"]?.ToString(),
                qTrib = reader.GetFieldValue<double?>("qTrib"),
                vUnTrib = reader.GetFieldValue<double?>("vUnTrib"),
                indTot = reader.GetFieldValue<int?>("indTot"),
            };
        }

        public async Task<List<Item>> GetAll(int skip = 0, int take = 30, Dictionary<string, string> filters = null)
        {
            try
            {
                var list = new List<Item>();

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

        public async Task<List<Item>> GetAll(int nfeID)
        {
            try
            {
                var list = new List<Item>();

                using (var conn = new NpgsqlConnection(connString))
                {
                    await conn.OpenAsync();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $@"SELECT * FROM { table }
                                    WHERE ""ProcessID"" = { nfeID };";

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

        public async Task<Item> Get(int id)
        {
            try
            {
                Item item = null;

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
                                item = BuildObject(reader);
                            }
                        }
                    }

                    await conn.CloseAsync();
                }

                return item;
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

        public Item Insert(Item item)
        {
            try
            {
                object id;

                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();

                    using (var cmd = conn.CreateCommand())
                    {

                        cmd.CommandText = $@"INSERT INTO { table } (
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
                                , ""vProd""
                                , ""cEANTrib""
                                , ""uTrib""
                                , ""qTrib""
                                , ""vUnTrib""
                                , ""indTot""
                            ) VALUES (
                                { item.nItem }
                                , '{ item.cProd }'
                                , '{ item.cEAN }'
                                , '{ item.xProd }'
                                , { NullableUtils.TestValue(item.NCM) }
                                , { NullableUtils.TestValue(item.CFOP) }
                                , '{ item.uCom }'
                                , { NullableUtils.TestValue(item.qCom) }
                                , { NullableUtils.TestValue(item.vUnCom) }
                                , { NullableUtils.TestValue(item.orig) }
                                , { NullableUtils.TestValue(item.CST) }
                                , { NullableUtils.TestValue(item.modBC) }
                                , { NullableUtils.TestValue(item.vBC) }
                                , { NullableUtils.TestValue(item.pICMS) }
                                , { NullableUtils.TestValue(item.vICMS) }
                                , { NullableUtils.TestValue(item.cEnq) }
                                , '{ item.CST_IPI }'
                                , '{ item.CST_PIS }'
                                , { NullableUtils.TestValue(item.vBC_PIS) }
                                , { NullableUtils.TestValue(item.pPIS) }
                                , { NullableUtils.TestValue(item.vPIS) }
                                , '{ item.CST_COFINS }'
                                , { NullableUtils.TestValue(item.vBC_COFINS) }
                                , { NullableUtils.TestValue(item.pCOFINS) }
                                , { NullableUtils.TestValue(item.vCOFINS) }
                                , { item.NFeID }
                                , { NullableUtils.TestValue(item.vProd) }
                                , '{ item.cEANTrib }'
                                , '{ item.uTrib }'
                                , { NullableUtils.TestValue(item.qTrib) }
                                , { NullableUtils.TestValue(item.vUnTrib) }
                                , { NullableUtils.TestValue(item.indTot) }
                            )
                            RETURNING ""cProd"";";

                        id = cmd.ExecuteScalar();
                    }

                    conn.Close();
                }

                if (id != null && id.GetType() != typeof(DBNull))
                {
                    item.cProd = (string)id;

                    return item;
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

        public bool InserWithoutObjReturn(Item item)
        {
            var lastCulture = Thread.CurrentThread.CurrentCulture;

            try
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");

                int rows = 0;

                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();

                    using (var cmd = conn.CreateCommand())
                    {

                        cmd.CommandText = $@"INSERT INTO { table } (
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
                                , ""vProd""
                                , ""cEANTrib""
                                , ""uTrib""
                                , ""qTrib""
                                , ""vUnTrib""
                                , ""indTot""
                            ) VALUES (
                                { item.nItem }
                                , '{ item.cProd }'
                                , '{ item.cEAN }'
                                , '{ item.xProd }'
                                , '{ item.NCM }'
                                , '{ item.CFOP }'
                                , '{ item.uCom }'
                                , { NullableUtils.TestValue(item.qCom) }
                                , { NullableUtils.TestValue(item.vUnCom) }
                                , { NullableUtils.TestValue(item.orig) }
                                , '{ item.CST }'
                                , { NullableUtils.TestValue(item.modBC) }
                                , { NullableUtils.TestValue(item.vBC) }
                                , { NullableUtils.TestValue(item.pICMS) }
                                , { NullableUtils.TestValue(item.vICMS) }
                                , { NullableUtils.TestValue(item.cEnq) }
                                , '{ item.CST_IPI }'
                                , '{ item.CST_PIS }'
                                , { NullableUtils.TestValue(item.vBC_PIS) }
                                , { NullableUtils.TestValue(item.pPIS) }
                                , { NullableUtils.TestValue(item.vPIS) }
                                , '{ item.CST_COFINS }'
                                , { NullableUtils.TestValue(item.vBC_COFINS) }
                                , { NullableUtils.TestValue(item.pCOFINS) }
                                , { NullableUtils.TestValue(item.vCOFINS) }
                                , { item.NFeID }
                                , { NullableUtils.TestValue(item.vProd) }
                                , '{ item.cEANTrib }'
                                , '{ item.uTrib }'
                                , { NullableUtils.TestValue(item.qTrib) }
                                , { NullableUtils.TestValue(item.vUnTrib) }
                                , { NullableUtils.TestValue(item.indTot) }
                            );";

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
            finally
            {
                Thread.CurrentThread.CurrentCulture = lastCulture;
            }
        }

        public bool Insert(CrossCutting.SerializationModels.Detalhe detalhe, int cNFe)
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
                                , ""cNF""
                            ) VALUES (
                                '{ detalhe.nItem }'
                                , '{ detalhe.Produto.cProd }'
                                , '{ detalhe.Produto.cEAN }'
                                , '{ detalhe.Produto.xProd }'
                                , '{ detalhe.Produto.NCM }'
                                , '{ detalhe.Produto.CFOP }'
                                , '{ detalhe.Produto.uCom }'
                                , '{ detalhe.Produto.qCom }'
                                , '{ detalhe.Produto.vUnCom }'
                                , '{ detalhe.Imposto.ICMS.ICMS00.orig }'
                                , '{ detalhe.Imposto.ICMS.ICMS00.CST }'
                                , '{ detalhe.Imposto.ICMS.ICMS00.modBC }'
                                , '{ detalhe.Imposto.ICMS.ICMS00.vBC }'
                                , '{ detalhe.Imposto.ICMS.ICMS00.pICMS }'
                                , '{ detalhe.Imposto.ICMS.ICMS00.vICMS }'
                                , '{ detalhe.Imposto.IPI.cEnq }'
                                , '{ detalhe.Imposto.IPI.IPINT.CST }'
                                , '{ detalhe.Imposto.PIS.PISAliq.CST }'
                                , '{ detalhe.Imposto.PIS.PISAliq.vBC }'
                                , '{ detalhe.Imposto.PIS.PISAliq.pPIS }'
                                , '{ detalhe.Imposto.PIS.PISAliq.vPIS }'
                                , '{ detalhe.Imposto.COFINS.COFINSAliq.CST }'
                                , '{ detalhe.Imposto.COFINS.COFINSAliq.vBC }'
                                , '{ detalhe.Imposto.COFINS.COFINSAliq.pCOFINS }'
                                , '{ detalhe.Imposto.COFINS.COFINSAliq.vCOFINS }'
                                , '{ cNFe }');";

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

        public Item Edit(Item item)
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
                                ""nItem"" = '{ item.nItem }'
                                , ""cProd"" = '{ item.cProd }'
                                , ""cEAN"" = '{ item.cEAN }'
                                , ""xProd"" = '{ item.xProd }'
                                , ""NCM"" = '{ item.NCM }'
                                , ""CFOP"" = '{ item.CFOP }'
                                , ""uCom"" = '{ item.uCom }'
	                            , ""qCom"" = '{ item.qCom }'
                                , ""vUnCom"" = '{ item.vUnCom }'
                                , ""orig"" = '{ item.orig }'
                                , ""CST"" = '{ item.CST }'
                                , ""modBC"" = '{ item.modBC }'
                                , ""vBC"" = '{ item.vBC }'
                                , ""pICMS"" = '{ item.pICMS }'
                                , ""vICMS"" = '{ item.vICMS }'
	                            , ""cEnq"" = '{ item.cEnq }'
                                , ""CST_IPI"" = '{ item.CST_IPI }'
                                , ""CST_PIS"" = '{ item.CST_PIS }'
                                , ""vBC_PIS"" = '{ item.vBC_PIS }'
                                , ""pPIS"" = '{ item.pPIS }'
                                , ""vPIS"" = '{ item.vPIS }'
                                , ""CST_COFINS"" = '{ item.CST_COFINS }'
	                            , ""vBC_COFINS"" = '{ item.vBC_COFINS }'
                                , ""pCOFINS"" = '{ item.pCOFINS }'
                                , ""vCOFINS"" = '{ item.vCOFINS }'
                                , ""NFeID"" = '{ item.NFeID }'
                                , ""vProd"" = '{ item.vProd }'
                                , ""cEANTrib"" = '{ item.cEANTrib }'
                                , ""uTrib"" = '{ item.uTrib }'
                                , ""qTrib"" = '{ item.qTrib }'
                                , ""vUnTrib"" = '{ item.vUnTrib }'
                                , ""indTot"" = '{ item.indTot }'
                            WHERE ""ID"" = { item.ID };";

                        rows = cmd.ExecuteNonQuery();
                    }

                    conn.Close();
                }

                return rows > 0 ? item : null;
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
