using Blacksmith.Automap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Blacksmith.Automap.Services
{
    public class ObjectPropertyScanner : IPropertyScanner
    {
        public bool canScan(object item)
        {
            return true;
        }

        public IDictionary<string, IPropertyAccessor> getReadProperties(object source)
        {
            return prv_buildProperties(source, p => p.CanRead);
        }

        public IDictionary<string, IPropertyAccessor> getWriteProperties(object source)
        {
            return prv_buildProperties(source, p => p.CanRead && p.CanWrite);
        }

        private static Dictionary<string, IPropertyAccessor> prv_buildProperties(object source, Func<PropertyInfo, bool> filter)
        {
            return source
                .GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(filter)
                .Select(p => new PropertyInfoAccessor(p))
                .Cast<IPropertyAccessor>()
                .ToDictionary(p => p.Name);
        }

    }
}