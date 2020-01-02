using System;
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
                for (int i = 0, n = this.Instance.FieldCount; i < n; ++i)
                    yield return this.Instance.GetName(i);
            }
        }
    }
}
