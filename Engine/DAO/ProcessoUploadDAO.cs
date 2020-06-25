using CrossCutting;
using Dominio;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DAO
{
    public class ProcessoUploadDAO : CommonDAO
    {
        public ProcessoUploadDAO() => table = "\"ProcessosUpload\"";

        public ProcessoUpload BuildObject(NpgsqlDataReader reader)
        {
            return new ProcessoUpload
            {
                ID = Convert.ToInt32(reader["ID"]),
                PastaZip = reader["PastaZip"]?.ToString(),
                ProcessoID = Convert.ToInt32(reader["ProcessoID"]),
                QntArq = Convert.ToInt32(reader["QntArq"]),
                Ativo = Convert.ToBoolean(reader["Ativo"]),
                DataInicio = reader.GetFieldValue<DateTime?>("DataInicio"),
                Entrada = Convert.ToBoolean(reader["Entrada"]),
            };
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

        public async Task<List<ProcessoUpload>> GetAll(int processoID, int skip = 0, int take = 30)
        {
            try
            {
                var list = new List<ProcessoUpload>();

                using (var conn = new NpgsqlConnection(connString))
                {
                    await conn.OpenAsync();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $@"SELECT * FROM { table }
                                            WHERE ""ProcessoID"" = { processoID }
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
                        cmd.CommandText = $@"SELECT * FROM { table } 
                                WHERE ""ID"" = { id };";

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                processoUpload = BuildObject(reader);
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

        public async Task<ProcessoUpload> Get(int processoID, string zipPath)
        {
            try
            {
                ProcessoUpload processoUpload = null;

                using (var conn = new NpgsqlConnection(connString))
                {
                    await conn.OpenAsync();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $@"SELECT * FROM { table } 
                                WHERE ""ProcessoID"" = { processoID }
                                    AND ""PastaZip"" = '{ zipPath }';";

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                processoUpload = BuildObject(reader);
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
                        cmd.CommandText = $@"INSERT INTO { table } (
                                ""ProcessoID""
                                , ""PastaZip""
                                , ""QntArq""
                                , ""Ativo""
                                , ""DataInicio""
                                , ""Entrada""
                            ) VALUES (
                                { processoUpload.ProcessoID }
                                , '{ processoUpload.PastaZip }'
                                , { processoUpload.QntArq }
                                , '{ processoUpload.Ativo }'
                                , '{ processoUpload.DataInicio ?? DateTime.Now }'
                                , '{ processoUpload.Entrada }')
                            RETURNING ""ID"";";

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

        public ProcessoUpload Edit(ProcessoUpload processoUpload)
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
                                ""ProcessoID"" = { processoUpload.ProcessoID }
                                , ""PastaZip"" = '{ processoUpload.PastaZip }'
                                , ""QntArq"" = { processoUpload.QntArq }
                                , ""Ativo"" = '{ processoUpload.Ativo }'
                                , ""DataInicio"" = '{ processoUpload.DataInicio }'
                            WHERE ""ID"" = { processoUpload.ID };";

                        rows = cmd.ExecuteNonQuery();
                    }

                    conn.Close();
                }

                return rows > 0 ? processoUpload : null;
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
