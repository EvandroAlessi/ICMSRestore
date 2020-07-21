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
        private const string itemsInsertQuery = @"INSERT INTO ""Itens"" (
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
                                , ""CSOSN""
                                , ""vProd""
                                , ""cEANTrib""
                                , ""uTrib""
                                , ""qTrib""
                                , ""vUnTrib""
                                , ""indTot""
                                , ""NFeID""
                            ) VALUES (
                                 @nItem
                                , @cProd
                                , @cEAN
                                , @xProd
                                , @NCM
                                , @CFOP
                                , @uCom
                                , @qCom
                                , @vUnCom
                                , @orig
                                , @CST
                                , @modBC
                                , @vBC
                                , @pICMS
                                , @vICMS
                                , @cEnq
                                , @CST_IPI
                                , @CST_PIS
                                , @vBC_PIS
                                , @pPIS
                                , @vPIS
                                , @CST_COFINS
                                , @vBC_COFINS
                                , @pCOFINS
                                , @vCOFINS
                                , @CSOSN
                                , @vProd
                                , @cEANTrib
                                , @uTrib
                                , @qTrib
                                , @vUnTrib
                                , @indTot
                                , @nfeID
                            )";

        private const string invoiceInsertQuery = @"INSERT INTO ""NFe"" (
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
                                , ""Chave""
                            ) VALUES (
                                @ProcessoID
                                , @CEP
                                , @CEP_DEST
                                , @CNPJ
                                , @CNPJ_DEST
                                , @CPF_DEST
                                , @CRT
                                , @IE
                                , @IEST
                                , @UF
                                , @UF_DEST
                                , @cMun
                                , @cMun_DEST
                                , @cNF
                                , @cPais
                                , @cPais_DEST
                                , @cUF
                                , @dhEmi
                                , @dhSaiEnt
                                , @email_DEST
                                , @indPag
                                , @mod
                                , @nNF
                                , @natOp
                                , @nro
                                , @nro_DEST
                                , @serie
                                , @xBairro
                                , @xBairro_DEST
                                , @xFant
                                , @xLgr
                                , @xLgr_DEST
                                , @xMun
                                , @xMun_DEST
                                , @xNome
                                , @xNome_DEST
                                , @xPais
                                , @xPais_DEST
                                , @vBC_TOTAL
                                , @vICMS_TOTAL
                                , @vICMSDeson_TOTAL
                                , @vFCP_TOTAL
                                , @vBCST_TOTAL
                                , @vST_TOTAL
                                , @vFCPST_TOTAL
                                , @vFCPSTRet_TOTAL
                                , @vProd_TOTAL
                                , @vFrete_TOTAL
                                , @vSeg_TOTAL
                                , @vDesc_TOTAL
                                , @vII_TOTAL
                                , @vIPI_TOTAL
                                , @vIPIDevol_TOTAL
                                , @vPIS_TOTAL
                                , @vCOFINS_TOTAL
                                , @vOutro_TOTAL
                                , @vNF_TOTAL
                                , @Entrada
                                , @Chave
                            ) RETURNING ""ID"" ;";

        public NFeDAO() => table = "\"NFe\"";

        public NpgsqlCommand BuildItemsInsertCommand()
        {
            var cmd = new NpgsqlCommand(itemsInsertQuery);

            cmd.Parameters.Add("@nItem", NpgsqlTypes.NpgsqlDbType.Integer);
            cmd.Parameters.Add("@cProd", NpgsqlTypes.NpgsqlDbType.Varchar);
            cmd.Parameters.Add("@cEAN", NpgsqlTypes.NpgsqlDbType.Varchar);
            cmd.Parameters.Add("@xProd", NpgsqlTypes.NpgsqlDbType.Varchar);
            cmd.Parameters.Add("@NCM", NpgsqlTypes.NpgsqlDbType.Integer);
            cmd.Parameters.Add("@CFOP", NpgsqlTypes.NpgsqlDbType.Integer);
            cmd.Parameters.Add("@uCom", NpgsqlTypes.NpgsqlDbType.Varchar);
            cmd.Parameters.Add("@qCom", NpgsqlTypes.NpgsqlDbType.Real);
            cmd.Parameters.Add("@vUnCom", NpgsqlTypes.NpgsqlDbType.Real);
            cmd.Parameters.Add("@orig", NpgsqlTypes.NpgsqlDbType.Integer);
            cmd.Parameters.Add("@CST", NpgsqlTypes.NpgsqlDbType.Integer);
            cmd.Parameters.Add("@modBC", NpgsqlTypes.NpgsqlDbType.Integer);
            cmd.Parameters.Add("@vBC", NpgsqlTypes.NpgsqlDbType.Real);
            cmd.Parameters.Add("@vICMS", NpgsqlTypes.NpgsqlDbType.Real);
            cmd.Parameters.Add("@pICMS", NpgsqlTypes.NpgsqlDbType.Real);
            cmd.Parameters.Add("@cEnq", NpgsqlTypes.NpgsqlDbType.Integer);
            cmd.Parameters.Add("@CST_IPI", NpgsqlTypes.NpgsqlDbType.Varchar);
            cmd.Parameters.Add("@CST_PIS", NpgsqlTypes.NpgsqlDbType.Varchar);
            cmd.Parameters.Add("@vBC_PIS", NpgsqlTypes.NpgsqlDbType.Real);
            cmd.Parameters.Add("@pPIS", NpgsqlTypes.NpgsqlDbType.Real);
            cmd.Parameters.Add("@vPIS", NpgsqlTypes.NpgsqlDbType.Real);
            cmd.Parameters.Add("@CST_COFINS", NpgsqlTypes.NpgsqlDbType.Varchar);
            cmd.Parameters.Add("@vBC_COFINS", NpgsqlTypes.NpgsqlDbType.Real);
            cmd.Parameters.Add("@pCOFINS", NpgsqlTypes.NpgsqlDbType.Real);
            cmd.Parameters.Add("@vCOFINS", NpgsqlTypes.NpgsqlDbType.Real);
            cmd.Parameters.Add("@CSOSN", NpgsqlTypes.NpgsqlDbType.Integer);
            cmd.Parameters.Add("@vProd", NpgsqlTypes.NpgsqlDbType.Real);
            cmd.Parameters.Add("@cEANTrib", NpgsqlTypes.NpgsqlDbType.Varchar);
            cmd.Parameters.Add("@uTrib", NpgsqlTypes.NpgsqlDbType.Varchar);
            cmd.Parameters.Add("@qTrib", NpgsqlTypes.NpgsqlDbType.Real);
            cmd.Parameters.Add("@vUnTrib", NpgsqlTypes.NpgsqlDbType.Real);
            cmd.Parameters.Add("@indTot", NpgsqlTypes.NpgsqlDbType.Integer);
            cmd.Parameters.Add("@NFeID", NpgsqlTypes.NpgsqlDbType.Integer);

            return cmd;
        }

        public NpgsqlCommand BuildInsertCommand(NFe nfe)
        {
            var cmd = new NpgsqlCommand(invoiceInsertQuery);

            cmd.Parameters.AddWithNullableValue("@ProcessoID", NpgsqlTypes.NpgsqlDbType.Integer, nfe.ProcessoID);
            cmd.Parameters.AddWithNullableValue("@CEP", NpgsqlTypes.NpgsqlDbType.Varchar, nfe.CEP);
            cmd.Parameters.AddWithNullableValue("@CEP_DEST", NpgsqlTypes.NpgsqlDbType.Varchar, nfe.CEP_DEST);
            cmd.Parameters.AddWithNullableValue("@CNPJ", NpgsqlTypes.NpgsqlDbType.Varchar, nfe.CNPJ);
            cmd.Parameters.AddWithNullableValue("@CNPJ_DEST", NpgsqlTypes.NpgsqlDbType.Varchar, nfe.CNPJ_DEST);
            cmd.Parameters.AddWithNullableValue("@CPF_DEST", NpgsqlTypes.NpgsqlDbType.Varchar, nfe.CPF_DEST);
            cmd.Parameters.AddWithNullableValue("@CRT", NpgsqlTypes.NpgsqlDbType.Integer, nfe.CRT);
            cmd.Parameters.AddWithNullableValue("@IE", NpgsqlTypes.NpgsqlDbType.Varchar, nfe.IE);
            cmd.Parameters.AddWithNullableValue("@IEST", NpgsqlTypes.NpgsqlDbType.Varchar, nfe.IEST);
            cmd.Parameters.AddWithNullableValue("@UF", NpgsqlTypes.NpgsqlDbType.Varchar, nfe.UF);
            cmd.Parameters.AddWithNullableValue("@UF_DEST", NpgsqlTypes.NpgsqlDbType.Varchar, nfe.UF_DEST);
            cmd.Parameters.AddWithNullableValue("@cMun", NpgsqlTypes.NpgsqlDbType.Varchar, nfe.cMun);
            cmd.Parameters.AddWithNullableValue("@cMun_DEST", NpgsqlTypes.NpgsqlDbType.Varchar, nfe.cMun_DEST);
            cmd.Parameters.AddWithNullableValue("@cNF", NpgsqlTypes.NpgsqlDbType.Integer, nfe.cNF);
            cmd.Parameters.AddWithNullableValue("@cPais", NpgsqlTypes.NpgsqlDbType.Integer, nfe.cPais);
            cmd.Parameters.AddWithNullableValue("@cPais_DEST", NpgsqlTypes.NpgsqlDbType.Integer, nfe.cPais_DEST);
            cmd.Parameters.AddWithNullableValue("@cUF", NpgsqlTypes.NpgsqlDbType.Integer, nfe.cUF);
            cmd.Parameters.AddWithNullableValue("@dhEmi", NpgsqlTypes.NpgsqlDbType.TimestampTz, nfe.dhEmi);
            cmd.Parameters.AddWithNullableValue("@dhSaiEnt", NpgsqlTypes.NpgsqlDbType.TimestampTz, nfe.dhSaiEnt);
            cmd.Parameters.AddWithNullableValue("@email_DEST", NpgsqlTypes.NpgsqlDbType.Varchar, nfe.email_DEST);
            cmd.Parameters.AddWithNullableValue("@indPag", NpgsqlTypes.NpgsqlDbType.Integer, nfe.indPag);
            cmd.Parameters.AddWithNullableValue("@mod", NpgsqlTypes.NpgsqlDbType.Varchar, nfe.mod);
            cmd.Parameters.AddWithNullableValue("@nNF", NpgsqlTypes.NpgsqlDbType.Integer, nfe.nNF);
            cmd.Parameters.AddWithNullableValue("@natOp", NpgsqlTypes.NpgsqlDbType.Varchar, nfe.natOp);
            cmd.Parameters.AddWithNullableValue("@nro", NpgsqlTypes.NpgsqlDbType.Varchar, nfe.nro);
            cmd.Parameters.AddWithNullableValue("@nro_DEST", NpgsqlTypes.NpgsqlDbType.Varchar, nfe.nro_DEST);
            cmd.Parameters.AddWithNullableValue("@serie", NpgsqlTypes.NpgsqlDbType.Integer, nfe.serie);
            cmd.Parameters.AddWithNullableValue("@xBairro", NpgsqlTypes.NpgsqlDbType.Varchar, nfe.xBairro);
            cmd.Parameters.AddWithNullableValue("@xBairro_DEST", NpgsqlTypes.NpgsqlDbType.Varchar, nfe.xBairro_DEST);
            cmd.Parameters.AddWithNullableValue("@xFant", NpgsqlTypes.NpgsqlDbType.Varchar, nfe.xFant);
            cmd.Parameters.AddWithNullableValue("@xLgr", NpgsqlTypes.NpgsqlDbType.Varchar, nfe.xLgr);
            cmd.Parameters.AddWithNullableValue("@xLgr_DEST", NpgsqlTypes.NpgsqlDbType.Varchar, nfe.xLgr_DEST);
            cmd.Parameters.AddWithNullableValue("@xMun", NpgsqlTypes.NpgsqlDbType.Varchar, nfe.xMun);
            cmd.Parameters.AddWithNullableValue("@xMun_DEST", NpgsqlTypes.NpgsqlDbType.Varchar, nfe.xMun_DEST);
            cmd.Parameters.AddWithNullableValue("@xNome", NpgsqlTypes.NpgsqlDbType.Varchar, nfe.xNome);
            cmd.Parameters.AddWithNullableValue("@xNome_DEST", NpgsqlTypes.NpgsqlDbType.Varchar, nfe.xNome_DEST);
            cmd.Parameters.AddWithNullableValue("@xPais", NpgsqlTypes.NpgsqlDbType.Varchar, nfe.xPais);
            cmd.Parameters.AddWithNullableValue("@xPais_DEST", NpgsqlTypes.NpgsqlDbType.Varchar, nfe.xPais_DEST);
            cmd.Parameters.AddWithNullableValue("@vBC_TOTAL", NpgsqlTypes.NpgsqlDbType.Real, nfe.vBC_TOTAL);
            cmd.Parameters.AddWithNullableValue("@vICMS_TOTAL", NpgsqlTypes.NpgsqlDbType.Real, nfe.vICMS_TOTAL);
            cmd.Parameters.AddWithNullableValue("@vICMSDeson_TOTAL", NpgsqlTypes.NpgsqlDbType.Real, nfe.vICMSDeson_TOTAL);
            cmd.Parameters.AddWithNullableValue("@vFCP_TOTAL", NpgsqlTypes.NpgsqlDbType.Real, nfe.vFCP_TOTAL);
            cmd.Parameters.AddWithNullableValue("@vBCST_TOTAL", NpgsqlTypes.NpgsqlDbType.Real, nfe.vBCST_TOTAL);
            cmd.Parameters.AddWithNullableValue("@vST_TOTAL", NpgsqlTypes.NpgsqlDbType.Real, nfe.vST_TOTAL);
            cmd.Parameters.AddWithNullableValue("@vFCPST_TOTAL", NpgsqlTypes.NpgsqlDbType.Real, nfe.vFCPST_TOTAL);
            cmd.Parameters.AddWithNullableValue("@vFCPSTRet_TOTAL", NpgsqlTypes.NpgsqlDbType.Real, nfe.vFCPSTRet_TOTAL);
            cmd.Parameters.AddWithNullableValue("@vProd_TOTAL", NpgsqlTypes.NpgsqlDbType.Real, nfe.vProd_TOTAL);
            cmd.Parameters.AddWithNullableValue("@vFrete_TOTAL", NpgsqlTypes.NpgsqlDbType.Real, nfe.vFrete_TOTAL);
            cmd.Parameters.AddWithNullableValue("@vSeg_TOTAL", NpgsqlTypes.NpgsqlDbType.Real, nfe.vSeg_TOTAL);
            cmd.Parameters.AddWithNullableValue("@vDesc_TOTAL", NpgsqlTypes.NpgsqlDbType.Real, nfe.vDesc_TOTAL);
            cmd.Parameters.AddWithNullableValue("@vII_TOTAL", NpgsqlTypes.NpgsqlDbType.Real, nfe.vII_TOTAL);
            cmd.Parameters.AddWithNullableValue("@vIPI_TOTAL", NpgsqlTypes.NpgsqlDbType.Real, nfe.vIPI_TOTAL);
            cmd.Parameters.AddWithNullableValue("@vIPIDevol_TOTAL", NpgsqlTypes.NpgsqlDbType.Real, nfe.vIPIDevol_TOTAL);
            cmd.Parameters.AddWithNullableValue("@vPIS_TOTAL", NpgsqlTypes.NpgsqlDbType.Real, nfe.vPIS_TOTAL);
            cmd.Parameters.AddWithNullableValue("@vCOFINS_TOTAL", NpgsqlTypes.NpgsqlDbType.Real, nfe.vCOFINS_TOTAL);
            cmd.Parameters.AddWithNullableValue("@vOutro_TOTAL", NpgsqlTypes.NpgsqlDbType.Real, nfe.vOutro_TOTAL);
            cmd.Parameters.AddWithNullableValue("@vNF_TOTAL", NpgsqlTypes.NpgsqlDbType.Real, nfe.vNF_TOTAL);
            cmd.Parameters.AddWithNullableValue("@Entrada", NpgsqlTypes.NpgsqlDbType.Boolean, nfe.Entrada);
            cmd.Parameters.AddWithNullableValue("@Chave", NpgsqlTypes.NpgsqlDbType.Varchar, nfe.Chave);

            return cmd;
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
                ProcessoID = Convert.ToInt32(reader["ProcessoID"]),
                Entrada = reader.GetFieldValue<bool>("Entrada"),
                Chave = reader["Chave"]?.ToString(),
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
                CNPJ = reader["CNPJ"]?.ToString(),
                Entrada = reader.GetFieldValue<bool>("Entrada"),
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

        public int? Exists(string chave, int processoID)
        {
            try
            {
                int? currentID = null;

                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $@"SELECT ""ID"" FROM { table } 
                                WHERE ""ProcessoID"" = { processoID }
                                    AND ""Chave"" = '{ chave }';";
                        //AND ""nNF"" = { nNF }

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                currentID = Convert.ToInt32(reader["id"]);
                                break;
                            }
                        }
                    }

                    conn.Close();
                }

                return currentID;
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

                    using (var cmd = BuildInsertCommand(nfe))
                    {
                        cmd.Connection = conn;

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
                            using (var cmd = BuildInsertCommand(nfe))
                            {
                                cmd.Connection = conn;
                                cmd.Transaction = transaction;
                                cmd.CommandType = CommandType.Text;
                                //cmd.Prepare();

                                var nfeID = (int)cmd.ExecuteScalar();

                                if (nfeID <= 0)
                                {
                                    // rollback
                                    throw new NpgsqlException();
                                }

                                using(var cmd2 = BuildItemsInsertCommand())
                                {
                                    cmd2.Connection = conn;
                                    cmd2.Transaction = transaction;
                                    cmd2.CommandType = CommandType.Text;

                                    foreach (var item in itens)
                                    {
                                        cmd2.Parameters.ChangeParameterValue(0, item.nItem);
                                        cmd2.Parameters.ChangeParameterValue(1, item.cProd);
                                        cmd2.Parameters.ChangeParameterValue(2, item.cEAN);
                                        cmd2.Parameters.ChangeParameterValue(3, item.xProd);
                                        cmd2.Parameters.ChangeParameterValue(4, item.NCM);
                                        cmd2.Parameters.ChangeParameterValue(5, item.CFOP);
                                        cmd2.Parameters.ChangeParameterValue(6, item.uCom);
                                        cmd2.Parameters.ChangeParameterValue(7, item.qCom);
                                        cmd2.Parameters.ChangeParameterValue(8, item.vUnCom);
                                        cmd2.Parameters.ChangeParameterValue(9, item.orig);
                                        cmd2.Parameters.ChangeParameterValue(10, item.CST);
                                        cmd2.Parameters.ChangeParameterValue(11, item.modBC);
                                        cmd2.Parameters.ChangeParameterValue(12, item.vBC);
                                        cmd2.Parameters.ChangeParameterValue(13, item.vICMS);
                                        cmd2.Parameters.ChangeParameterValue(14, item.pICMS);
                                        cmd2.Parameters.ChangeParameterValue(15, item.cEnq);
                                        cmd2.Parameters.ChangeParameterValue(16, item.CST_IPI);
                                        cmd2.Parameters.ChangeParameterValue(17, item.CST_PIS);
                                        cmd2.Parameters.ChangeParameterValue(18, item.vBC_PIS);
                                        cmd2.Parameters.ChangeParameterValue(19, item.pPIS);
                                        cmd2.Parameters.ChangeParameterValue(20, item.vPIS);
                                        cmd2.Parameters.ChangeParameterValue(21, item.CST_COFINS);
                                        cmd2.Parameters.ChangeParameterValue(22, item.vBC_COFINS);
                                        cmd2.Parameters.ChangeParameterValue(23, item.pCOFINS);
                                        cmd2.Parameters.ChangeParameterValue(24, item.vCOFINS);
                                        cmd2.Parameters.ChangeParameterValue(25, item.CSOSN);
                                        cmd2.Parameters.ChangeParameterValue(26, item.vProd);
                                        cmd2.Parameters.ChangeParameterValue(27, item.cEANTrib);
                                        cmd2.Parameters.ChangeParameterValue(28, item.uTrib);
                                        cmd2.Parameters.ChangeParameterValue(29, item.qTrib);
                                        cmd2.Parameters.ChangeParameterValue(30, item.vUnTrib);
                                        cmd2.Parameters.ChangeParameterValue(31, item.indTot);
                                        cmd2.Parameters.ChangeParameterValue(32, nfeID);

                                        if (cmd2.ExecuteNonQuery() != 1)
                                        {
                                            // rollback
                                            throw new NpgsqlException();
                                        }
                                    }
                                }

                                transaction.Commit();
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

                    using (var cmd = BuildInsertCommand(nfe))
                    {
                        cmd.Connection = conn;

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
