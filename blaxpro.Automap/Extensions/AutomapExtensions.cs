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
        private static IMapper mapper;

        static AutomapExtensions()
        {
            mapper = new DefaultMapper();
        }

        public static IMapper Mapper
        {
            get => mapper;
            set
            {
                mapper = value ?? throw new ArgumentNullException(nameof(Mapper));
            }
        }

        public static T mapTo<T>(this object item) where T : class, new()
        {
            return prv_mapTo<T>(item, new T());
        }

        public static T mapTo<T>(this object item, T targetItem) where T : class
        {
            return prv_mapTo<T>(item, targetItem);
        }

        private static T prv_mapTo<T>(object item, T target) where T : class
        {
            IMap map;
            Type sourceType, targetType;

            sourceType = item.GetType();
            targetType = typeof(T);
            map = Mapper.getMap(sourceType, targetType);

            foreach (PropertyMap propertyMap in map)
            {
                object value;

                value = propertyMap.SourceProperty.GetValue(item);
                propertyMap.TargetProperty.SetValue(target, value);
            }

            return target;
        }
    }
}
