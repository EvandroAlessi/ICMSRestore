using CrossCutting;
using CrossCutting.Models;
using Dominio;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class ProcessoUploadDAO
    {
        static string connString = AppSettings.ConnectionString;
        const string quote = "\"";

        public ProcessoUpload Make(NpgsqlDataReader reader)
        {
            return new ProcessoUpload
            {
                ID = Convert.ToInt32(reader["ID"]),
                PastaZip = reader["PastaZip"]?.ToString(),
                ProcessoID = Convert.ToInt32(reader["ProcessoID"]),
                QntArq = Convert.ToInt32(reader["QntArq"]),
                Ativo = Convert.ToBoolean(reader["Ativo"]),
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

        public async Task<List<ProcessoUpload>> GetAll(int skip = 0, int take = 30, Dictionary<string, string> filters = null)
        {
            try
            {
                var list = new List<ProcessoUpload>();

                using (var conn = new NpgsqlConnection(connString))
                {
                    await conn.OpenAsync();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $@"SELECT * FROM {quote}ProcessosUpload{ quote}
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

        public async Task<List<ProcessoUpload>> GetAll(int processoID)
        {
            try
            {
                var list = new List<ProcessoUpload>();

                using (var conn = new NpgsqlConnection(connString))
                {
                    await conn.OpenAsync();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $@"SELECT * FROM {quote}ProcessosUpload{ quote}
                                WHERE {quote}ProcessoID{quote}= { processoID };";

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

        public async Task<ProcessoUpload> Get(int id)
        {
            try
            {
                ProcessoUpload processoUpload = null;

                using (var conn = new NpgsqlConnection(connString))
                {
                    await conn.OpenAsync();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $@"SELECT * FROM {quote}ProcessosUpload{quote} 
                                WHERE {quote}ID{quote} = { id };";

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                processoUpload = Make(reader);
                            }
                        }
                    }

                    await conn.CloseAsync();
                }

                return processoUpload;
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
                        cmd.CommandText = $@"SELECT {quote}ID{quote} FROM {quote}ProcessosUpload{quote} 
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

        public ProcessoUpload Insert(ProcessoUpload processoUpload)
        {
            try
            {
                object id;

                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $@"INSERT INTO {quote}ProcessosUpload{quote} (
                                {quote}ProcessoID{quote}
                                , {quote}PastaZip{quote}
                                , {quote}QntArq{quote}
                                , {quote}Ativo{quote}
                            ) VALUES (
                                '{ processoUpload.ProcessoID }'
                                , '{ processoUpload.PastaZip }'
                                , '{ processoUpload.QntArq }'
                                , '{ processoUpload.Ativo }')
                            RETURNING {quote}ID{quote};";

                        id = cmd.ExecuteScalar();
                    }

                    conn.Close();
                }

                if (id != null && id.GetType() != typeof(DBNull))
                {
                    processoUpload.ID = (int)id;

                    return processoUpload;
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

        public bool Edit(ProcessoUpload processoUpload)
        {
            try
            {
                int rows = 0;

                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $@"UPDATE {quote}ProcessosUpload{quote} SET
                                {quote}ProcessoID{quote} = '{ processoUpload.ProcessoID }'
                                , {quote}PastaZip{quote} = '{ processoUpload.PastaZip }'
                                , {quote}QntArq{quote} = '{ processoUpload.QntArq }'
                                , {quote}Ativo{quote} = '{ processoUpload.Ativo }'
                            WHERE {quote}ID{quote} = { processoUpload.ID };";

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
                        cmd.CommandText = $@"DELETE FROM {quote}ProcessosUpload{quote}
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
