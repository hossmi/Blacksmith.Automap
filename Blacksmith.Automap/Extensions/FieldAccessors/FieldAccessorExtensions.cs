using Blacksmith.Automap.Services;
using Blacksmith.Automap.Services.FieldAccessors;
using System;
using System.Collections.Generic;
using System.Data;

namespace Blacksmith.Automap.Extensions.FieldAccessors
{
    public static class FieldAccessorExtensions
    {
        public static IFieldAccessor<DataRow> access(this DataRow row)
        {
            return new DataRowFieldAccessor(row);
        }

        public static IFieldAccessor<IDictionary<string, object>> access(this IDictionary<string, object> dictionary)
        {
            return new DictionaryFieldAccessor(dictionary);
        }

        public static IReadOnlyFieldAccessor<IDataRecord> access(this IDataRecord record)
        {
            return new DataRecordFieldAccessor(record);
        }

        public static IDictionary<string, object> asDictionary(this IReadOnlyFieldAccessor accessor)
        {
            throw new NotImplementedException();
        }

        public static IDictionary<string, object> asRow(this IReadOnlyFieldAccessor accessor, Func<DataRow> newRowDelegate)
        {
            throw new NotImplementedException();
        }
    }
}
