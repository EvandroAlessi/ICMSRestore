using CrossCutting;
using CrossCutting.Models;
using Dominio;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAO
{
    public class ItemDAO
    {
        static string connString = AppSettings.ConnectionString;
        const string quote = "\"";

        public Item Make(NpgsqlDataReader reader)
        {
            return  new Item
                {
                    nItem = Convert.ToInt32(reader["nItem"]),
                    cProd = reader["cProd"]?.ToString(),
                    cEAN = reader["cEAN"]?.ToString(),
                    xProd = reader["xProd"]?.ToString(),
                    NCM = Convert.ToInt32(reader["NCM"]),
                    CFOP = Convert.ToInt32(reader["CFOP"]),
                    uCom = reader["uCom"]?.ToString(),
                    qCom = Convert.ToDouble(reader["qCom"]),
                    vUnCom = Convert.ToDouble(reader["vUnCom"]),
                    orig = Convert.ToInt32(reader["orig"]),
                    CST = Convert.ToInt32(reader["CST"]),
                    modBC = Convert.ToInt32(reader["modBC"]),
                    vBC = Convert.ToDouble(reader["vBC"]),
                    pICMS = Convert.ToDouble(reader["pICMS"]),
                    vICMS = Convert.ToDouble(reader["vICMS"]),
                    cEnq = Convert.ToInt32(reader["cEnq"]),
                    CST_IPI = reader["CST_IPI"]?.ToString(),
                    CST_PIS = reader["CST_PIS"]?.ToString(),
                    vBC_PIS = Convert.ToDouble(reader["vBC_PIS"]),
                    pPIS = Convert.ToDouble(reader["pPIS"]),
                    vPIS = Convert.ToDouble(reader["vPIS"]),
                    CST_COFINS = reader["CST_COFINS"]?.ToString(),
                    vBC_COFINS = Convert.ToDouble(reader["vBC_COFINS"]),
                    pCOFINS = Convert.ToDouble(reader["pCOFINS"]),
                    vCOFINS = Convert.ToDouble(reader["vCOFINS"]),
                    NFeID = Convert.ToInt32(reader["NFeID"]),
                    ID = Convert.ToInt32(reader["ID"])
                };
        }

        public async Task<Pagination> GetPagination(int skip = 0, int take = 30, Dictionary<string, string> filters = null)
        {
            try
            {
                Pagination pagination = new Pagination();

                using (var conn = new NpgsqlConnection(connString))
                {
                    await conn.OpenAsync();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $@"SELECT Count(*) FROM {quote}Empresas{quote} 
                                            { DynamicWhere.BuildFilters(filters) }
                                            LIMIT { take } 
                                            OFFSET { skip };";

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                pagination.Count = reader.GetInt32(0);
                                pagination.PageCount = (pagination.Count / take) + 1;
                                pagination.PageSize = take;

                                break;
                            }
                        }
                    }

                    await conn.CloseAsync();
                }

                return pagination;
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
                        cmd.CommandText = $@"SELECT * FROM {quote}Itens{quote}
                                            { DynamicWhere.BuildFilters(filters) }
                                            LIMIT { take } 
                                            OFFSET { skip };";

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(Make(reader));
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
                        cmd.CommandText = $@"SELECT * FROM {quote}Itens{quote}
                                    WHERE {quote}NFeID{quote} = { nfeID };";

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(Make(reader));
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
                        cmd.CommandText = $@"SELECT * FROM {quote}Itens{quote} 
                                WHERE {quote}ID{quote} = { id };";

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                item = Make(reader);
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
                        cmd.CommandText = $@"SELECT {quote}ID{quote} FROM {quote}Itens{quote} 
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

                        cmd.CommandText = $@"INSERT INTO {quote}Itens{ quote} (
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
                                , { item.NFeID })
                            RETURNING {quote}cProd{quote};";

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

                        cmd.CommandText = $@"INSERT INTO {quote}Itens{ quote} (
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
                                , { item.NFeID });";

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
                        cmd.CommandText = $@"INSERT INTO {quote}Itens{ quote} (
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
                                , {quote}cNF{quote}
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

        public bool Edit(Item item)
        {
            try
            {
                int rows = 0;

                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $@"UPDATE {quote}Items{quote} SET
                                {quote}nItem{quote} = '{ item.nItem }'
                                , {quote}cProd{quote} = '{ item.cProd }'
                                , {quote}cEAN{quote} = '{ item.cEAN }'
                                , {quote}xProd{quote} = '{ item.xProd }'
                                , {quote}NCM{quote} = '{ item.NCM }'
                                , {quote}CFOP{quote} = '{ item.CFOP }'
                                , {quote}uCom{quote} = '{ item.uCom }'
	                            , {quote}qCom{quote} = '{ item.qCom }'
                                , {quote}vUnCom{quote} = '{ item.vUnCom }'
                                , {quote}orig{quote} = '{ item.orig }'
                                , {quote}CST{quote} = '{ item.CST }'
                                , {quote}modBC{quote} = '{ item.modBC }'
                                , {quote}vBC{quote} = '{ item.vBC }'
                                , {quote}pICMS{quote} = '{ item.pICMS }'
                                , {quote}vICMS{quote} = '{ item.vICMS }'
	                            , {quote}cEnq{quote} = '{ item.cEnq }'
                                , {quote}CST_IPI{quote} = '{ item.CST_IPI }'
                                , {quote}CST_PIS{quote} = '{ item.CST_PIS }'
                                , {quote}vBC_PIS{quote} = '{ item.vBC_PIS }'
                                , {quote}pPIS{quote} = '{ item.pPIS }'
                                , {quote}vPIS{quote} = '{ item.vPIS }'
                                , {quote}CST_COFINS{quote} = '{ item.CST_COFINS }'
	                            , {quote}vBC_COFINS{quote} = '{ item.vBC_COFINS }'
                                , {quote}pCOFINS{quote} = '{ item.pCOFINS }'
                                , {quote}vCOFINS{quote} = '{ item.vCOFINS }'
                                , {quote}NFeID{quote} = '{ item.NFeID }''
                            WHERE {quote}ID{quote} = { item.ID };";

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
                        cmd.CommandText = $@"DELETE FROM {quote}Items{quote}
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
