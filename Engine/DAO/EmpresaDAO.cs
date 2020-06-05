﻿using CrossCutting;
using Dominio;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class EmpresaDAO
    {
        static string connString = AppSettings.ConnectionString;
        const string quote = "\"";

        public async Task<List<Empresa>> GetAll()
        {
            try
            {
                var list = new List<Empresa>();

                using (var conn = new NpgsqlConnection(connString))
                {
                    await conn.OpenAsync();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $@"SELECT * FROM {quote}Empresas{ quote};";

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var empresa = new Empresa
                                {
                                    ID = Convert.ToInt32(reader["ID"]),
                                    CNPJ = reader["CNPJ"]?.ToString(),
                                    Nome = reader["Nome"]?.ToString(),
                                    Cidade = reader["Cidade"]?.ToString(),
                                    UF = reader["UF"]?.ToString(),
                                };

                                list.Add(empresa);
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

        public async Task<Empresa> Get(int id)
        {
            try
            {
                Empresa empresa = null;

                using (var conn = new NpgsqlConnection(connString))
                {
                    await conn.OpenAsync();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $@"SELECT * FROM {quote}Empresas{quote} 
                                WHERE {quote}ID{quote} = { id };";

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                empresa = new Empresa
                                {
                                    ID = Convert.ToInt32(reader["ID"]),
                                    CNPJ = reader["CNPJ"]?.ToString(),
                                    Nome = reader["Nome"]?.ToString(),
                                    Cidade = reader["Cidade"]?.ToString(),
                                    UF = reader["UF"]?.ToString(),
                                };
                            }
                        }
                    }

                    await conn.CloseAsync();
                }

                return empresa;
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
                        cmd.CommandText = $@"SELECT {quote}ID{quote}  FROM {quote}Empresas{quote} 
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

        public Empresa Insert(Empresa empresa)
        {
            try
            {
                object id;

                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $@"INSERT INTO {quote}Empresas{quote} (
                                {quote}Nome{quote}
                                , {quote}Cidade{quote}
                                , {quote}UF{quote}
                            ) VALUES (
                                '{ empresa.Nome }'
                                , '{ empresa.Cidade }'
                                , '{ empresa.UF }')
                            RETURNING {quote}ID{quote};";

                        id = cmd.ExecuteScalar();
                    }

                    conn.Close();
                }

                if (id != null && id.GetType() != typeof(DBNull))
                {
                    empresa.ID = (int)id;

                    return empresa;
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

        public bool Edit(Empresa empresa)
        {
            try
            {
                int rows = 0;

                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $@"UPDATE {quote}Empresas{quote} SET
                                {quote}CNPJ{quote} = '{ empresa.CNPJ }'
                                , {quote}Nome{quote} = '{ empresa.Nome }'
                                , {quote}Cidade{quote} = '{ empresa.Cidade }'
                                , {quote}UF{quote} = '{ empresa.UF }'
                            WHERE {quote}ID{quote} = { empresa.ID };";

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
                        cmd.CommandText = $@"DELETE FROM {quote}Empresas{quote}
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