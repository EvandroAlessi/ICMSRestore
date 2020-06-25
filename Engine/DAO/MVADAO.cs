using CrossCutting;
using CrossCutting.Models;
using Dominio;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAO
{
    public class MVADAO : CommonDAO
    {
        public MVADAO() => table = "\"MVA\"";

        public MVA BuildObject(NpgsqlDataReader reader)
        {
            return new MVA
            {
                ID = Convert.ToInt32(reader["ID"]),
                CEST = Convert.ToInt32(reader["CEST"]),
                Descricao = reader["Descricao"]?.ToString(),
                MVA_ST = Convert.ToDouble(reader["xProd"]),
                NCM_SH = Convert.ToInt32(reader["NCM_SH"]),
                DataInicial = Convert.ToDateTime(reader["DataInicial"]),
                DataFinal = Convert.ToDateTime(reader["DataFinal"])
            };
        }

        public async Task<List<MVA>> GetAll(int skip = 0, int take = 30, Dictionary<string, string> filters = null)
        {
            try
            {
                var list = new List<MVA>();

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

        public async Task<MVA> Get(int id)
        {
            try
            {
                MVA mva = null;

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
                                mva = BuildObject(reader);
                            }
                        }
                    }

                    await conn.CloseAsync();
                }

                return mva;
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

        public MVA Insert(MVA mva)
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
                                ""CEST""
                                , ""Descricao""
                                , ""MVA_ST""
                                , ""NCM_SH""
                                , ""DataInicial""
                                , ""DataFinal""
                            ) VALUES (
                                { mva.CEST }
                                , '{ mva.Descricao }'
                                , { mva.MVA_ST }
                                , { mva.NCM_SH }
                                , { mva.DataInicial }
                                , { mva.DataFinal })
                            RETURNING ""ID"";";

                        id = cmd.ExecuteScalar();
                    }

                    conn.Close();
                }

                if (id != null && id.GetType() != typeof(DBNull))
                {
                    mva.ID = (int)id;

                    return mva;
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

        public MVA Edit(MVA mva)
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
                                ""CEST"" = { mva.CEST }
                                , ""Descricao"" = '{ mva.Descricao }'
                                , ""MVA_ST"" = { mva.MVA_ST }
                                , ""NCM_SH"" = '{ mva.NCM_SH }'
                                , ""DataInicial"" = '{ mva.DataInicial }'
                                , ""DataFinal"" = '{ mva.DataFinal }'
                            WHERE ""ID"" = { mva.ID };";

                        rows = cmd.ExecuteNonQuery();
                    }

                    conn.Close();
                }

                return rows > 0 ? mva : null;
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
