using CrossCutting;
using CrossCutting.Models;
using Dominio;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAO
{
    public class ProcessoDAO : CommonDAO
    {
        public ProcessoDAO() => table = "\"Processos\"";

        public Processo BuildObject(NpgsqlDataReader reader)
        {
            return new Processo
            {
                ID = Convert.ToInt32(reader["ID"]),
                Nome = reader["Nome"]?.ToString(),
                DataCriacao = Convert.ToDateTime(reader["DataCriacao"]),
                InicioPeriodo = Convert.ToDateTime(reader["InicioPeriodo"]),
                FimPeriodo = Convert.ToDateTime(reader["FimPeriodo"]),
                EmpresaID = Convert.ToInt32(reader["EmpresaID"]),
                Finalizado = Convert.ToBoolean(reader["Finalizado"])
            };
        }

        public async Task<List<Processo>> GetAll(int skip = 0, int take = 30, Dictionary<string, string> filters = null)
        {
            try
            {
                var list = new List<Processo>();

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
                            var empresaDAO = new EmpresaDAO();

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

        public async Task<Processo> Get(int id)
        {
            try
            {
                Processo processo = null;

                using (var conn = new NpgsqlConnection(connString))
                {
                    await conn.OpenAsync();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $@"SELECT * FROM { table }
                                WHERE ""ID"" = { id };";

                        using (var reader = cmd.ExecuteReader())
                        {
                            var empresaDAO = new EmpresaDAO();

                            while (reader.Read())
                            {
                                processo = BuildObject(reader);
                            }
                        }
                    }

                    await conn.CloseAsync();
                }

                return processo;
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

        public async Task<long> GetFinishedCount()
        {
            try
            {
                long count = 0;

                using (var conn = new NpgsqlConnection(connString))
                {
                    await conn.OpenAsync();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $@"SELECT Count(*) FROM { table }
                                            WHERE ""Finalizado"";";

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                count = reader.GetInt32(0);

                                break;
                            }
                        }
                    }

                    await conn.CloseAsync();
                }

                return count;
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

        public Processo Insert(Processo processo)
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
                                ""EmpresaID""
                                , ""Nome""
                                , ""DataCriacao""
                                , ""InicioPeriodo""
                                , ""FimPeriodo""
                                , ""Finalizado""
                            ) VALUES (
                                { processo.EmpresaID }
                                , '{ processo.Nome }'
                                , '{ processo.DataCriacao ?? DateTime.Now }'
                                , '{ processo.InicioPeriodo }'
                                , '{ processo.FimPeriodo }'
                                , '{ processo.Finalizado }'
                            )
                            RETURNING ""ID"";";

                        id = cmd.ExecuteScalar();
                    }

                    conn.Close();
                }

                if (id != null && id.GetType() != typeof(DBNull))
                {
                    processo.ID = (int)id;

                    return processo;
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

        public Processo Edit(Processo processo)
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
                                ""EmpresaID"" = '{ processo.EmpresaID }'
                                , ""Nome"" = '{ processo.Nome }'
                                , ""DataCriacao"" = '{ processo.DataCriacao }'
                                , ""InicioPeriodo"" = '{ processo.InicioPeriodo }'
                                , ""FimPeriodo"" = '{ processo.FimPeriodo }'
                                , ""Finalizado"" = '{ processo.Finalizado }'
                            WHERE ""ID"" = { processo.ID };";

                        rows = cmd.ExecuteNonQuery();
                    }

                    conn.Close();
                }

                return rows > 0 ? processo : null;
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
