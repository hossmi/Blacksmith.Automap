using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace Blacksmith.Automap.Services.FieldAccessors
{
    public class DataRowFieldAccessor : IFieldAccessor<DataRow>
    {
        public DataRowFieldAccessor(DataRow dataRow)
        {
            this.Instance = dataRow;
        }

        public object this[string name]
        {
            get => this.Instance[name];
            set => this.Instance[name] = value;
        }

        public DataRow Instance { get; }

        public IEnumerable<string> Fields
        {
            get
            {
                return prv_getFields(this.Instance);
            }
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return prv_getEnumerator(this.Instance);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return prv_getEnumerator(this.Instance);
        }

        private static IEnumerable<string> prv_getFields(DataRow instance)
        {
            foreach (DataColumn column in instance.Table.Columns)
                yield return column.ColumnName;
        }

        private static IEnumerator<KeyValuePair<string, object>> prv_getEnumerator(DataRow instance)
        {
            IEnumerable<string> fields;

            fields = prv_getFields(instance);

            foreach (string field in fields)
                yield return new KeyValuePair<string, object>(field, instance[field]);
        }
    }
}
