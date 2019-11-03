using System;

namespace Blacksmith.Automap.Models
{
    public interface IPropertyAccessor
    {
        string Name { get; }
        object getValue(object obj);
        void setValue(object obj, object value);
        Type Type { get; }
        Type ObjectType { get; }
    }
}