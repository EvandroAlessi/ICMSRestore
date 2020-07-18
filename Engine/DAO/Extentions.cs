using Npgsql;
using System;
using System.Collections.Generic;

namespace DAO
{
    public static class Extentions
    {
        public static NpgsqlParameter AddWithNullableValue(this NpgsqlParameterCollection collection, string parameterName, NpgsqlTypes.NpgsqlDbType type, object value)
        {
            return value is null 
                ? collection.AddWithValue(parameterName, type, DBNull.Value) 
                : collection.AddWithValue(parameterName, type, value);
        }

        public static void ChangeParameterValue(this NpgsqlParameterCollection collection, int index, object value)
        {
            collection[index].Value = value is null
                ? DBNull.Value
                : value;
        }
    }
}
