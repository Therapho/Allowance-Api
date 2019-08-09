using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace AllowanceFunctions.Common
{
    public static class HttpRequestExtensions
    {
        public static T? GetValueOrDefault<T>(this IQueryCollection query, string parameterName) where T: struct
        {
            if (!query.ContainsKey(parameterName) || string.IsNullOrEmpty(query[parameterName])) return null;
            return query.GetValue<T>(parameterName);
        }

        public static T GetValue<T>(this IQueryCollection query, string parameterName) 
        {
            string stringValue = query[parameterName];
            T convertedValue = (T)Convert.ChangeType(stringValue, typeof(T));
            return convertedValue;
        }
    }
}
