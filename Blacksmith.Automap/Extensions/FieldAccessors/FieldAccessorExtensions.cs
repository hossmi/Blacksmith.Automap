using Blacksmith.Automap.Services;
using Blacksmith.Automap.Services.FieldAccessors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Blacksmith.Automap.Extensions.FieldAccessors
{
    public static class FieldAccessorExtensions
    {
        public static IFieldAccessor<DataRow> convert(this DataRow row)
        {
            return new DataRowFieldAccessor(row);
        }

        public static IFieldAccessor<IDictionary<string, object>> convert(this IDictionary<string, object> dictionary)
        {
            return new DictionaryFieldAccessor(dictionary);
        }

        public static IReadOnlyFieldAccessor<IDataRecord> convert(this IDataRecord record)
        {
            return new DataRecordFieldAccessor(record);
        }

        public static IFieldAccessor<T> convert<T>(this T item)
        {
            return new ObjectFieldAccessor<T>(item);
        }

        public static IDictionary<string, object> toDictionary(this IReadOnlyFieldAccessor accessor)
        {
            throw new NotImplementedException();
        }

        public static IDictionary<string, object> toDataRow(this IReadOnlyFieldAccessor accessor, Func<DataRow> newRowDelegate)
        {
            throw new NotImplementedException();
        }

        public static T to<T>(this IReadOnlyFieldAccessor accessor) where T : new()
        {
            throw new NotImplementedException();
        }

        public static T to<T>(this IReadOnlyFieldAccessor accessor, Func<IReadOnlyFieldAccessor<T>, T> build) where T : new()
        {
            throw new NotImplementedException();
        }
    }
}
