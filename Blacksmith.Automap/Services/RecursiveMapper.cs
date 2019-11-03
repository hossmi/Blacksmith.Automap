using System;
using Blacksmith.Automap.Exceptions;
using Blacksmith.Automap.Models;

namespace Blacksmith.Automap.Services
{
    public class RecursiveMapper : IMapper
    {
        private readonly IMapRepository mapRepository;

        public RecursiveMapper(IMapRepository mapRepository)
        {
            this.mapRepository = mapRepository ?? throw new ArgumentNullException(nameof(mapRepository));
        }

        public void map(object source, object target)
        {
            prv_mapTo(source, target, this.mapRepository);
        }

        private static void prv_mapTo(object source, object target, IMapRepository mapRepository)
        {
            IMap map;

            map = mapRepository.getMap(source, target);

            foreach (PropertyMap propertyMap in map)
            {
                object value;
                bool needsRecursiveMap;

                value = propertyMap.SourceProperty.getValue(source);
                needsRecursiveMap = prv_needsRecursiveMap(propertyMap);

                if (needsRecursiveMap)
                {
                    object childTarget;

                    childTarget = Activator.CreateInstance(propertyMap.TargetProperty.Type);
                    prv_mapTo(value, childTarget, mapRepository);
                    propertyMap.TargetProperty.setValue(target, childTarget);
                }
                else
                {
                    try
                    {
                        propertyMap.TargetProperty.setValue(target, value);
                    }
                    catch (ArgumentException ex)
                    {
                        throw new MappingException(source.GetType(), target.GetType(), "Error assigning property value", ex);
                    }
                }
            }
        }

        private static bool prv_needsRecursiveMap(PropertyMap propertyMap)
        {
            bool sourceIsNoStringClass, targetIsNoStringClass, sourceIsCustomStruct, targetIsCustomStruct;
            bool sourceIsClassOrStruct, targetIsClassOrStruct;

            sourceIsNoStringClass = prv_isNoStringClass(propertyMap.SourceProperty.Type);
            targetIsNoStringClass = prv_isNoStringClass(propertyMap.TargetProperty.Type);
            sourceIsCustomStruct = prv_isCustomStruct(propertyMap.SourceProperty.Type);
            targetIsCustomStruct = prv_isCustomStruct(propertyMap.TargetProperty.Type);
            sourceIsClassOrStruct = sourceIsNoStringClass || sourceIsCustomStruct;
            targetIsClassOrStruct = targetIsNoStringClass || targetIsCustomStruct;

            return sourceIsClassOrStruct && targetIsClassOrStruct;
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
