using CrossCutting;
using CrossCutting.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DAO.Extentions;

namespace DAO
{
    public class CommonDAO
    {
        protected static readonly string connString = AppSettings.ConnectionString;
        protected string table = string.Empty;

        public async Task<Pagination> GetPagination(int page, int take, Dictionary<string, string> filters)
        {
            try
            {
                Pagination pagination = new Pagination();

                using (var conn = new NpgsqlConnection(connString))
                {
                    await conn.OpenAsync();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $@"SELECT Count(*) FROM { table } 
                                            { DynamicWhere.BuildFilters(filters) };";

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                pagination.Count = reader.GetInt32(0);

                                if (pagination.Count == take)
                                {
                                    pagination.PageCount = 1;
                                }
                                else if (pagination.Count % take == 0)
                                {
                                    pagination.PageCount = pagination.Count / take;
                                }
                                else
                                {
                                    pagination.PageCount = (pagination.Count / take) + 1;
                                }

                                pagination.PageSize = take;
                                pagination.CurrentPage = page;

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

        public async Task<long> GetCount()
        {
            try
            {
                long count = 0;

                using (var conn = new NpgsqlConnection(connString))
                {
                    await conn.OpenAsync();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $@"SELECT Count(*) FROM { table };";

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
    }
}
