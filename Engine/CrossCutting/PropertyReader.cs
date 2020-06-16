using System;

namespace CrossCutting
{
    public class PropertyReader
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string GetProperties<TEntity>(TEntity entity)
        {
            string aux = string.Empty;

            if (!typeof(TEntity).IsPrimitive && !(entity is String || entity is string))
            {
                var properties = typeof(TEntity).GetProperties();

                foreach (var prop in properties)
                {
                    aux += $"{ prop.Name} = { prop.GetValue(entity) }; ";
                }
            }
            else
            {
                aux = $"Path = { entity }; ";
            }

            return aux;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static string GetAllParameters<TEntity>(TEntity parameter)
        {
            string parameterString = "";

            try
            {
                var propriedades = parameter.GetType().GetProperties();

                foreach (var prop in propriedades)
                {
                    parameterString += $"{ prop.Name} = { prop.GetValue(parameter) }; ";
                }
            }
            catch { }

            return parameterString;
        }

    }
}
