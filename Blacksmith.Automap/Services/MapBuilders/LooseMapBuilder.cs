using System;
using System.Collections.Generic;
using System.Reflection;

namespace Blacksmith.Automap.Services.MapBuilders
{
    public class LooseMapBuilder : AbstractMapBuilder
    {
        protected override void processUnpairedSourceProperty(Type sourceType, Type targetType, PropertyInfo property)
        {
        }

        protected override void processUnpairedTargetProperties(Type sourceType, Type targetType, IEnumerable<PropertyInfo> targetProperties)
        {
        }
    }
}
