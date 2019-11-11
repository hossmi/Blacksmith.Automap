using Blacksmith.Automap.Exceptions;
using Blacksmith.Automap.Extensions;
using Blacksmith.Automap.Services;
using Xunit;

namespace Blacksmith.Automap.Tests
{
    public class MissingTargetPropertiesTests
    {
        private readonly IMapper mapper;

        public class First
        {
            public static string SomeStaticStringProperty { get; set; }
            public int IntProperty { get; set; }
            public string StringProperty { get; set; }
        }

        public class Second
        {
            public static decimal? SomeStaticDoubleNullableProperty { get; set; }
            public int Intproperty { get; set; }
            public string StringProperty { get; set; }
            public string Stringproperty { get; set; }
            public decimal? DoubleNullableProperty { get; set; }
        }

        public MissingTargetPropertiesTests()
        {
            this.mapper = new RecursiveMapper(new StoragelessMapRepository(new StrictMapBuilder()));
        }

        [Fact]
        public void map_fails_to_new_instance_because_of_missing_target_properties()
        {
            First first;
            Second second;

            first = new First
            {
                IntProperty = 34,
                StringProperty = "Some string",
            };

            

            Assert.Throws<MappingException>(() => second = first.mapTo<Second>(this.mapper));
        }

        [Fact]
        public void map_fails_to_existing_instance_because_of_missing_target_properties()
        {
            First first;
            Second second;

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

            Assert.Throws<MappingException>(() => first.mapTo(second, this.mapper));

        }
    }
}
