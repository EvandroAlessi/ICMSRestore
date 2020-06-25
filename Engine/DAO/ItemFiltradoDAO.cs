using CrossCutting;
using Dominio;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DAO
{
    public class ItemFiltradoDAO : CommonDAO
    {
        public ItemFiltradoDAO() => table = "\"ItensFiltrados\"";

        public ItemFiltrado BuildObject(NpgsqlDataReader reader)
        {
            return new ItemFiltrado
            {
                ProcessoID = Convert.ToInt32(reader["ProcessoID"]),
                ItemID = Convert.ToInt32(reader["ItemID"]),
                cProd = reader["cProd"]?.ToString(),
                xProd = reader["xProd"]?.ToString(),
                NCM = Convert.ToInt32(reader["NCM"]),
                CFOP = Convert.ToInt32(reader["CFOP"]),
                uCom = reader["uCom"]?.ToString(),
                qCom = reader.GetFieldValue<double?>("qCom"),
                vUnCom = reader.GetFieldValue<double?>("vUnCom"),
                orig = reader.GetFieldValue<int?>("orig"),
                CST = reader.GetFieldValue<int?>("CST"),
                vBC = reader.GetFieldValue<double?>("vBC"),
                vICMS = reader.GetFieldValue<double?>("vICMS"),
                CSOSN = reader.GetFieldValue<int?>("CSOSN"),
                Entrada = Convert.ToBoolean(reader["Entrada"]),
                nNF = Convert.ToInt32(reader["nNF"]),
                dhEmi = Convert.ToDateTime(reader["dhEmi"]),
                dhSaiEnt = reader.GetFieldValue<DateTime?>("dhSaiEnt"),
            };
        }

        public async Task<List<ItemFiltrado>> GetAll(int skip = 0, int take = 30, Dictionary<string, string> filters = null)
        {
            try
            {
                var list = new List<ItemFiltrado>();

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

        public async Task<List<ItemFiltrado>> GetAll(int processID, int skip = 0, int take = 30)
        {
            try
            {
                var list = new List<ItemFiltrado>();

                using (var conn = new NpgsqlConnection(connString))
                {
                    await conn.OpenAsync();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $@"SELECT * FROM { table } 
                                            WHERE ""ProcessoID"" = { processID }
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

        public async Task<ItemFiltrado> Get(int id)
        {
            try
            {
                ItemFiltrado itemFiltrado = null;

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
                                itemFiltrado = BuildObject(reader);
                            }
                        }
                    }

                    await conn.CloseAsync();
                }

                return itemFiltrado;
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

        public ItemFiltrado Insert(ItemFiltrado itemFiltrado)
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
                                ""ProcessoID""
                                , ""ItemID""
                                , ""cProd""
                                , ""xProd""
                                , ""NCM""
                                , ""CFOP""
                                , ""uCom""
                                , ""qCom""
                                , ""vUnCom""
                                , ""orig""
                                , ""CST""
                                , ""vBC""
                                , ""vICMS""
                                , ""CSOSN""
                                , ""Entrada""
                                , ""nNF""
                                , ""dhEmi""
                                , ""dhSaiEnt""
                            ) VALUES (
                                { itemFiltrado.ProcessoID }
                                , { itemFiltrado.ItemID }
                                , '{ itemFiltrado.cProd }'
                                , '{ itemFiltrado.xProd }'
                                , { itemFiltrado.NCM }
                                , { NullableUtils.TestValue(itemFiltrado.CFOP) }
                                , { NullableUtils.TestValue(itemFiltrado.uCom) }
                                , { NullableUtils.TestValue(itemFiltrado.qCom) }
                                , { NullableUtils.TestValue(itemFiltrado.vUnCom) }
                                , { NullableUtils.TestValue(itemFiltrado.orig) }
                                , { NullableUtils.TestValue(itemFiltrado.CST) }
                                , { NullableUtils.TestValue(itemFiltrado.vBC) }
                                , { NullableUtils.TestValue(itemFiltrado.vICMS) }
                                , { NullableUtils.TestValue(itemFiltrado.CSOSN) }
                                , '{ itemFiltrado.Entrada }'
                                , { itemFiltrado.nNF }
                                , '{ itemFiltrado.dhEmi }'
                                , '{ itemFiltrado.dhSaiEnt }'
                            )
                            RETURNING ""ID"";";

                        id = cmd.ExecuteScalar();
                    }

                    conn.Close();
                }

                if (id != null && id.GetType() != typeof(DBNull))
                {
                    itemFiltrado.ID = (int)id;

                    return itemFiltrado;
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

        public ItemFiltrado Edit(ItemFiltrado itemFiltrado)
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
                                ""ProcessoID"" = { itemFiltrado.ProcessoID }
                                , ""ItemID"" = { itemFiltrado.ItemID }
                                , ""cProd"" = '{ itemFiltrado.cProd }'
                                , ""xProd"" = '{ itemFiltrado.xProd }'
                                , ""NCM"" = { itemFiltrado.NCM }
                                , ""CFOP"" = { NullableUtils.TestValue(itemFiltrado.CFOP) }
                                , ""uCom"" = { NullableUtils.TestValue(itemFiltrado.uCom) }
                                , ""qCom"" = { NullableUtils.TestValue(itemFiltrado.qCom) }
                                , ""vUnCom"" = { NullableUtils.TestValue(itemFiltrado.vUnCom) }
                                , ""orig"" = { NullableUtils.TestValue(itemFiltrado.orig) }
                                , ""CST"" = { NullableUtils.TestValue(itemFiltrado.CST) }
                                , ""vBC"" = { NullableUtils.TestValue(itemFiltrado.vBC) }
                                , ""vICMS"" = { NullableUtils.TestValue(itemFiltrado.vICMS) }
                                , ""CSOSN"" = { NullableUtils.TestValue(itemFiltrado.CSOSN) }
                                , ""Entrada"" = '{ itemFiltrado.Entrada }'
                                , ""nNF"" = { itemFiltrado.nNF }
                                , ""dhEmi"" = '{ itemFiltrado.dhEmi }'
                                , ""dhSaiEnt"" = '{ itemFiltrado.dhSaiEnt }'
                            WHERE ""ID"" = { itemFiltrado.ID };";

                        rows = cmd.ExecuteNonQuery();
                    }

                    conn.Close();
                }

                return rows > 0 ? itemFiltrado : null;
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
