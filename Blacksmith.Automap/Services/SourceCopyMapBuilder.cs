using System;
using System.Collections.Generic;
using System.Reflection;

namespace Blacksmith.Automap.Services
{
    public class SourceCopyMapBuilder : AbstractMapBuilder
    {
        protected override void processUnassignedTargetProperties(Type sourceType, Type targetType, IDictionary<string, PropertyInfo> targetProperties)
        {
        }
    }
}
