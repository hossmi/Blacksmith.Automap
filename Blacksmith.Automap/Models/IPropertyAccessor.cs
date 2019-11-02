using System;

namespace Blacksmith.Automap.Models
{
    public interface IPropertyAccessor
    {
        object getValue(object obj);
        void setValue(object obj, object value);
        Type PropertyType { get; }
    }
}