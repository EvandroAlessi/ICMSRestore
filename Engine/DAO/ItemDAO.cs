using CrossCutting;
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
        static string connString = "Server=127.0.0.1;Port=5432;Database=icms_restore;User Id=postgres;Password=admin";
        const string quote = "\"";

        public async Task<List<Item>> GetAll()
        {
            try
            {
                var list = new List<Item>();

                using (var conn = new NpgsqlConnection(connString))
                {
                    await conn.OpenAsync();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $@"SELECT * FROm {quote}Itens{ quote};";

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var item = new Item
                                {
                                    nItem = Convert.ToInt32(reader["nItem"]),
                                    cProd = reader["cProd"]?.ToString(),
                                    cEAN = reader["cEAN"]?.ToString(),
                                    xProd = reader["xProd"]?.ToString(),
                                    NCM = reader["NCM"]?.ToString(),
                                    CFOP = reader["CFOP"]?.ToString(),
                                    uCom = reader["uCom"]?.ToString(),
                                    qCom = Convert.ToDouble(reader["qCom"]),
                                    vUnCom = Convert.ToDouble(reader["vUnCom"]),
                                    orig = Convert.ToInt32(reader["orig"]),
                                    CST = reader["CST"]?.ToString(),
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
                                    cNF = Convert.ToInt32(reader["cNF"])
                                };

                                list.Add(item);
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

        public bool Insert(Item item)
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
                                , {quote}cNF{quote}
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
                                , { item.cNF });";

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
    }
}
