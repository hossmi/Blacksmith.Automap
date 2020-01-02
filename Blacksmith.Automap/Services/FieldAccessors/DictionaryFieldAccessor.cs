using System.Collections.Generic;

namespace Blacksmith.Automap.Services.FieldAccessors
{
    public class DictionaryFieldAccessor : IFieldAccessor<IDictionary<string, object>>
    {
        public DictionaryFieldAccessor(IDictionary<string, object> dictionary)
        {
            this.Instance = dictionary 
                ?? throw new System.ArgumentNullException(nameof(dictionary));
        }

        public object this[string name]
        {
            get => this.Instance[name];
            set => this.Instance[name] = value;
        }

        public IEnumerable<string> Fields => this.Instance.Keys;

        public IDictionary<string, object> Instance { get; }
    }
}
