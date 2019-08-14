using System;
using blaxpro.Automap.Exceptions;
using blaxpro.Automap.Extensions;
using Xunit;

namespace blaxpro.Automap.Tests
{
    public class UnitTest1
    {
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

        [Fact(DisplayName = "Map to new instance")]
        public void map_to_new_instance()
        {
            First first;
            Second second;

            first = new First
            {
                IntProperty = 34,
                StringProperty = "Some string",
            };

            Assert.Throws<MappingException>(() => second = first.mapTo<Second>());
        }

        [Fact(DisplayName = "Map to existing instance")]
        public void map_to_existing_instance()
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

            Assert.Throws<MappingException>(() => first.mapTo(second));

        }
    }
}
