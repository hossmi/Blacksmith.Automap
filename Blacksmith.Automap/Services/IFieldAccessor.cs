using System.Collections.Generic;

namespace Blacksmith.Automap.Services
{
    public interface IReadOnlyFieldAccessor : IEnumerable<KeyValuePair<string, object>>
    {
        IEnumerable<string> Fields { get; }
        object this[string name] { get; }
        //TField get<TField>(string name);
        //TField get<TField>(string name, TField defaultValue);
    }

    public interface IReadOnlyFieldAccessor<T> : IReadOnlyFieldAccessor
    {
        T Instance { get; }
    }

    public interface IFieldAccessor : IReadOnlyFieldAccessor
    {
        new object this[string name] { get; set; }
        //TField set<TField>(string name, TField value);
    }

    public interface IFieldAccessor<T> : IFieldAccessor, IReadOnlyFieldAccessor<T>
    {
    }
}
