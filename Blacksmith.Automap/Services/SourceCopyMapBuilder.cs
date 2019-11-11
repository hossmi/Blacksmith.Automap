using Blacksmith.Automap.Exceptions;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Blacksmith.Automap.Services
{
    public class SourceCopyMapBuilder : AbstractMapBuilder
    {
        protected override void processUnpairedSourceProperty(Type sourceType, Type targetType, PropertyInfo property)
        {
            throw new UnpairedMappingException(sourceType, targetType, new[] { property }
                , $"Property '{property.Name}' not found at '{targetType.FullName}' type.");
        }

        protected override void processUnpairedTargetProperties(Type sourceType, Type targetType, IEnumerable<PropertyInfo> targetProperties)
        {
        }
    }
}
