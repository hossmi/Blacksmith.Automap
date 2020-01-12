using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Blacksmith.Automap.Services.FieldAccessors
{
    public class ObjectFieldAccessor<T> : IFieldAccessor<T>
    {
        private readonly IDictionary<string, PropertyInfo> propertyMap;

        public ObjectFieldAccessor(T instance)
        {
            this.Instance = instance;
            this.Fields = instance
                .GetType()
                .GetProperties()
                .Where(p => p.CanRead && p.CanWrite)
                .Select(p => p.Name);

            this.propertyMap = instance
                .GetType()
                .GetProperties()
                .Where(p => p.CanRead && p.CanWrite)
                .ToDictionary(p => p.Name);
        }
        public object this[string name]
        {
            get => this.propertyMap[name].GetValue(this.Instance);
            set => this.propertyMap[name].SetValue(this.Instance, value);
        }

        public T Instance { get; }

        public IEnumerable<string> Fields { get; }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return prv_getEnumerator(this.Instance, this.propertyMap);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return prv_getEnumerator(this.Instance, this.propertyMap);
        }

        private static IEnumerator<KeyValuePair<string, object>> prv_getEnumerator(
            T instance, IDictionary<string, PropertyInfo> propertyMap)
        {
            foreach (KeyValuePair<string, PropertyInfo> property in propertyMap)
            {
                object value;

                value = property.Value.GetValue(instance);

                yield return new KeyValuePair<string, object>(property.Key, value);
            }
        }
    }
}
