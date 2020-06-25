using CrossCutting;
using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class MetricsDAO
    {
        //private static readonly List<string> tables = new List<string>() { "Empresas", "Processos", "NFe" };
        //private static readonly string connString = AppSettings.ConnectionString;
        //private const string quote = "\"";

        //public async Task<dynamic> GetCounts()
        //{
        //    try
        //    {
        //        dynamic counts = new
        //        {
        //            Companies = 0,
        //            Processes = 0,
        //            FinishedProcesses = 0,
        //            Invoices = 0,
        //        };

        //        using (var conn = new NpgsqlConnection(connString))
        //        {
        //            await conn.OpenAsync();

        //            foreach (var table in tables)
        //            {
        //                using (var cmd = conn.CreateCommand())
        //                {
        //                    cmd.CommandText = $@"SELECT Count(*) FROM { table };";

        //                    using (var reader = cmd.ExecuteReader())
        //                    {
        //                        while (reader.Read())
        //                        {
        //                            counts.Companies = reader.GetInt32(0);

        //                            break;
        //                        }
        //                    }
        //                }
        //            }
                   
        //            await conn.CloseAsync();
        //        }

        //        return counts;
        //    }
        //    catch (NpgsqlException ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}
