using CrossCutting;
using CrossCutting.Models;
using Dominio;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAO
{
    public class MVADAO
    {
        static string connString = AppSettings.ConnectionString;
        const string quote = "\"";

        public MVA Make(NpgsqlDataReader reader)
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
                        cmd.CommandText = $@"SELECT * FROM {quote}MVA{ quote}
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
                        cmd.CommandText = $@"SELECT * FROM {quote}MVA{quote} 
                                WHERE {quote}ID{quote} = { id };";

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                mva = Make(reader);
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
                        cmd.CommandText = $@"SELECT {quote}ID{quote} FROM {quote}MVA{quote} 
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
                        cmd.CommandText = $@"INSERT INTO {quote}MVA{quote} (
                                {quote}CEST{quote}
                                , {quote}Descricao{quote}
                                , {quote}MVA_ST{quote}
                                , {quote}NCM_SH{quote}
                                , {quote}DataInicial{quote}
                                , {quote}DataFinal{quote}
                            ) VALUES (
                                { mva.CEST }
                                , '{ mva.Descricao }'
                                , { mva.MVA_ST }
                                , { mva.NCM_SH }
                                , { mva.DataInicial }
                                , { mva.DataFinal })
                            RETURNING {quote}ID{quote};";

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

        public bool Edit(MVA mva)
        {
            try
            {
                int rows = 0;

                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $@"UPDATE {quote}MVA{quote} SET
                                {quote}CEST{quote} = { mva.CEST }
                                , {quote}Descricao{quote} = '{ mva.Descricao }'
                                , {quote}MVA_ST{quote} = { mva.MVA_ST }
                                , {quote}NCM_SH{quote} = '{ mva.NCM_SH }'
                                , {quote}DataInicial{quote} = '{ mva.DataInicial }'
                                , {quote}DataFinal{quote} = '{ mva.DataFinal }'
                            WHERE {quote}ID{quote} = { mva.ID };";

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
                        cmd.CommandText = $@"DELETE FROM {quote}MVA{quote}
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
