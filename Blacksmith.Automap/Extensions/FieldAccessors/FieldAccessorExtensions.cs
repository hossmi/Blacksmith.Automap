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
            return accessor.ToDictionary(item => item.Key, item => item.Value);
        }

        public static DataRow toDataRow(this IReadOnlyFieldAccessor accessor, Func<DataRow> getNewRow)
        {
            DataRow row;

            row = getNewRow();

            foreach (var item in accessor)
                row[item.Key] = item.Value;

            return row;
        }

        public static T to<T>(this IReadOnlyFieldAccessor accessor) where T : new()
        {
            IFieldAccessor<T> resultAccessor;

            resultAccessor = new ObjectFieldAccessor<T>(new T());

            foreach (var item in accessor)
                resultAccessor[item.Key] = item.Value;

            return resultAccessor.Instance;
        }

        public static T to<T>(this IReadOnlyFieldAccessor accessor, Func<IReadOnlyFieldAccessor, T> build)
        {
            T instance;
            IFieldAccessor<T> resultAccessor;

            instance = build(accessor);
            resultAccessor = new ObjectFieldAccessor<T>(instance);

            foreach (var item in accessor)
                resultAccessor[item.Key] = item.Value;

            return instance;
        }
    }
}
