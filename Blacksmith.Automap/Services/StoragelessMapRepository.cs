using System;
using System.Collections.Generic;
using System.Linq;
using Blacksmith.Automap.Exceptions;
using Blacksmith.Automap.Models;

namespace Blacksmith.Automap.Services
{
    public class StoragelessMapRepository : IMapRepository
    {
        private readonly IEnumerable<IPropertyScanner> propertyScanners;

        public StoragelessMapRepository(IEnumerable<IPropertyScanner> propertyScanners)
        {
            this.propertyScanners = propertyScanners
                .Concat(new[] { new PrvFailPropertyScanner() });
        }

        public IMap getMap(object source, object target)
        {
            IEnumerable<PropertyMap> propertyMaps;

            propertyMaps = prv_getPropertyMaps(this.propertyScanners, source, target);

            return new Map(propertyMaps);
        }

        private static IEnumerable<PropertyMap> prv_getPropertyMaps(
            IEnumerable<IPropertyScanner> propertyScanners, object source, object target)
        {
            IEnumerable<IPropertyAccessor> sourceProperties;
            IDictionary<string, IPropertyAccessor> targetProperties;

            sourceProperties = propertyScanners
                .First(o => o.canScan(source))
                .getReadProperties(source)
                .Values;

            targetProperties = propertyScanners
                .First(o => o.canScan(target))
                .getWriteProperties(source);

            foreach (IPropertyAccessor sourceProperty in sourceProperties)
            {
                IPropertyAccessor targetProperty;

                if (prv_match(sourceProperty, targetProperties, out targetProperty))
                {
                    yield return new PropertyMap
                    {
                        SourceProperty = sourceProperty,
                        TargetProperty = targetProperty,
                    };
                }
                else if (prv_ignoreCaseMatch(sourceProperty, targetProperties, out targetProperty))
                {
                    yield return new PropertyMap
                    {
                        SourceProperty = sourceProperty,
                        TargetProperty = targetProperty,
                    };
                }
                else
                {
                    throw new MappingException(source.GetType(), target.GetType(), $"Property '{sourceProperty.Name}' not found at '{target.GetType().FullName}' type.");
                }
            }

            if (targetProperties.Any())
                throw new MappingException(source.GetType(), target.GetType(), $"Some target properties of '{target.GetType().FullName}' could not be assigned.");
        }

        private static bool prv_match(
            IPropertyAccessor sourceProperty
            , IDictionary<string, IPropertyAccessor> targetProperties
            , out IPropertyAccessor targetProperty)
        {
            if (targetProperties.ContainsKey(sourceProperty.Name))
            {
                targetProperty = targetProperties[sourceProperty.Name];
                targetProperties.Remove(sourceProperty.Name);
                return true;
            }
            else
            {
                targetProperty = null;
                return false;
            }
        }

        private static bool prv_ignoreCaseMatch(
            IPropertyAccessor sourceProperty
            , IDictionary<string, IPropertyAccessor> targetProperties
            , out IPropertyAccessor targetProperty)
        {
            int matches;
            StringComparer stringComparer;
            Type sourceType;

            sourceType = sourceProperty.ObjectType;
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
                            throw new MappingException(sourceType, property.ObjectType, $"Multiple matches for '{sourceProperty.Name}' property.");

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

        private class PrvFailPropertyScanner : IPropertyScanner
        {
            public bool canScan(object item)
            {
                return true;
            }

            public IDictionary<string, IPropertyAccessor> getReadProperties(object source)
            {
                throw new PropertyScannerException(source.GetType(), $"No property scanner could read object of type '{source.GetType()}'.");
            }

            public IDictionary<string, IPropertyAccessor> getWriteProperties(object source)
            {
                throw new PropertyScannerException(source.GetType(), $"No property scanner could read object of type '{source.GetType()}'.");
            }
        }
    }
}
