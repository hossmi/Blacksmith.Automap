using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Blacksmith.Automap.Models
{
    public class Map : IMap
    {
        private readonly PropertyMap[] propertyMaps;

        public Map(IEnumerable<PropertyMap> propertyMaps)
        {
            this.propertyMaps = propertyMaps.ToArray();
        }

        public PropertyMap this[int index] => this.propertyMaps[index];
        public int Count => this.propertyMaps.Length;

        public IEnumerator<PropertyMap> GetEnumerator()
        {
            return this.propertyMaps
                .AsEnumerable()
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.propertyMaps.GetEnumerator();
        }
    }
}
