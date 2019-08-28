using Blaxpro.Automap.Exceptions;
using Blaxpro.Automap.Extensions;
using Xunit;

namespace Blaxpro.Automap.Tests
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

        [Fact(DisplayName = "Map from int to double")]
        public void map_from_int_to_double()
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

        [Fact(DisplayName = "Map from int to bool")]
        public void map_from_int_to_bool()
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
