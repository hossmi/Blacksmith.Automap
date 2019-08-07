using System;
using System.Collections.Generic;
using System.Text;

namespace blaxpro.Automap.Extensions
{
    public static class AutomapExtensions
    {
        public static T mapTo<T>(this object item) where T: class, new()
        {
            throw new NotImplementedException();
        }

        public static T mapTo<T>(this object item, T targetItem) where T : class
        {
            throw new NotImplementedException();
        }

        public static T mapToOrDefault<T>(this object item) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public static T mapToOrDefault<T>(this object item, T targetItem) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
