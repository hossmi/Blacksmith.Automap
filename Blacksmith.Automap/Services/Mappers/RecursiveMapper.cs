using System;
using System.Linq;
using Blacksmith.Automap.Exceptions;
using Blacksmith.Automap.Models;
using Blacksmith.Validations;

namespace Blacksmith.Automap.Services.Mappers
{
    public class RecursiveMapper : IMapper
    {
        private readonly IValidator assert;
        private IMapRepository mapRepository;


        public RecursiveMapper(IMapRepository mapRepository)
        {
            this.assert = Asserts.Default;
            this.Repository = mapRepository;
        }

        public IMapRepository Repository
        {
            get => this.mapRepository;
            set
            {
                this.assert.isNotNull(value);
                this.mapRepository = value;
            }
        }

        public void map(object source, object target)
        {
            prv_mapTo(source, target, this.mapRepository);
        }

        private static void prv_mapTo(object source, object target, IMapRepository mapRepository)
        {
            IMap map;
            Type sourceType, targetType;

            sourceType = source.GetType();
            targetType = target.GetType();
            map = mapRepository.getMap(sourceType, targetType);

            foreach (PropertyMap propertyMap in map)
            {
                object value;
                bool needsRecursiveMap;

                value = propertyMap.SourceProperty.GetValue(source);
                needsRecursiveMap = prv_needsRecursiveMap(propertyMap);

                if (needsRecursiveMap)
                {
                    object childTarget;

                    childTarget = Activator.CreateInstance(propertyMap.TargetProperty.PropertyType);
                    prv_mapTo(value, childTarget, mapRepository);
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
        }

        private static bool prv_needsRecursiveMap(PropertyMap propertyMap)
        {
            bool sourceHasReadWriteProperties, targetHasReadWriteProperties;

            sourceHasReadWriteProperties = propertyMap.SourceProperty
                .PropertyType
                .GetProperties()
                .Any(p => p.CanRead && p.CanWrite);

            targetHasReadWriteProperties = propertyMap.TargetProperty
                .PropertyType
                .GetProperties()
                .Any(p => p.CanRead && p.CanWrite);

            return sourceHasReadWriteProperties && targetHasReadWriteProperties;
        }
    }
}
