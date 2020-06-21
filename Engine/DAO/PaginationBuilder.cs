using CrossCutting;
using CrossCutting.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAO
{
    public class PaginationBuilder
    {
        static string connString = AppSettings.ConnectionString;
        const string quote = "\"";

        public async Task<Pagination> GetPagination(string table, int page, int take, Dictionary<string, string> filters)
        {
            try
            {
                Pagination pagination = new Pagination();

                using (var conn = new NpgsqlConnection(connString))
                {
                    await conn.OpenAsync();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $@"SELECT Count(*) FROM { quote + table + quote } 
                                            { DynamicWhere.BuildFilters(filters) };";

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                pagination.Count = reader.GetInt32(0);
                                pagination.PageCount = pagination.Count == take ? 1 : ((pagination.Count / take) + 1);
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
    }
}
