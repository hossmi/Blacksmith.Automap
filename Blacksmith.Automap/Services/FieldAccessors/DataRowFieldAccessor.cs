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
                foreach (DataColumn column in this.Instance.Table.Columns)
                    yield return column.ColumnName;
            }
        }
    }
}
