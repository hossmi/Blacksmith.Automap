using Blacksmith.Automap.Exceptions;
using Blacksmith.Automap.Extensions;
using Blacksmith.Automap.Services;
using Blacksmith.Automap.Services.MapBuilders;
using Blacksmith.Automap.Services.Mappers;
using Blacksmith.Automap.Services.MapRepositories;
using Xunit;

namespace Blacksmith.Automap.Tests
{
    public class MissingTargetPropertiesTests
    {
        public class First
        {
            public static string SomeStaticStringProperty { get; set; }
            public int IntProperty { get; set; }
            public string StringProperty { get; set; }
            public decimal Amount { get; set; }
        }

        public class Second
        {
            public static decimal? SomeStaticDoubleNullableProperty { get; set; }
            public int Intproperty { get; set; }
            public string StringProperty { get; set; }
            public string Stringproperty { get; set; }
            public decimal? DoubleNullableProperty { get; set; }
            public decimal Amount { get; set; }
        }

        [Fact]
        public void map_fails_to_new_instance_because_of_missing_target_properties()
        {
            First first;
            Second second;
            IMapper mapper;

            mapper = new RecursiveMapper(new StoragelessMapRepository(new StrictMapBuilder()));

            first = new First
            {
                IntProperty = 34,
                StringProperty = "Some string",
            };

            Assert.Throws<UnpairedMappingException>(() => second = first.mapTo<Second>(mapper));
        }

        [Fact]
        public void map_fails_to_existing_instance_because_of_missing_target_properties()
        {
            First first;
            Second second;
            IMapper mapper;

            mapper = new RecursiveMapper(new StoragelessMapRepository(new StrictMapBuilder()));

            first = new First
            {
                IntProperty = 34,
                StringProperty = "Some string",
            };

            second = new Second
            {
                Intproperty = 666,
                StringProperty = "aaa",
                Stringproperty = "untouched",
                DoubleNullableProperty = 3.1415m
            };

            Assert.Throws<UnpairedMappingException>(() => first.mapTo(second, mapper));

        }

        [Fact]
        public void loose_map_tests()
        {
            IMapper mapper;
            First first;
            Second second;

            mapper = new RecursiveMapper(new StoragelessMapRepository(new SourceCopyMapBuilder()));

            first = new First
            {
                IntProperty = 34,
                StringProperty = "Some string",
            };

            second = new Second
            {
                Intproperty = 666,
                StringProperty = "aaa",
                Stringproperty = "untouched",
                DoubleNullableProperty = 3.1415m,
                Amount = 89.34m,
            };

            Assert.Throws<UnpairedMappingException>(() => second.mapTo(first, mapper));

            mapper.Repository.MapBuilder = new LooseMapBuilder();
            second.mapTo(first, mapper);

            Assert.Equal(666, first.IntProperty);
            Assert.Equal("aaa", first.StringProperty);
            Assert.Equal(89.34m, first.Amount);
        }
    }
}
