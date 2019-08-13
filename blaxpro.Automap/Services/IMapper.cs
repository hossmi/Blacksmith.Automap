using System;
using blaxpro.Automap.Models;

namespace blaxpro.Automap.Services
{
    public interface IMapper
    {
        IMap getMap(Type sourceType, Type targetType);
    }
}
