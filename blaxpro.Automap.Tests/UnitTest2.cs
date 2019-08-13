using blaxpro.Automap.Extensions;
using Xunit;

namespace blaxpro.Automap.Tests
{
    public class UnitTest2
    {
        public class First
        {
            public int IntProperty { get; set; }
            public string StringProperty { get; set; }
        }

        public class Second
        {
            public static string SomeStaticStringProperty { get; set; }
            public int Intproperty { get; set; }
            public string Stringproperty { get; set; }
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
            Assert.Equal("Some string", second.Stringproperty);
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
                Stringproperty = "aaa",
            };

            first.mapTo(second);

            Assert.Equal(34, second.Intproperty);
            Assert.Equal("Some string", second.Stringproperty);
        }
    }
}
