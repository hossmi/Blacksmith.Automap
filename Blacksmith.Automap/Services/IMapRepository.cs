using System;
using Blacksmith.Automap.Models;

namespace Blacksmith.Automap.Services
{
    public interface IMapRepository
    {
        IMap getMap(Type sourceType, Type targetType);
    }
}
