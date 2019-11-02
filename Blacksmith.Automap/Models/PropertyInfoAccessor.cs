using System;
using System.Reflection;

namespace Blacksmith.Automap.Models
{
    public class PropertyInfoAccessor : IPropertyAccessor
    {
        private readonly PropertyInfo property;

        public PropertyInfoAccessor(PropertyInfo property)
        {
            this.property = property;
        }

        public Type PropertyType => this.property.PropertyType;

        public object getValue(object obj)
        {
            return this.property.GetValue(obj);
        }

        public void setValue(object obj, object value)
        {
            this.property.SetValue(obj, value);
        }
    }
}