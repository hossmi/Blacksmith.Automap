using System;
using blaxpro.Automap.Models;

namespace blaxpro.Automap.Services
{
    public interface IMapRepository
    {
        IMap getMap(Type sourceType, Type targetType);
    }
}
