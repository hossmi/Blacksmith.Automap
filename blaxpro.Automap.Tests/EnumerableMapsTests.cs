using System.Collections.Generic;
using System.Linq;
using Blaxpro.Automap.Extensions;
using Xunit;

namespace Blaxpro.Automap.Tests
{
    public class EnumerableMapsTests
    {
        public class Source
        {
            public int count { get; set; }
        }

        public class DefaultConstructorTarget
        {
            public double Count { get; set; }
        }

        public class ParametrizedConstructorTarget
        {
            public ParametrizedConstructorTarget(double count)
            {
                this.CouNT = count;
            }

            public double CouNT { get; set; }
        }

        [Fact(DisplayName = "Map enumerble")]
        public void map_enumerable_of_default_constructor()
        {
            Source[] source;
            IList<DefaultConstructorTarget> target;

            source = new[]
            {
                new Source { count = 13 },
                new Source { count = 21 },
                new Source { count = 34 },
                new Source { count = 55 },
            };

            target = source
                .map<DefaultConstructorTarget>()
                .ToList();

            Assert.Equal(4, target.Count);
            Assert.Equal(55, target[3].Count);
        }

        [Fact(DisplayName = "Map enumerble")]
        public void map_enumerable_of_parametrized_constructor()
        {
            Source[] source;
            IList<ParametrizedConstructorTarget> target;

            source = new[]
            {
                new Source { count = 13 },
                new Source { count = 21 },
                new Source { count = 34 },
                new Source { count = 55 },
            };

            target = source
                .map(i => new ParametrizedConstructorTarget(i.count))
                .ToList();

            Assert.Equal(4, target.Count);
            Assert.Equal(55, target[3].CouNT);
        }
    }
}
