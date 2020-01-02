using System;
using System.Collections.Generic;
using Blacksmith.Automap.Services;
using Blacksmith.Automap.Services.MapBuilders;
using Blacksmith.Automap.Services.Mappers;
using Blacksmith.Automap.Services.MapRepositories;
using Blacksmith.Validations;

namespace Blacksmith.Automap.Extensions
{
    public static class AutomapExtensions
    {
        private static readonly IValidator assert;
        private static IMapper currentMapper;

        static AutomapExtensions()
        {
            assert = Asserts.Default;

            currentMapper = new RecursiveMapper(
                new StoragelessMapRepository(
                    new SourceCopyMapBuilder()
                    )
                );
        }

        [Obsolete]
        public static void setAsDefault(this IMapper mapper)
        {
            Mapper = mapper;
        }

        [Obsolete]
        public static void setAsDefault(this IMapRepository mapRepository)
        {
            currentMapper.Repository = mapRepository;
        }

        public static IMapper Mapper
        {
            get => currentMapper;
            set
            {
                assert.isNotNull(value);
                currentMapper = value;
            }
        }

        public static IMapRepository MapRepository
        {
            get => currentMapper.Repository;
            set
            {
                currentMapper.Repository = value;
            }
        }

        public static IMapBuilder MapBuilder
        {
            get => currentMapper.Repository.MapBuilder;
            set
            {
                currentMapper.Repository.MapBuilder = value;
            }
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

        private static T prv_map<S, T>(IMapper mapper, S source, T target)
        {
            mapper.map(source, target);
            return target;
        }

    }
}
