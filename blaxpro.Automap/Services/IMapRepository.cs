using System;
using Blaxpro.Automap.Models;

namespace Blaxpro.Automap.Services
{
    public interface IMapRepository
    {
        IMap getMap(Type sourceType, Type targetType);
    }
}
