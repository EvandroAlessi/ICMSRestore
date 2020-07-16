using CrossCutting;
using CrossCutting.Models;
using CrossCutting.ResultModels;
using Dominio;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DAO
{
    public class ItemFiltradoDAO : CommonDAO
    {
        private static readonly int[] entDevol = new int[4] { 1411, 1415, 2411, 2415 };
        private static readonly int[] saiDevol = new int[2] { 5411, 5415 };

        public ItemFiltradoDAO() => table = "\"ItensFiltrados\"";

        public ItemFiltrado BuildObject(NpgsqlDataReader reader)
        {
            return new ItemFiltrado
            {
                ID = Convert.ToInt32(reader["ID"]),
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
                cNF = Convert.ToInt32(reader["cNF"]),
                pICMS = reader.GetFieldValue<double?>("pICMS"),
                vProd = reader.GetFieldValue<double>("vProd"),
                Chave = reader["Chave"]?.ToString(),
                CNPJ = reader["CNPJ"]?.ToString(),
                CNPJ_DEST = reader["CNPJ_DEST"]?.ToString(),
                IE = reader["IE"]?.ToString(),
                nItem = reader.GetFieldValue<int>("nItem"),
                UF = reader["UF"]?.ToString(),
                UF_DEST = reader["UF_DEST"]?.ToString(),
                xNome = reader["xNome"]?.ToString(),
                cEAN = reader["cEAN"]?.ToString(),
            };
        }

        public async Task<bool> CalcItems(string path, int processID, int? ncm = null)
        {
            try
            {
                var list = new List<ItemFiltrado>();

                using (var conn = new NpgsqlConnection(connString))
                {
                    await conn.OpenAsync();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $@"SELECT *
                                FROM { table }
                                WHERE ""ProcessoID"" = { processID } 
                                    { (ncm.HasValue ? $@"AND ""NCM"" = '{ ncm }'" : "") }
                                ORDER BY 1 DESC;";
                        
                                   // ""Entrada"" = { entrada } AND 

                        using var reader = cmd.ExecuteReader();
                        {
                            while (reader.Read())
                            {
                                list.Add(BuildObject(reader));
                            }
                        }
                    }

                    await conn.CloseAsync();
                }

                return await calc(path, list);
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

        public async Task<bool> calc(string path, List<ItemFiltrado> items)
        {
            try
            {
                var files = new List<ArquivoFinal>();

                //var error = items.Any(x => items.Any(y => y.CNPJ != x.CNPJ));

                //if (error)
                //{
                //    throw new Exception("Entre as notas foi encontrado ao menos 2 CNPJs distintos!");
                //}

                foreach (var byYear in items.GroupBy(x => x.dhEmi.Year))
                {
                    foreach (var byMonth in byYear.GroupBy(x => x.dhEmi.Month))
                    {
                        foreach (var byNCM in byMonth.GroupBy(x => x.NCM))
                        {
                            var nfesEntrada = new List<NFeEntrada>();
                            var nfesSaida = new List<NFeSaida>();
                            var nfesEntradaDevol = new List<NFeEntradaDevol>();
                            var nfesSaidaDevol = new List<NFeSaidaDevol>();

                            foreach (var item in byNCM)
                            {
                                var CST_CSOSN = (item.CST != null 
                                        ? item.CST 
                                        : item.CSOSN
                                    )?.ToString("D" + 2);

                                if (item.Entrada)
                                {
                                    if (!entDevol.Contains(item.CFOP))
                                    {
                                        nfesEntrada.Add(new NFeEntrada
                                        {
                                            DT_DOC = item.dhEmi.ToString("ddMMyyyy"),
                                            COD_RESP_RET = 0,
                                            CST_CSOSN = CST_CSOSN,
                                            CHAVE = item.Chave,
                                            N_NF = item.nNF.ToString(),
                                            CNPJ_EMIT = item.CNPJ,
                                            UF_EMIT = item.UF,
                                            CNPJ_DEST = item.CNPJ_DEST,
                                            UF_DEST = item.UF_DEST,
                                            CFOP = item.CFOP.ToString("D" + 4),
                                            N_ITEM = item.nItem.ToString(),
                                            UNID_ITEM = item.uCom,
                                            QTD_ENTRADA = item.qCom.Value,
                                            VL_UNIT_ITEM = item.vUnCom.Value,
                                            VL_BC_ICMS_ST = 0.0,
                                            VL_ICMS_SUPORT_ENTR = 0.0,
                                        });
                                    }
                                    else
                                    {
                                        nfesEntradaDevol.Add(new NFeEntradaDevol
                                        {
                                            DT_DOC = item.dhEmi.ToString("ddMMyyyy"),
                                            CST_CSOSN = CST_CSOSN,
                                            CHAVE = item.Chave,
                                            N_NF = item.nNF.ToString(),
                                            CNPJ_EMIT = item.CNPJ,
                                            UF_EMIT = item.UF,
                                            CNPJ_DEST = item.CNPJ_DEST,
                                            UF_DEST = item.UF_DEST,
                                            CFOP = item.CFOP.ToString("D" + 4),
                                            N_ITEM = item.nItem.ToString(),
                                            UNID_ITEM = item.uCom,
                                            VL_UNIT_ITEM = item.vUnCom.Value,
                                            VL_BC_ICMS_ST = 0.0,
                                            VL_ICMS_SUPORT_ENTR = 0.0,
                                            CHAVE_REF = "",
                                            N_ITEM_REF = "",
                                            QTD_DEVOLVIDA = 0.0
                                        });
                                    }
                                }
                                else
                                {
                                    if (!saiDevol.Contains(item.CFOP))
                                    {
                                        nfesSaida.Add(new NFeSaida
                                        {
                                            DT_DOC = item.dhEmi.ToString("ddMMyyyy"),
                                            CST_CSOSN = CST_CSOSN,
                                            CHAVE = item.Chave,
                                            N_NF = item.nNF.ToString(),
                                            CNPJ_EMIT = item.CNPJ,
                                            UF_EMIT = item.UF,
                                            CNPJ_DEST = item.CNPJ_DEST,
                                            UF_DEST = item.UF_DEST,
                                            CFOP = item.CFOP.ToString("D" + 4),
                                            N_ITEM = item.nItem.ToString(),
                                            UNID_ITEM = item.uCom,
                                            QTD_SAIDA = item.qCom.Value,
                                            VL_UNIT_ITEM = item.vUnCom.Value,
                                            VL_ICMS_EFET = (item.vUnCom * 18 / 100).Value
                                        });
                                    }
                                    else
                                    {
                                        nfesSaidaDevol.Add(new NFeSaidaDevol
                                        {
                                            DT_DOC = item.dhEmi.ToString("ddMMyyyy"),
                                            CST_CSOSN = CST_CSOSN,
                                            CHAVE = item.Chave,
                                            N_NF = item.nNF.ToString(),
                                            CNPJ_EMIT = item.CNPJ,
                                            UF_EMIT = item.UF,
                                            CNPJ_DEST = item.CNPJ_DEST,
                                            UF_DEST = item.UF_DEST,
                                            CFOP = item.CFOP.ToString("D" + 4),
                                            N_ITEM = item.nItem.ToString(),
                                            UNID_ITEM = item.uCom,
                                            VL_UNIT_ITEM = item.vUnCom.Value,
                                            CHAVE_REF = "",
                                            N_ITEM_REF = "",
                                            QTD_DEVOLVIDA = 0.0,
                                            VL_ICMS_EFETIVO = 0.0
                                        });
                                    }
                                }
                            }

                            double menorValUnEntrada = nfesEntrada.Count > 0
                                    ? nfesEntrada.Min(x => x.QTD_ENTRADA)
                                    : 0.0; 

                            double countSaidas = nfesSaida.Count > 0 
                                    ? nfesSaida.Sum(x => x.QTD_SAIDA)
                                    : 0.0;

                            double countEntradas = nfesEntrada.Count > 0
                                    ? nfesEntrada.Sum(x => x.QTD_ENTRADA)
                                    : 0.0;

                            var totalEntradas = new TotalEntrada
                            {
                                QTD_TOT_ENTRADA = countEntradas,
                                MENOR_VL_UNIT_ITEM = menorValUnEntrada,
                                //VL_BC_ICMSST_UNIT_MED = 0.0,
                                //VL_TOT_ICMS_SUPORT_ENTR = 0.0,
                                //VL_UNIT_MED_ICMS_SUPORT_ENTR = 0.0,
                            };

                            //Menos as devoluções
                            totalEntradas.VL_BC_ICMSST_UNIT_MED = nfesEntrada.Sum(x => x.VL_BC_ICMS_ST) /* - Sum(DEVOLUCOES) */ / totalEntradas.QTD_TOT_ENTRADA;

                            //Menos as devoluções
                            totalEntradas.VL_TOT_ICMS_SUPORT_ENTR = nfesEntrada.Sum(x => x.VL_ICMS_SUPORT_ENTR) /* - Sum(DEVOLUCOES) */;

                            totalEntradas.VL_UNIT_MED_ICMS_SUPORT_ENTR = totalEntradas.VL_TOT_ICMS_SUPORT_ENTR / totalEntradas.QTD_TOT_ENTRADA;


                            var totalSaidas = new TotalSaida
                            {
                                QTD_TOT_SAIDA = countSaidas,
                                //VL_TOT_ICMS_EFETIVO = 0.0,
                                APUR_ICMSST_RECUPERAR_RESSARCIR = 0.0,
                                VL_CONFRONTO_ICMS_ENTRADA = 0.0,
                                RESULT_RECUPERAR_RESSARCIR = 0.0,
                                RESULT_COMPLEMENTAR = 0.0,
                                APUR_ICMSST_COMPLEMENTAR = 0.0,
                                APUR_FECOP_RESSARCIR = 0.0,
                                APUR_FECOP_COMPLEMENTAR = 0.0,
                            };

                            //Menos as devoluções
                            totalSaidas.VL_TOT_ICMS_EFETIVO = nfesSaida.Sum(x => x.VL_ICMS_EFET) /* - Sum(DEVOLUCOES) */;

                            files.Add(new ArquivoFinal
                            {
                                Contribuinte = new Contribuinte
                                {
                                    CNPJ = byNCM.First().CNPJ,
                                    CNPJ_CD = "",
                                    CD_FIN = 0,
                                    IE = byNCM.First().IE,
                                    IE_CD = "",
                                    MES_ANO = byNCM.First().dhEmi.ToString("MMyyyy"),
                                    NOME = byNCM.First().xNome,
                                    N_REG_ESPECIAL = "",
                                },
                                Produto = new Produto
                                {
                                    IND_FECOP = 0,
                                    COD_ITEM = byNCM.First().cProd,
                                    COD_BARRAS = byNCM.First().cEAN,
                                    COD_ANP = "",
                                    NCM = byNCM.First().NCM.ToString("D" + 8),
                                    CEST = "",
                                    DESCR_ITEM = byNCM.First().xProd,
                                    UNID_ITEM = byNCM.First().uCom,
                                    ALIQ_ICMS_ITEM = 18.00,
                                    ALIQ_FECOP = 0.0,
                                    QTD_TOT_ENTRADA = countEntradas,
                                    QTD_TOT_SAIDA = countSaidas,
                                },
                                TotalEntrada = totalEntradas,
                                NFesEntrada = nfesEntrada,
                                NFesEntradaDevol = nfesEntradaDevol,
                                TotalSaida = totalSaidas,
                                NFesSaida = nfesSaida,
                                NFesSaidaDevol = nfesSaidaDevol,
                                FimInfoBase = new FimInfoBase
                                {
                                    QTD_LIN = (3 + nfesEntrada.Count() + 1 + nfesSaida.Count() + 1)
                                },
                                Total = new Total
                                {
                                    REG1200_ICMSST_RECUPERAR_RESSARCIR = 0.0,
                                    REG1200_ICMSST_COMPLEMENTAR = 0.0,
                                    REG1300_ICMSST_RECUPERAR_RESSARCIR = 0.0,
                                    REG1400_ICMSST_RECUPERAR_RESSARCIR = 0.0,
                                    REG1500_ICMSST_RECUPERAR_RESSARCIR = 0.0,
                                    REG9000_FECOP_RESSARCIR = 0.0,
                                    REG9000_FECOP_COMPLEMENTAR = 0.0,
                                },
                                FimArquivo = new FimArquivo
                                {
                                    QTD_LIN = (3 + nfesEntrada.Count() + 1 + nfesSaida.Count() + 3)
                                }
                            });
                        }
                    }
                }


                foreach (var arquivo in files)
                {
                    lock (locker)
                    {
                        PathControl.Create(path);

                        var path2 = Path.Combine(path, arquivo.Produto.NCM);

                        PathControl.Create(path2);

                        path2 = Path.Combine(path2, arquivo.Produto.DESCR_ITEM.Replace("/", "") + ".txt");

                        using (FileStream file = new FileStream(path2, FileMode.Create, FileAccess.Write, FileShare.None))
                        {
                            using (StreamWriter stream = new StreamWriter(file))
                            {
                                lock (stream)
                                {
                                    //Contribuinte
                                    stream.WriteLine(arquivo.Contribuinte.REG + '|' +
                                                     arquivo.Contribuinte.COD_VERSAO + '|' +
                                                     arquivo.Contribuinte.MES_ANO + '|' +
                                                     arquivo.Contribuinte.CNPJ + '|' +
                                                     arquivo.Contribuinte.IE + '|' +
                                                     arquivo.Contribuinte.NOME + '|' +
                                                     arquivo.Contribuinte.CD_FIN + '|' +
                                                     arquivo.Contribuinte.N_REG_ESPECIAL + '|' +
                                                     arquivo.Contribuinte.CNPJ_CD + '|' +
                                                     arquivo.Contribuinte.IE_CD);

                                    //Produto
                                    stream.WriteLine(arquivo.Produto.REG + '|' +
                                                     arquivo.Produto.IND_FECOP + '|' +
                                                     arquivo.Produto.COD_ITEM + '|' +
                                                     arquivo.Produto.COD_BARRAS + '|' +
                                                     arquivo.Produto.COD_ANP + '|' +
                                                     arquivo.Produto.NCM + '|' +
                                                     arquivo.Produto.CEST + '|' +
                                                     arquivo.Produto.DESCR_ITEM + '|' +
                                                     arquivo.Produto.UNID_ITEM + '|' +
                                                     arquivo.Produto.ALIQ_ICMS_ITEM + '|' +
                                                     arquivo.Produto.ALIQ_FECOP + '|' +
                                                     arquivo.Produto.QTD_TOT_ENTRADA + '|' +
                                                     arquivo.Produto.QTD_TOT_SAIDA);

                                    //TotalEntrada
                                    stream.WriteLine(arquivo.TotalEntrada.REG + '|' +
                                                     arquivo.TotalEntrada.QTD_TOT_ENTRADA + '|' +
                                                     arquivo.TotalEntrada.MENOR_VL_UNIT_ITEM + '|' +
                                                     arquivo.TotalEntrada.VL_BC_ICMSST_UNIT_MED + '|' +
                                                     arquivo.TotalEntrada.VL_TOT_ICMS_SUPORT_ENTR + '|' +
                                                     arquivo.TotalEntrada.VL_UNIT_MED_ICMS_SUPORT_ENTR);

                                    //NFeEntrada
                                    foreach (var nfe in arquivo.NFesEntrada)
                                    {
                                        stream.WriteLine(nfe.REG + '|' +
                                                         nfe.DT_DOC + '|' +
                                                         nfe.COD_RESP_RET + '|' +
                                                         nfe.CST_CSOSN + '|' +
                                                         nfe.CHAVE + '|' +
                                                         nfe.N_NF + '|' +
                                                         nfe.CNPJ_EMIT + '|' +
                                                         nfe.UF_EMIT + '|' +
                                                         nfe.CNPJ_DEST + '|' +
                                                         nfe.UF_DEST + '|' +
                                                         nfe.CFOP + '|' +
                                                         nfe.N_ITEM + '|' +
                                                         nfe.UNID_ITEM + '|' +
                                                         nfe.QTD_ENTRADA + '|' +
                                                         nfe.VL_UNIT_ITEM + '|' +
                                                         nfe.VL_BC_ICMS_ST + '|' +
                                                         nfe.VL_ICMS_SUPORT_ENTR);
                                    }

                                    //TotalSaida
                                    stream.WriteLine(arquivo.TotalSaida.REG + '|' +
                                                     arquivo.TotalSaida.QTD_TOT_SAIDA + '|' +
                                                     arquivo.TotalSaida.VL_TOT_ICMS_EFETIVO + '|' +
                                                     arquivo.TotalSaida.VL_CONFRONTO_ICMS_ENTRADA + '|' +
                                                     arquivo.TotalSaida.RESULT_RECUPERAR_RESSARCIR + '|' +
                                                     arquivo.TotalSaida.RESULT_COMPLEMENTAR + '|' +
                                                     arquivo.TotalSaida.APUR_ICMSST_RECUPERAR_RESSARCIR + '|' +
                                                     arquivo.TotalSaida.APUR_ICMSST_COMPLEMENTAR + '|' +
                                                     arquivo.TotalSaida.APUR_FECOP_RESSARCIR + '|' +
                                                     arquivo.TotalSaida.APUR_FECOP_COMPLEMENTAR);

                                    //NFeSaida
                                    foreach (var nfe in arquivo.NFesSaida)
                                    {
                                        stream.WriteLine(nfe.REG + '|' +
                                                         nfe.DT_DOC + '|' +
                                                         nfe.CST_CSOSN + '|' +
                                                         nfe.CHAVE + '|' +
                                                         nfe.N_NF + '|' +
                                                         nfe.CNPJ_EMIT + '|' +
                                                         nfe.UF_EMIT + '|' +
                                                         nfe.CNPJ_DEST + '|' +
                                                         nfe.UF_DEST + '|' +
                                                         nfe.CFOP + '|' +
                                                         nfe.N_ITEM + '|' +
                                                         nfe.UNID_ITEM + '|' +
                                                         nfe.QTD_SAIDA + '|' +
                                                         nfe.VL_UNIT_ITEM + '|' +
                                                         nfe.VL_ICMS_EFET);
                                    }

                                    //FimInfoBase
                                    stream.WriteLine(arquivo.FimInfoBase.REG + '|' +
                                                     arquivo.FimInfoBase.QTD_LIN);

                                    //Total
                                    stream.WriteLine(arquivo.Total.REG + '|' +
                                                     arquivo.Total.REG1200_ICMSST_RECUPERAR_RESSARCIR + '|' +
                                                     arquivo.Total.REG1200_ICMSST_COMPLEMENTAR + '|' +
                                                     arquivo.Total.REG1300_ICMSST_RECUPERAR_RESSARCIR + '|' +
                                                     arquivo.Total.REG1400_ICMSST_RECUPERAR_RESSARCIR + '|' +
                                                     arquivo.Total.REG1500_ICMSST_RECUPERAR_RESSARCIR + '|' +
                                                     arquivo.Total.REG9000_FECOP_RESSARCIR + '|' +
                                                     arquivo.Total.REG9000_FECOP_COMPLEMENTAR);

                                    //FimArquivo
                                    stream.WriteLine(arquivo.FimArquivo.REG + '|' +
                                                     arquivo.FimArquivo.QTD_LIN);

                                    stream.Close();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return true;
        }

        private static object locker = new object();

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



        public async Task<List<ProductMedia>> calcSumarry(List<ItemFiltrado> items)
        {
            try
            {
                var products = new List<ProductMedia>();

                foreach (var byYear in items.GroupBy(x => x.dhEmi.Year))
                {
                    foreach (var byMonth in byYear.GroupBy(x => x.dhEmi.Month))
                    {
                        foreach (var byNCM in byMonth.GroupBy(x => x.NCM))
                        {
                            var product = new ProductMedia();

                            product.Name = byNCM.FirstOrDefault()?.xProd;
                            product.NCM = byNCM.FirstOrDefault()?.NCM;
                            product.MonthYear = byNCM.FirstOrDefault()?.dhEmi.ToString("MM/yyyy");
                            product.LowerValue = byNCM.Any(x => !x.Entrada) 
                                ? byNCM.Where(x => !x.Entrada)?.Min(x => x.vUnCom.Value)
                                : null;
                            product.HighestValue = byNCM.Any(x => !x.Entrada) 
                                ? byNCM.Where(x => !x.Entrada)?.Max(x => x.vUnCom.Value)
                                : null;
                            product.Media = byNCM.Any(x => !x.Entrada) 
                                ? byNCM.Where(x => !x.Entrada)?.Average(x => x.vUnCom.Value)
                                : null;
                            product.TotalResults = byNCM.Select(x => x).Count();
                            product.TotalValue = byNCM.Any(x => !x.Entrada) 
                                ? byNCM.Where(x => !x.Entrada)?.Sum(x => x.vUnCom.Value) 
                                : null;
                            product.MediaEntry = (byNCM.Any(x => x.Entrada) 
                                ? byNCM.Where(x => x.Entrada)?.Average(x => x.vUnCom.Value) 
                                : null);

                            products.Add(product);
                        }
                    }
                }

                return products.OrderByDescending(x => x.MediaEntry).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<ProductMedia>> GetSumarry(Processo process)
        {
            try
            {
                var list = new List<ItemFiltrado>();

                using (var conn = new NpgsqlConnection(connString))
                {
                    await conn.OpenAsync();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $@"SELECT *
                                FROM { table }
                                WHERE 
                                    ""dhEmi"" BETWEEN '{ process.InicioPeriodo }' AND '{ process.FimPeriodo }'
                                ORDER BY 1 DESC;";

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

                return await calcSumarry(list);
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
                                , ""vProd""
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
                                , { NullableUtils.TestValue(itemFiltrado.vProd) }
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
                                , ""vProd"" = { NullableUtils.TestValue(itemFiltrado.vProd) }
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
