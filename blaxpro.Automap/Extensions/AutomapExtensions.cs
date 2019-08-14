using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using blaxpro.Automap.Exceptions;
using blaxpro.Automap.Models;
using blaxpro.Automap.Services;

namespace blaxpro.Automap.Extensions
{
    public static class AutomapExtensions
    {
        private static IMapRepository currentMapRepository;
        private static IMapper currentMapper;

        static AutomapExtensions()
        {
            currentMapRepository = new StoragelessMapRepository();
            currentMapper = new RecursiveMapper(currentMapRepository);
        }

        public static void setAsDefault(this IMapper mapper)
        {
            currentMapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public static void setAsDefault(this IMapRepository mapRepository)
        {
            currentMapRepository = mapRepository ?? throw new ArgumentNullException(nameof(mapRepository));
        }

        public static T mapTo<T>(this object item) where T : new()
        {
            return prv_map<T>(currentMapper, item, new T());
        }

        public static T mapTo<T>(this object item, T targetItem) 
        {
            return prv_map<T>(currentMapper, item, targetItem);
        }

        public static T mapTo<T>(this object item, IMapper mapper) where T : new()
        {
            return prv_map<T>(mapper, item, new T());
        }

        public static T mapTo<T>(this object item, T targetItem, IMapper mapper) 
        {
            return prv_map<T>(mapper, item, targetItem);
        }

        private static T prv_map<T>(IMapper mapper, object source, T target)
        {
            mapper.map(source, target);
            return target;
        }

    }
}
