using System;
using System.Collections.Generic;
using Blacksmith.Automap.Services;

namespace Blacksmith.Automap.Extensions
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

        public static TOut mapTo<TIn, TOut>(this TIn item, Func<TIn, TOut> newInstanceDelegate)
        {
            return prv_map(currentMapper, item, newInstanceDelegate(item));
        }

        public static TOut mapTo<TIn, TOut>(this TIn item, Func<TIn, TOut> newInstanceDelegate, IMapper mapper)
        {
            return prv_map(mapper, item, newInstanceDelegate(item));
        }

        public static IEnumerable<T> map<T>(this IEnumerable<object> items) where T : new()
        {
            foreach (object item in items)
                yield return prv_map<T>(currentMapper, item, new T());
        }

        public static IEnumerable<T> map<S, T>(this IEnumerable<S> items, Func<S, T> newInstanceDelegate) 
        {
            foreach (S item in items)
                yield return prv_map(currentMapper, item, newInstanceDelegate(item));
        }

        public static IEnumerable<T> map<T>(this IEnumerable<object> items, IMapper mapper) where T : new()
        {
            foreach (object item in items)
                yield return prv_map<T>(mapper, item, new T());
        }

        public static IEnumerable<T> map<S, T>(this IEnumerable<S> items, Func<S, T> newInstanceDelegate, IMapper mapper) where T : new()
        {
            foreach (S item in items)
                yield return prv_map(mapper, item, newInstanceDelegate(item));
        }

        private static T prv_map<T>(IMapper mapper, object source, T target)
        {
            mapper.map(source, target);
            return target;
        }

        private static T prv_map<S,T>(IMapper mapper, S source, T target)
        {
            mapper.map(source, target);
            return target;
        }

    }
}
