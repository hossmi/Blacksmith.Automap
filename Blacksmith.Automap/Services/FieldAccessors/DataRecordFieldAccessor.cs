using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace Blacksmith.Automap.Services.FieldAccessors
{
    public class DataRecordFieldAccessor : IReadOnlyFieldAccessor<IDataRecord>
    {
        public DataRecordFieldAccessor(IDataRecord record)
        {
            this.Instance = record;
        }

        public object this[string name]
        {
            get => this.Instance[name];
        }

        public IDataRecord Instance { get; }

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

        private static IEnumerator<KeyValuePair<string, object>> prv_getEnumerator(IDataRecord instance)
        {
            IEnumerable<string> fields;

            fields = prv_getFields(instance);

            foreach (string field in fields)
                yield return new KeyValuePair<string, object>(field, instance[field]);
        }

        private static IEnumerable<string> prv_getFields(IDataRecord instance)
        {
            for (int i = 0, n = instance.FieldCount; i < n; ++i)
                yield return instance.GetName(i);
        }

    }
}
