using System;
using System.Collections.Generic;

namespace CrossCutting
{
    public class DynamicWhere
    {
        const string quote = "\"";

        public static string BuildFilters(Dictionary<string, string> filters)
        {
            if (filters is null || filters.Count == 0)
            {
                return string.Empty;
            }

            List<String> conditions = new List<String>();

            foreach (var filter in filters)
            {
                if (filter.Key == "page" || filter.Key == "take")
                    continue;

                conditions.Add($"{ quote + filter.Key + quote } =  '{ filter.Value }'");
            }

            return conditions.Count > 0 
                ? " WHERE " + string.Join(" AND ", conditions) 
                : string.Empty;
        }
    }
}
