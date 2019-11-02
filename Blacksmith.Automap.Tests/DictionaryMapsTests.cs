using Blacksmith.Automap.Exceptions;
using Blacksmith.Automap.Extensions;
using Blacksmith.Automap.Extensions.Dictionaries;
using System;
using System.Collections.Generic;
using Xunit;

namespace Blacksmith.Automap.Tests
{
    public class DictionaryMapsTests
    {
        public class Person
        {
            public string Name { get; set; }
            public string LastName { get; set; }
            public DateTime BirthDate { get; set; }
            public decimal Amount { get; set; }
        }

        [Fact]
        public void map_from_dictionary()
        {
            IDictionary<string, object> sourceValues;
            Person targetPerson;

            sourceValues = new Dictionary<string, object>
            {
                { "Name", "Narciso" },
                { "Lastname", "De la Rosa Huertas" },
                { "amount", 13000m },
                { "Birthdate", new DateTime(2005,8,13) },
            };

            targetPerson = sourceValues.mapValuesTo<Person>();

            Assert.Equal(13000m, targetPerson.Amount);
            Assert.Equal("Narciso", targetPerson.Name);
            Assert.Equal("De la Rosa Huertas", targetPerson.LastName);
            Assert.Equal(new DateTime(2005, 8, 13), targetPerson.BirthDate);
        }

        [Fact]
        public void map_from_int_to_bool_fails()
        {
            IDictionary<string, object> target;
            Person source;

            source = new Person
            {
                Name = "Rosa",
                LastName = "Robles Olmos",
                BirthDate = new DateTime(1955, 11, 12),
                Amount = 86000m,
            };

            target = source.toDictionary();

            Assert.Equal(86000m, target[nameof(Person.Amount)]);
            Assert.Equal("Rosa", target[nameof(Person.Name)]);
            Assert.Equal("Robles Olmos", target[nameof(Person.LastName)]);
            Assert.Equal(new DateTime(1955, 11, 12), target[nameof(Person.BirthDate)]);
        }
    }
}
