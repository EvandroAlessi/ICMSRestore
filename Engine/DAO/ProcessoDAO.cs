using CrossCutting;
using Dominio;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class ProcessoDAO
    {
        static string connString = AppSettings.ConnectionString;
        const string quote = "\"";

        public async Task<List<Processo>> GetAll()
        {
            try
            {
                var list = new List<Processo>();

                using (var conn = new NpgsqlConnection(connString))
                {
                    await conn.OpenAsync();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $@"SELECT * FROM {quote}Processos{ quote};";

                        using (var reader = cmd.ExecuteReader())
                        {
                            var empresaDAO = new EmpresaDAO();

                            while (reader.Read())
                            {
                                var processo = new Processo
                                {
                                    ID = Convert.ToInt32(reader["ID"]),
                                    Nome = reader["Nome"]?.ToString(),
                                    DataCriacao = Convert.ToDateTime(reader["DataCriacao"]),
                                    InicioPeriodo = Convert.ToDateTime(reader["InicioPeriodo"]),
                                    FimPeriodo = Convert.ToDateTime(reader["FimPeriodo"]),
                                    Empresa = await empresaDAO.Get(Convert.ToInt32(reader["EmpresaID"]))
                                };

                                list.Add(processo);
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
                        cmd.CommandText = $@"SELECT * FROM {quote}Processos{quote} 
                                WHERE {quote}ID{quote} = { id };";

                        using (var reader = cmd.ExecuteReader())
                        {
                            var empresaDAO = new EmpresaDAO();

                            while (reader.Read())
                            {
                                processo = new Processo
                                {
                                    ID = Convert.ToInt32(reader["ID"]),
                                    Nome = reader["Nome"]?.ToString(),
                                    DataCriacao = Convert.ToDateTime(reader["DataCriacao"]),
                                    InicioPeriodo = Convert.ToDateTime(reader["InicioPeriodo"]),
                                    FimPeriodo = Convert.ToDateTime(reader["FimPeriodo"]),
                                    Empresa = await empresaDAO.Get(Convert.ToInt32(reader["EmpresaID"]))
                                };
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
                        cmd.CommandText = $@"SELECT {quote}ID{quote} FROM {quote}Processos{quote} 
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

        public Processo Insert(Processo processo)
        {
            try
            {
                object id;

                if (processo.DataCriacao == null)
                {
                    processo.DataCriacao = DateTime.Now;
                }

                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $@"INSERT INTO {quote}Processos{ quote} (
                                {quote}EmpresaID{quote}
                                , {quote}Nome{quote}
                                , {quote}DataCriacao{quote}
                                , {quote}InicioPeriodo{quote}
                                , {quote}FimPeriodo{quote}
                            ) VALUES (
                                { processo.Empresa.ID }
                                , '{ processo.Nome }'
                                , '{ processo.DataCriacao }'
                                , '{ processo.InicioPeriodo }'
                                , '{ processo.FimPeriodo }')
                            RETURNING {quote}ID{quote};";

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

        public bool Edit(Processo processo)
        {
            try
            {
                int rows = 0;

                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $@"UPDATE {quote}Processos{quote} SET
                                {quote}EmpresaID{quote} = '{ processo.Empresa.ID }'
                                , {quote}Nome{quote} = '{ processo.Nome }'
                                , {quote}DataCriacao{quote} = '{ processo.DataCriacao }'
                                , {quote}InicioPeriodo{quote} = '{ processo.InicioPeriodo }'
                                , {quote}FimPeriodo{quote} = '{ processo.FimPeriodo }'
                            WHERE {quote}ID{quote} = { processo.ID };";

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
                        cmd.CommandText = $@"DELETE FROM {quote}Processos{quote}
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
