using blaxpro.Automap.Extensions;
using Xunit;

namespace blaxpro.Automap.Tests
{
    public class UnitTest1
    {
        public class First
        {
            public int IntProperty { get; set; }
            public string StringProperty { get; set; }
        }

        public class Second
        {
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

            second = first.mapTo<Second>();

            Assert.Equal(34, second.Intproperty);
            Assert.Equal("Some string", second.StringProperty);
            Assert.Null(second.Stringproperty);
            Assert.Null(second.DoubleNullableProperty);
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

            first.mapTo(second);

            Assert.Equal(34, second.Intproperty);
            Assert.Equal("Some string", second.StringProperty);
            Assert.Equal("untouched", second.Stringproperty);
            Assert.Equal(3.1415m, second.DoubleNullableProperty);
        }
    }
}
