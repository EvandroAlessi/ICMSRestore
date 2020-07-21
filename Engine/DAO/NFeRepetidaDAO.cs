using CrossCutting;
using Dominio;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAO
{
    public class NFeRepetidaDAO : CommonDAO
    {
        public NFeRepetidaDAO() => table = "\"NFeRepetidas\"";

        public NFeRepetida BuildObject(NpgsqlDataReader reader)
        {
            return new NFeRepetida
            {
                ID = Convert.ToInt32(reader["ID"]),
                NFeID = Convert.ToInt32(reader["NFeID"]),
                ProcessoID = Convert.ToInt32(reader["ProcessoID"]),
                DataHora = Convert.ToDateTime(reader["DataHora"]),
                StackTrace = reader["StackTrace"]?.ToString(),
                Chave = reader["Chave"]?.ToString(),
                MensagemErro = reader["MensagemErro"]?.ToString(),
                XML = reader["XML"]?.ToString(),
            };
        }

        public async Task<List<NFeRepetida>> GetAll(int skip = 0, int take = 30, Dictionary<string, string> filters = null)
        {
            try
            {
                var list = new List<NFeRepetida>();

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

        public async Task<NFeRepetida> Get(int id)
        {
            try
            {
                NFeRepetida repetida = null;

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
                                repetida = BuildObject(reader);
                            }
                        }
                    }

                    await conn.CloseAsync();
                }

                return repetida;
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

        public void Insert(NFeRepetida repetida)
        {
            try
            {
                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $@"INSERT INTO { table } (
                                ""StackTrace""
                                , ""MensagemErro""
                                , ""DataHora""
                                , ""ProcessoID""
                                , ""Chave""
                                , ""XML""
                                , ""NFeID""
                            ) VALUES (
                                '{ repetida.StackTrace }'
                                , '{ repetida.MensagemErro }'
                                , '{ repetida.DataHora ?? DateTime.Now }'
                                , { repetida.ProcessoID }
                                , '{ repetida.Chave }'
                                , '{ repetida.XML }'
                                , { repetida.NFeID }
                            );";

                        if (cmd.ExecuteNonQuery() == 0)
                        {
                            throw new NpgsqlException($"Não foi possivel inserir NFeRepetida, NFeID: { repetida.NFeID }, xml: { repetida.XML }");
                        }
                    }

                    conn.Close();
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

        public NFeRepetida Edit(NFeRepetida repetida)
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
                                ""StackTrace"" = '{ repetida.StackTrace }'
                                , ""MensagemErro"" = '{ repetida.MensagemErro }'
                                , ""ProcessoID"" = { repetida.ProcessoID }
                                , ""Chave"" = '{ repetida.Chave }'
                                , ""XML"" = '{ repetida.XML }'
                                , ""NFeID"" = { repetida.NFeID }
                            WHERE ""ID"" = { repetida.ID };";

                        rows = cmd.ExecuteNonQuery();
                    }

                    conn.Close();
                }

                return rows > 0 ? repetida : null;
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
