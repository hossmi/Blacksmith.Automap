using System.Collections.Generic;

namespace Blacksmith.Automap.Services.FieldAccessors
{
    public interface IReadOnlyFieldAccessor
    {
        IEnumerable<string> Fields { get; }
        object this[string name] { get; }

    }
    public interface IReadOnlyFieldAccessor<T> : IReadOnlyFieldAccessor
        where T : class
    {
        T Instance { get; }
    }

    public interface IFieldAccessor : IReadOnlyFieldAccessor
    {
        new object this[string name] { get; set; }
    }
    public interface IFieldAccessor<T> : IFieldAccessor, IReadOnlyFieldAccessor<T>
        where T : class
    {
    }
}
