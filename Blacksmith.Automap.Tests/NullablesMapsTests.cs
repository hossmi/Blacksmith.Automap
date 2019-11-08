using Blacksmith.Automap.Extensions;
using Xunit;

namespace Blacksmith.Automap.Tests
{
    public class NullablesMapsTests
    {
        public class UserData : AbstractData
        {
            public string Name { get; set; }
        }

        public class AbstractData
        {
            public int? Id { get; set; }
        }

        public void map_to_new_instance_works_as_expected()
        {
            UserData source, target;

            source = new UserData
            {
                Name = "pepe",
                Id = 55,
            };

            target = source.mapTo<UserData>();

            Assert.Equal(55, target.Id.Value);
            Assert.Equal("pepe", target.Name);
        }
    }
}
