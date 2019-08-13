using blaxpro.Automap.Extensions;
using Xunit;

namespace blaxpro.Automap.Tests
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

        [Fact(DisplayName = "Map from int to double")]
        public void Map_from_int_to_double()
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
    }
}
