using System;
using System.Collections.Generic;
using Blacksmith.Automap.Models;

namespace Blacksmith.Automap.Services
{
    public interface IMapBuilder
    {
        IEnumerable<PropertyMap> build(Type sourceType, Type targetType);
    }
}
