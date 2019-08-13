using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using blaxpro.Automap.Exceptions;
using blaxpro.Automap.Models;
using blaxpro.Automap.Services;

namespace blaxpro.Automap.Extensions
{
    public static class AutomapExtensions
    {
        private static IMapper currentMapper;

        static AutomapExtensions()
        {
            currentMapper = new DefaultMapper();
        }

        public static IMapper Mapper
        {
            get => currentMapper;
            set
            {
                currentMapper = value ?? throw new ArgumentNullException(nameof(Mapper));
            }
        }

        public static T mapTo<T>(this object item) where T : class, new()
        {
            return (T)prv_mapTo(item, new T(), currentMapper);
        }

        public static T mapTo<T>(this object item, T targetItem) where T : class
        {
            return (T)prv_mapTo(item, targetItem, currentMapper);
        }

        private static object prv_mapTo(object item, object target, IMapper mapper)
        {
            IMap map;
            Type sourceType, targetType;

            sourceType = item.GetType();
            targetType = target.GetType();
            map = mapper.getMap(sourceType, targetType);

            foreach (PropertyMap propertyMap in map)
            {
                object value;
                bool needsRecursiveMap;

                value = propertyMap.SourceProperty.GetValue(item);
                needsRecursiveMap = prv_needsRecursiveMap(propertyMap);

                if (needsRecursiveMap)
                {
                    object childTarget;

                    childTarget = Activator.CreateInstance(propertyMap.TargetProperty.PropertyType);
                    prv_mapTo(value, childTarget, mapper);
                    propertyMap.TargetProperty.SetValue(target, childTarget);
                }
                else
                {
                    try
                    {
                        propertyMap.TargetProperty.SetValue(target, value);
                    }
                    catch (ArgumentException ex)
                    {
                        throw new MappingException(sourceType, targetType, "Error assigning property value", ex);
                    }
                }
            }

            return target;
        }

        private static bool prv_needsRecursiveMap(PropertyMap propertyMap)
        {
            bool sourceIsNoStringClass, targetIsNoStringClass, sourceIsCustomStruct, targetIsCustomStruct;

            sourceIsNoStringClass = prv_isNoStringClass(propertyMap.SourceProperty.PropertyType);
            targetIsNoStringClass = prv_isNoStringClass(propertyMap.TargetProperty.PropertyType);
            sourceIsCustomStruct = prv_isCustomStruct(propertyMap.SourceProperty.PropertyType);
            targetIsCustomStruct = prv_isCustomStruct(propertyMap.TargetProperty.PropertyType);

            return sourceIsCustomStruct && targetIsCustomStruct
                || sourceIsNoStringClass && targetIsNoStringClass;
        }

        private static bool prv_isNoStringClass(Type type)
        {
            return type.IsClass && type != typeof(string);
        }

        private static bool prv_isCustomStruct(Type type)
        {
            return type.IsValueType && !type.IsEnum && !type.IsPrimitive;
        }
    }
}
