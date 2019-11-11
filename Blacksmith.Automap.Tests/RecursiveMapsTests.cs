using Blacksmith.Automap.Extensions;
using Blacksmith.Automap.Tests.Models.Data;
using Blacksmith.Automap.Tests.Models.Domain;
using Xunit;

namespace Blacksmith.Automap.Tests
{
    public class RecursiveMapsTests
    {
        public class Source
        {
            public int count { get; set; }
            public ChildSource Child { get; set; }
        }

        public class ChildSource
        {
            public short SomeProperty { get; set; }
        }

        public class Target
        {
            public double Count { get; set; }
            public ChildTarget child { get; set; }
        }

        public struct ChildTarget
        {
            public long Someproperty { get; set; }
        }

        [Fact(DisplayName = "Recursive map")]
        public void recursive_map()
        {
            Source source;
            Target target;

            source = new Source
            {
                count = 34,
                Child = new ChildSource
                {
                    SomeProperty = 89,
                },
            };

            target = source.mapTo<Target>();

            Assert.Equal(34.0, target.Count);
            Assert.Equal(89L, target.child.Someproperty);
        }

        [Fact]
        public void missing_target_properties_test()
        {
            Tag tag;
            TagData data;

            tag = new Tag("pepe");

            data = tag.mapTo<TagData>();

            Assert.Equal("pepe", data.Name);
            Assert.Null(data.Id);
        }

    }
}
