using CrossCutting;
using Dominio;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DAO
{
    public class ItemFiltradoDAO : PaginationBuilder
    {
        static string connString = AppSettings.ConnectionString;
        const string quote = "\"";

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
                        cmd.CommandText = $@"SELECT * FROM {quote}ItensFiltrados{quote} 
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
                        cmd.CommandText = $@"SELECT * FROM {quote}ItensFiltrados{quote} 
                                            WHERE {quote}ProcessoID{quote} = { processID }
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
                        cmd.CommandText = $@"SELECT * FROM {quote}ItensFiltrados{quote} 
                                WHERE {quote}ID{quote} = { id };";

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
                        cmd.CommandText = $@"SELECT {quote}ID{quote} FROM {quote}ItensFiltrados{quote} 
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
                        cmd.CommandText = $@"INSERT INTO {quote}ItensFiltrados{quote} (
                                {quote}ProcessoID{quote}
                                , {quote}ItemID{quote}
                                , {quote}cProd{quote}
                                , {quote}xProd{quote}
                                , {quote}NCM{quote}
                                , {quote}CFOP{quote}
                                , {quote}uCom{quote}
                                , {quote}qCom{quote}
                                , {quote}vUnCom{quote}
                                , {quote}orig{quote}
                                , {quote}CST{quote}
                                , {quote}vBC{quote}
                                , {quote}vICMS{quote}
                                , {quote}CSOSN{quote}
                                , {quote}Entrada{quote}
                                , {quote}nNF{quote}
                                , {quote}dhEmi{quote}
                                , {quote}dhSaiEnt{quote}
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
                            RETURNING {quote}ID{quote};";

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
                        cmd.CommandText = $@"UPDATE {quote}ItensFiltrados{quote} SET
                                {quote}ProcessoID{quote} = { itemFiltrado.ProcessoID }
                                , {quote}ItemID{quote} = { itemFiltrado.ItemID }
                                , {quote}cProd{quote} = '{ itemFiltrado.cProd }'
                                , {quote}xProd{quote} = '{ itemFiltrado.xProd }'
                                , {quote}NCM{quote} = { itemFiltrado.NCM }
                                , {quote}CFOP{quote} = { NullableUtils.TestValue(itemFiltrado.CFOP) }
                                , {quote}uCom{quote} = { NullableUtils.TestValue(itemFiltrado.uCom) }
                                , {quote}qCom{quote} = { NullableUtils.TestValue(itemFiltrado.qCom) }
                                , {quote}vUnCom{quote} = { NullableUtils.TestValue(itemFiltrado.vUnCom) }
                                , {quote}orig{quote} = { NullableUtils.TestValue(itemFiltrado.orig) }
                                , {quote}CST{quote} = { NullableUtils.TestValue(itemFiltrado.CST) }
                                , {quote}vBC{quote} = { NullableUtils.TestValue(itemFiltrado.vBC) }
                                , {quote}vICMS{quote} = { NullableUtils.TestValue(itemFiltrado.vICMS) }
                                , {quote}CSOSN{quote} = { NullableUtils.TestValue(itemFiltrado.CSOSN) }
                                , {quote}Entrada{quote} = '{ itemFiltrado.Entrada }'
                                , {quote}nNF{quote} = { itemFiltrado.nNF }
                                , {quote}dhEmi{quote} = '{ itemFiltrado.dhEmi }'
                                , {quote}dhSaiEnt{quote} = '{ itemFiltrado.dhSaiEnt }'
                            WHERE {quote}ID{quote} = { itemFiltrado.ID };";

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
                        cmd.CommandText = $@"DELETE FROM {quote}ItensFiltrados{quote}
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
