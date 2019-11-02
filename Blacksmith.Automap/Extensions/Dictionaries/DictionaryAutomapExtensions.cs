using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Blacksmith.Automap.Extensions.Dictionaries
{
    public static class DataRecordAutomapExtensions
    {
        public static T mapValuesTo<T>(this IDictionary<string, object> source) where T : new()
        {
            IEnumerable<PropertyInfo> properties;
            T result;
            
            result = new T();

            properties = result
                .GetType()
                .GetProperties()
                .Where(p => p.CanWrite && p.CanRead);

            foreach (var p in properties)
            {
                if (source.ContainsKey(p.Name))
                    p.SetValue(result, source[p.Name]);
            }

            return result;
        }

        public static IDictionary<string, object> toDictionary(object item)
        {
            throw new NotImplementedException();
        }
    }
}
