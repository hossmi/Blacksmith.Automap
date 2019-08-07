using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using blaxpro.Automap.Exceptions;

namespace blaxpro.Automap.Extensions
{
    public static class AutomapExtensions
    {
        public static T mapTo<T>(this object item) where T : class, new()
        {
            return prv_mapTo<T>(item, new T());
        }

        public static T mapTo<T>(this object item, T targetItem) where T : class
        {
            return prv_mapTo<T>(item, targetItem);
        }

        public static T mapToOrDefault<T>(this object item) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public static T mapToOrDefault<T>(this object item, T targetItem) where T : class
        {
            throw new NotImplementedException();
        }

        private static T prv_mapTo<T>(object item, T target) where T : class
        {
            IDictionary<string, PropertyInfo> targetProperties, sourceProperties;
            ICollection<Action<object, T>> assignementCommands;
            Type sourceType, targetType;

            sourceType = item.GetType();
            targetType = typeof(T);

            sourceProperties = sourceType
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => p.CanRead)
                .ToDictionary(property => property.Name);

            targetProperties = targetType
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => p.CanRead && p.CanWrite)
                .ToDictionary(property => property.Name);

            assignementCommands = new List<Action<object, T>>();

            foreach (var source in sourceProperties)
            {
                PropertyInfo sourceProperty, targetProperty;

                sourceProperty = source.Value;

                if (prv_match(sourceType, source.Value, targetType, targetProperties, out targetProperty))
                {
                    assignementCommands.Add((sourceItem, targetItem) =>
                    {
                        object value;

                        value = sourceProperty.GetValue(sourceItem);
                        targetProperty.SetValue(targetItem, value);
                    });
                }
                else if (prv_ignoreCaseMatch(sourceType, source.Value, targetType, targetProperties, out targetProperty))
                {
                    assignementCommands.Add((sourceItem, targetItem) =>
                    {
                        object value;

                        value = sourceProperty.GetValue(sourceItem);
                        targetProperty.SetValue(targetItem, value);
                    });
                }
                else
                {
                    throw new MappingException(sourceType, targetType, $"Property '{source.Key}' not found at '{typeof(T).FullName}' type.");
                }
            }

            foreach (Action<object, T> command in assignementCommands)
                command(item, target);

            return target;
        }

        private static bool prv_match(Type sourceType, PropertyInfo sourceProperty, Type targetType, IDictionary<string, PropertyInfo> targetProperties, out PropertyInfo targetProperty)
        {
            if (targetProperties.ContainsKey(sourceProperty.Name))
            {
                targetProperty = targetProperties[sourceProperty.Name];

                if (targetProperty.PropertyType != sourceProperty.PropertyType)
                    throw new MappingException(sourceType, targetType, $@"Type missmatch between properties:
- Source: {sourceType.FullName}.{sourceProperty.Name} ({sourceProperty.PropertyType.FullName})
- Target: {targetType.FullName}.{targetProperty.Name} ({targetProperty.PropertyType.FullName})
");

                return true;
            }
            else
            {
                targetProperty = null;
                return false;
            }
        }

        private static bool prv_ignoreCaseMatch(Type sourceType, PropertyInfo sourceProperty, Type targetType, IDictionary<string, PropertyInfo> targetProperties, out PropertyInfo targetProperty)
        {
            string targetPropertyName;
            int matches;
            StringComparer stringComparer;

            stringComparer = StringComparer.InvariantCultureIgnoreCase;
            matches = 0;
            targetPropertyName = targetProperties
                .Keys
                .Where(propertyName =>
                {
                    if(stringComparer.Compare(sourceProperty.Name, propertyName) == 0)
                    {
                        matches++;

                        if (matches > 1)
                            throw new MappingException(sourceType, targetType, $"Multiple matches for '{sourceProperty.Name}' property.");

                        return true;
                    }

                    return false;
                })
                .FirstOrDefault();

            if(targetPropertyName != null)
            {
                targetProperty = targetProperties[targetPropertyName];

                if (targetProperty.PropertyType != sourceProperty.PropertyType)
                    throw new MappingException(sourceType, targetType, $@"Type missmatch between properties:
- Source: {sourceType.FullName}.{sourceProperty.Name} ({sourceProperty.PropertyType.FullName})
- Target: {targetType.FullName}.{targetProperty.Name} ({targetProperty.PropertyType.FullName})
");

                return true;
            }
            else
            {
                targetProperty = null;
                return false;
            }
        }
    }
}
