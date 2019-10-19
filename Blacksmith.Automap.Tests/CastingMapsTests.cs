using Blacksmith.Automap.Exceptions;
using Blacksmith.Automap.Extensions;
using Xunit;

namespace Blacksmith.Automap.Tests
{
    public class CastingMapsTests
    {
        public class Source
        {
            public int count { get; set; }
        }

        public class Target
        {
            public double Count { get; set; }
        }

        public class BoolTarget
        {
            public bool Count { get; set; }
        }

        [Fact]
        public void map_from_int_to_double_works()
        {
            Source source;
            Target target;

            source = new Source
            {
                count = 34,
            };

            target = source.mapTo<Target>();

            Assert.Equal(34.0, target.Count);
        }

        [Fact]
        public void map_from_int_to_bool_fails()
        {
            Source source;
            BoolTarget target;

            source = new Source
            {
                count = 34,
            };

            Assert.Throws<MappingException>(() => target = source.mapTo<BoolTarget>());
        }
    }
}
