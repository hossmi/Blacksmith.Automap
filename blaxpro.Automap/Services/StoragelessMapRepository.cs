using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using blaxpro.Automap.Exceptions;
using blaxpro.Automap.Models;

namespace blaxpro.Automap.Services
{
    public class StoragelessMapRepository : IMapRepository
    {
        public IMap getMap(Type sourceType, Type targetType)
        {
            IEnumerable<PropertyMap> propertyMaps;

            propertyMaps = prv_getPropertyMaps(sourceType, targetType);

            return new Map(propertyMaps);
        }

        private static IEnumerable<PropertyMap> prv_getPropertyMaps(Type sourceType, Type targetType)
        {
            IDictionary<string, PropertyInfo> targetProperties, sourceProperties;

            sourceProperties = sourceType
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => p.CanRead)
                .ToDictionary(property => property.Name);

            targetProperties = targetType
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => p.CanRead && p.CanWrite)
                .ToDictionary(property => property.Name);

            foreach (var source in sourceProperties)
            {
                PropertyInfo sourceProperty, targetProperty;

                sourceProperty = source.Value;

                if (prv_match(source.Value, targetProperties, out targetProperty))
                {
                    yield return new PropertyMap
                    {
                        SourceProperty = sourceProperty,
                        TargetProperty = targetProperty,
                    };
                }
                else if (prv_ignoreCaseMatch(source.Value, targetProperties, out targetProperty))
                {
                    yield return new PropertyMap
                    {
                        SourceProperty = sourceProperty,
                        TargetProperty = targetProperty,
                    };
                }
                else
                {
                    throw new MappingException(sourceType, targetType, $"Property '{source.Key}' not found at '{targetType.FullName}' type.");
                }
            }

            if (targetProperties.Any())
                throw new MappingException(sourceType, targetType, $"Some target properties not assigned '{targetType.FullName}'.");
        }

        private static bool prv_match(PropertyInfo sourceProperty, IDictionary<string, PropertyInfo> targetProperties, out PropertyInfo targetProperty)
        {
            if (targetProperties.ContainsKey(sourceProperty.Name))
            {
                Type sourceType, targetType;

                sourceType = sourceProperty.DeclaringType;
                targetProperty = targetProperties[sourceProperty.Name];
                targetType = targetProperty.DeclaringType;
                targetProperties.Remove(sourceProperty.Name);
                return true;
            }
            else
            {
                targetProperty = null;
                return false;
            }
        }

        private static bool prv_ignoreCaseMatch(PropertyInfo sourceProperty, IDictionary<string, PropertyInfo> targetProperties, out PropertyInfo targetProperty)
        {
            int matches;
            StringComparer stringComparer;
            Type sourceType;

            sourceType = sourceProperty.DeclaringType;
            stringComparer = StringComparer.InvariantCultureIgnoreCase;
            matches = 0;
            targetProperty = targetProperties
                .Values
                .Where(property =>
                {
                    if (stringComparer.Compare(sourceProperty.Name, property.Name) == 0)
                    {
                        matches++;

                        if (matches > 1)
                            throw new MappingException(sourceType, property.DeclaringType, $"Multiple matches for '{sourceProperty.Name}' property.");

                        return true;
                    }

                    return false;
                })
                .FirstOrDefault();

            if (targetProperty != null)
            {
                targetProperties.Remove(targetProperty.Name);
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
