using System;
using System.Collections.Generic;
using Blacksmith.Automap.Models;
using Blacksmith.Validations;

namespace Blacksmith.Automap.Services.MapRepositories
{
    public class StoragelessMapRepository : IMapRepository
    {
        private readonly IValidator assert;
        private IMapBuilder mapBuilder;

        public StoragelessMapRepository(IMapBuilder mapBuilder)
        {
            this.assert = Asserts.Default;
            this.MapBuilder = mapBuilder;
        }

        public IMapBuilder MapBuilder
        {
            get => this.mapBuilder;
            set
            {
                this.assert.isNotNull(value);
                this.mapBuilder = value;
            }
        }

        public IMap getMap(Type sourceType, Type targetType)
        {
            IEnumerable<PropertyMap> propertyMaps;

            this.assert.isNotNull(sourceType);
            this.assert.isNotNull(targetType);

            propertyMaps = this.mapBuilder.build(sourceType, targetType);

            return new Map(propertyMaps);
        }
    }
}
