using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Blacksmith.Automap.Exceptions;

namespace Blacksmith.Automap.Services
{
    public class StrictMapBuilder : AbstractMapBuilder
    {
        protected override void processUnassignedTargetProperties(Type sourceType, Type targetType, IDictionary<string, PropertyInfo> targetProperties)
        {
            if (targetProperties.Any())
            {
                throw new MappingException(sourceType, targetType, $"Some target properties of '{targetType.FullName}' could not be assigned.");
            }
        }
    }
}
