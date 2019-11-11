using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Blacksmith.Automap.Exceptions;

namespace Blacksmith.Automap.Services
{
    public class StrictMapBuilder : AbstractMapBuilder
    {
        protected override void processUnpairedTargetProperties(Type sourceType, Type targetType, IEnumerable<PropertyInfo> targetProperties)
        {
            if (targetProperties.Any())
            {
                throw new UnpairedMappingException(sourceType, targetType, targetProperties
                    , $"Some target properties of '{targetType.FullName}' could not be assigned.");
            }
        }

        protected override void processUnpairedSourceProperty(Type sourceType, Type targetType, PropertyInfo property)
        {
            throw new UnpairedMappingException(sourceType, targetType, new[] { property }
                , $"Property '{property.Name}' not found at '{targetType.FullName}' type.");
        }
    }
}
