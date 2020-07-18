using CrossCutting.ResultModels;
using Dominio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CrossCutting
{
    /// <summary>
    /// Responsavel pela geração do documento final
    /// </summary>
    public class DocumentBuilder
    {
        /// <summary>
        /// CFOP de devolução em notas de Entrada
        /// </summary>
        private static readonly int[] entDevol = new int[4] { 1411, 1415, 2411, 2415 };

        /// <summary>
        /// CFOP de devolução em notas de Saída
        /// </summary>
        private static readonly int[] saiDevol = new int[2] { 5411, 5415 };

        private static readonly object locker = new object();

        /// <summary>
        /// Salva N documentos gerados na pasta de seu referente NCM
        /// dentro de uma pasta temporária
        /// </summary>
        /// <param name="path"></param>
        /// <param name="documents"></param>
        public static void WriteDocument(string path, List<ArquivoFinal> documents)
        {
            try
            {
                foreach (var document in documents)
                {
                    WriteDocument(path, document);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        ///Salva o documento gerado na pasta de seu referente NCM
        /// dentro de uma pasta temporária
        /// </summary>
        /// <param name="path"></param>
        /// <param name="document"></param>
        public static void WriteDocument(string path, ArquivoFinal document)
        {
            try
            {
                lock (locker)
                {
                    PathControl.Create(path);

                    var ncmFolder = Path.Combine(path, document.Produto.NCM);

                    PathControl.Create(ncmFolder);

                    var documentName = $"{ document.Produto.DESCR_ITEM.Replace("/", "") }-{ document.Contribuinte.MES_ANO }.txt";

                    var documentPath = Path.Combine(ncmFolder, documentName);

                    using (FileStream file = new FileStream(documentPath, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        using (StreamWriter stream = new StreamWriter(file))
                        {
                            lock (stream)
                            {
                                //Contribuinte
                                stream.WriteLine(document.Contribuinte.REG + '|' +
                                                 document.Contribuinte.COD_VERSAO + '|' +
                                                 document.Contribuinte.MES_ANO + '|' +
                                                 document.Contribuinte.CNPJ + '|' +
                                                 document.Contribuinte.IE + '|' +
                                                 document.Contribuinte.NOME + '|' +
                                                 document.Contribuinte.CD_FIN + '|' +
                                                 document.Contribuinte.N_REG_ESPECIAL + '|' +
                                                 document.Contribuinte.CNPJ_CD + '|' +
                                                 document.Contribuinte.IE_CD);

                                //Produto
                                stream.WriteLine(document.Produto.REG + '|' +
                                                 document.Produto.IND_FECOP + '|' +
                                                 document.Produto.COD_ITEM + '|' +
                                                 document.Produto.COD_BARRAS + '|' +
                                                 document.Produto.COD_ANP + '|' +
                                                 document.Produto.NCM + '|' +
                                                 document.Produto.CEST + '|' +
                                                 document.Produto.DESCR_ITEM + '|' +
                                                 document.Produto.UNID_ITEM + '|' +
                                                 document.Produto.ALIQ_ICMS_ITEM + '|' +
                                                 document.Produto.ALIQ_FECOP + '|' +
                                                 document.Produto.QTD_TOT_ENTRADA + '|' +
                                                 document.Produto.QTD_TOT_SAIDA);

                                //TotalEntrada
                                stream.WriteLine(document.TotalEntrada.REG + '|' +
                                                 document.TotalEntrada.QTD_TOT_ENTRADA + '|' +
                                                 document.TotalEntrada.MENOR_VL_UNIT_ITEM + '|' +
                                                 document.TotalEntrada.VL_BC_ICMSST_UNIT_MED + '|' +
                                                 document.TotalEntrada.VL_TOT_ICMS_SUPORT_ENTR + '|' +
                                                 document.TotalEntrada.VL_UNIT_MED_ICMS_SUPORT_ENTR);

                                //NFeEntrada
                                foreach (var nfe in document.NFesEntrada)
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

                                //NFe Entrada Devoluções
                                foreach (var nfe in document.NFesEntradaDevol)
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
                                                     nfe.QTD_DEVOLVIDA + '|' +
                                                     nfe.VL_UNIT_ITEM + '|' +
                                                     nfe.VL_BC_ICMS_ST + '|' +
                                                     nfe.VL_ICMS_SUPORT_ENTR + '|' +
                                                     nfe.CHAVE_REF + '|' +
                                                     nfe.N_ITEM_REF + '|');
                                }

                                //TotalSaida
                                stream.WriteLine(document.TotalSaida.REG + '|' +
                                                 document.TotalSaida.QTD_TOT_SAIDA + '|' +
                                                 document.TotalSaida.VL_TOT_ICMS_EFETIVO + '|' +
                                                 document.TotalSaida.VL_CONFRONTO_ICMS_ENTRADA + '|' +
                                                 document.TotalSaida.RESULT_RECUPERAR_RESSARCIR + '|' +
                                                 document.TotalSaida.RESULT_COMPLEMENTAR + '|' +
                                                 document.TotalSaida.APUR_ICMSST_RECUPERAR_RESSARCIR + '|' +
                                                 document.TotalSaida.APUR_ICMSST_COMPLEMENTAR + '|' +
                                                 document.TotalSaida.APUR_FECOP_RESSARCIR + '|' +
                                                 document.TotalSaida.APUR_FECOP_COMPLEMENTAR);

                                //NFeSaida
                                foreach (var nfe in document.NFesSaida)
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

                                //NFe Saída Devoluções
                                foreach (var nfe in document.NFesSaidaDevol)
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
                                                     nfe.QTD_DEVOLVIDA + '|' +
                                                     nfe.VL_UNIT_ITEM + '|' +
                                                     nfe.VL_ICMS_EFETIVO + '|' +
                                                     nfe.CHAVE_REF + '|' +
                                                     nfe.N_ITEM_REF);
                                }

                                //FimInfoBase
                                stream.WriteLine(document.FimInfoBase.REG + '|' +
                                                 document.FimInfoBase.QTD_LIN);

                                //Total
                                stream.WriteLine(document.Total.REG + '|' +
                                                 document.Total.REG1200_ICMSST_RECUPERAR_RESSARCIR + '|' +
                                                 document.Total.REG1200_ICMSST_COMPLEMENTAR + '|' +
                                                 document.Total.REG1300_ICMSST_RECUPERAR_RESSARCIR + '|' +
                                                 document.Total.REG1400_ICMSST_RECUPERAR_RESSARCIR + '|' +
                                                 document.Total.REG1500_ICMSST_RECUPERAR_RESSARCIR + '|' +
                                                 document.Total.REG9000_FECOP_RESSARCIR + '|' +
                                                 document.Total.REG9000_FECOP_COMPLEMENTAR);

                                //Fimdocument
                                stream.WriteLine(document.FimArquivo.REG + '|' +
                                                 document.FimArquivo.QTD_LIN);

                                stream.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Povoa a classe que sera base para a geração do documento final
        /// Realiza todos os grup by usando LINQ
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static List<ArquivoFinal> BuildDocument(List<ItemFiltrado> items)
        {
            try
            {
                var files = new List<ArquivoFinal>();

                //VerifyCNPJ(items);

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
                                            COD_RESP_RET = 0, //VERIFICAR
                                            CST_CSOSN = CST_CSOSN,
                                            CHAVE = item.Chave, //VERIFICAR
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
                                            VL_BC_ICMS_ST = 0.0, //VERIFICAR
                                            VL_ICMS_SUPORT_ENTR = 0.0, //VERIFICAR
                                        });
                                    }
                                    else
                                    {
                                        nfesEntradaDevol.Add(new NFeEntradaDevol
                                        {
                                            DT_DOC = item.dhEmi.ToString("ddMMyyyy"),
                                            CST_CSOSN = CST_CSOSN,
                                            CHAVE = item.Chave, //VERIFICAR
                                            N_NF = item.nNF.ToString(),
                                            CNPJ_EMIT = item.CNPJ,
                                            UF_EMIT = item.UF,
                                            CNPJ_DEST = item.CNPJ_DEST,
                                            UF_DEST = item.UF_DEST,
                                            CFOP = item.CFOP.ToString("D" + 4),
                                            N_ITEM = item.nItem.ToString(),
                                            UNID_ITEM = item.uCom,
                                            QTD_DEVOLVIDA = item.qCom.Value,
                                            VL_UNIT_ITEM = item.vUnCom.Value,
                                            VL_BC_ICMS_ST = 0.0, //VERIFICAR
                                            VL_ICMS_SUPORT_ENTR = 0.0, //VERIFICAR
                                            CHAVE_REF = "", //VERIFICAR
                                            N_ITEM_REF = "", //VERIFICAR
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
                                            CHAVE = item.Chave, //VERIFICAR
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
                                            VL_ICMS_EFET = (item.vUnCom * 18 / 100).Value //VERIFICAR
                                        });
                                    }
                                    else
                                    {
                                        nfesSaidaDevol.Add(new NFeSaidaDevol
                                        {
                                            DT_DOC = item.dhEmi.ToString("ddMMyyyy"),
                                            CST_CSOSN = CST_CSOSN,
                                            CHAVE = item.Chave, //VERIFICAR
                                            N_NF = item.nNF.ToString(),
                                            CNPJ_EMIT = item.CNPJ,
                                            UF_EMIT = item.UF,
                                            CNPJ_DEST = item.CNPJ_DEST,
                                            UF_DEST = item.UF_DEST,
                                            CFOP = item.CFOP.ToString("D" + 4),
                                            N_ITEM = item.nItem.ToString(),
                                            UNID_ITEM = item.uCom,
                                            QTD_DEVOLVIDA = 0.0, //VERIFICAR
                                            VL_UNIT_ITEM = item.vUnCom.Value,
                                            VL_ICMS_EFETIVO = 0.0, //VERIFICAR
                                            CHAVE_REF = "", //VERIFICAR
                                            N_ITEM_REF = "", //VERIFICAR
                                        });
                                    }
                                }
                            }

                            double menorValUnEntrada = nfesEntrada.Count > 0
                                    ? nfesEntrada.Min(x => x.QTD_ENTRADA)
                                    : 0.0; //VERIFICAR

                            double countSaidas = nfesSaida.Count > 0
                                    ? nfesSaida.Sum(x => x.QTD_SAIDA)
                                    : 0.0; //VERIFICAR

                            double countEntradas = nfesEntrada.Count > 0
                                    ? nfesEntrada.Sum(x => x.QTD_ENTRADA)
                                    : 0.0; //VERIFICAR

                            var totalEntradas = new TotalEntrada
                            {
                                QTD_TOT_ENTRADA = countEntradas,
                                MENOR_VL_UNIT_ITEM = menorValUnEntrada,
                                //VL_BC_ICMSST_UNIT_MED = 0.0,
                                //VL_TOT_ICMS_SUPORT_ENTR = 0.0,
                                //VL_UNIT_MED_ICMS_SUPORT_ENTR = 0.0,
                            };

                            //Menos as devoluções
                            totalEntradas.VL_BC_ICMSST_UNIT_MED = nfesEntrada.Sum(x => x.VL_BC_ICMS_ST) /* - Sum(DEVOLUCOES) */ / totalEntradas.QTD_TOT_ENTRADA; //VERIFICAR

                            //Menos as devoluções
                            totalEntradas.VL_TOT_ICMS_SUPORT_ENTR = nfesEntrada.Sum(x => x.VL_ICMS_SUPORT_ENTR) /* - Sum(DEVOLUCOES) */; //VERIFICAR

                            totalEntradas.VL_UNIT_MED_ICMS_SUPORT_ENTR = totalEntradas.VL_TOT_ICMS_SUPORT_ENTR / totalEntradas.QTD_TOT_ENTRADA;


                            var totalSaidas = new TotalSaida
                            {
                                QTD_TOT_SAIDA = countSaidas,
                                //VL_TOT_ICMS_EFETIVO = 0.0,
                                APUR_ICMSST_RECUPERAR_RESSARCIR = 0.0, //VERIFICAR
                                VL_CONFRONTO_ICMS_ENTRADA = 0.0, //VERIFICAR
                                RESULT_RECUPERAR_RESSARCIR = 0.0, //VERIFICAR
                                RESULT_COMPLEMENTAR = 0.0, //VERIFICAR
                                APUR_ICMSST_COMPLEMENTAR = 0.0, //VERIFICAR
                                APUR_FECOP_RESSARCIR = 0.0, //VERIFICAR
                                APUR_FECOP_COMPLEMENTAR = 0.0, //VERIFICAR
                            };

                            //Menos as devoluções
                            totalSaidas.VL_TOT_ICMS_EFETIVO = nfesSaida.Sum(x => x.VL_ICMS_EFET) /* - Sum(DEVOLUCOES) */; //VERIFICAR

                            files.Add(new ArquivoFinal
                            {
                                Contribuinte = new Contribuinte
                                {
                                    CNPJ = byNCM.First().CNPJ,
                                    CNPJ_CD = "", //VERIFICAR
                                    CD_FIN = 0, //VERIFICAR
                                    IE = byNCM.First().IE,
                                    IE_CD = "", //VERIFICAR
                                    MES_ANO = byNCM.First().dhEmi.ToString("MMyyyy"),
                                    NOME = byNCM.First().xNome,
                                    N_REG_ESPECIAL = "", //VERIFICAR
                                },
                                Produto = new Produto
                                {
                                    IND_FECOP = 0, //VERIFICAR
                                    COD_ITEM = byNCM.First().cProd,
                                    COD_BARRAS = byNCM.First().cEAN,
                                    COD_ANP = "", //VERIFICAR
                                    NCM = byNCM.First().NCM.ToString("D" + 8),
                                    CEST = "", //VERIFICAR
                                    DESCR_ITEM = byNCM.First().xProd,
                                    UNID_ITEM = byNCM.First().uCom,
                                    ALIQ_ICMS_ITEM = 18.00, //VERIFICAR
                                    ALIQ_FECOP = 0.0, //VERIFICAR
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
                                    QTD_LIN = (3 + nfesEntrada.Count() + 1 + nfesSaida.Count() + 1) //VERIFICAR
                                },
                                Total = new Total
                                {
                                    REG1200_ICMSST_RECUPERAR_RESSARCIR = 0.0, //VERIFICAR
                                    REG1200_ICMSST_COMPLEMENTAR = 0.0, //VERIFICAR
                                    REG1300_ICMSST_RECUPERAR_RESSARCIR = 0.0, //VERIFICAR
                                    REG1400_ICMSST_RECUPERAR_RESSARCIR = 0.0, //VERIFICAR
                                    REG1500_ICMSST_RECUPERAR_RESSARCIR = 0.0, //VERIFICAR
                                    REG9000_FECOP_RESSARCIR = 0.0, //VERIFICAR
                                    REG9000_FECOP_COMPLEMENTAR = 0.0, //VERIFICAR
                                },
                                FimArquivo = new FimArquivo
                                {
                                    QTD_LIN = (3 + nfesEntrada.Count() + 1 + nfesSaida.Count() + 3) //VERIFICAR
                                }
                            });
                        }
                    }
                }

                return files;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Verifica se tem algum item com CNPJ distinto
        /// </summary>
        /// <param name="items"></param>
        public static void VerifyCNPJ(List<ItemFiltrado> items)
        {
            var error = items.Any(x => items.Any(y => y.CNPJ != x.CNPJ));

            if (error)
            {
                throw new Exception("Entre as notas foi encontrado ao menos 2 CNPJs distintos!");
            }
        }
    }
}
