using CrossCutting;
using Dominio;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAO
{
    public class ConversorDAO : CommonDAO
    {
        public ConversorDAO() => table = "\"Conversores\"";

        public Conversor BuildObject(NpgsqlDataReader reader)
        {
            return new Conversor
            {
                ID = Convert.ToInt32(reader["ID"]),
                EmpresaID = Convert.ToInt32(reader["EmpresaID"]),
                Unidade = reader["Unidade"]?.ToString(),
                UnidadeResultante = reader["UnidadeResultante"]?.ToString(),
                FatorConversao = Convert.ToDouble(reader["FatorConversao"]),
                cProd = reader["cProd"]?.ToString(),
                NCM = Convert.ToInt32(reader["NCM"]),
            };
        }

        public async Task<List<Conversor>> GetAll(int skip = 0, int take = 30, Dictionary<string, string> filters = null)
        {
            try
            {
                var list = new List<Conversor>();

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

        public async Task<List<Conversor>> GetAllByCompany(int companyID)
        {
            try
            {
                var list = new List<Conversor>();

                using (var conn = new NpgsqlConnection(connString))
                {
                    await conn.OpenAsync();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $@"SELECT * FROM { table } 
                                            WHERE ""EmpresaID"" = { companyID }
                                            ORDER BY ""ID"" desc;";

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

        public async Task<Conversor> Get(int id)
        {
            try
            {
                Conversor conversor = null;

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
                                conversor = BuildObject(reader);

                                break;
                            }
                        }
                    }

                    await conn.CloseAsync();
                }

                return conversor;
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

        public Conversor Insert(Conversor conversor)
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
                                , ""Unidade""
                                , ""UnidadeResultante""
                                , ""FatorConversao""
                                , ""cProd""
                                , ""NCM""
                            ) VALUES (
                                '{ conversor.EmpresaID }'
                                , '{ conversor.Unidade }'
                                , '{ conversor.UnidadeResultante }'
                                , '{ conversor.FatorConversao }'
                                , '{ conversor.cProd }'
                                , '{ conversor.NCM }'
                            )
                            RETURNING ""ID"";";

                        id = cmd.ExecuteScalar();
                    }

                    conn.Close();
                }

                if (id != null && id.GetType() != typeof(DBNull))
                {
                    conversor.ID = (int)id;

                    return conversor;
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

        public Conversor Edit(Conversor conversor)
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
                                ""EmpresaID"" = '{ conversor.EmpresaID }'
                                , ""Unidade"" = '{ conversor.Unidade }'
                                , ""UnidadeResultante"" = '{ conversor.UnidadeResultante }'
                                , ""FatorConversao"" = '{ conversor.FatorConversao }'
                                , ""cProd"" = '{ conversor.cProd }'
                                , ""NCM"" = '{ conversor.NCM }'
                            WHERE ""ID"" = { conversor.ID };";

                        rows = cmd.ExecuteNonQuery();
                    }

                    conn.Close();
                }

                return rows > 0 ? conversor : null;
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
