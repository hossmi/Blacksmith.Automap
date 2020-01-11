using Blacksmith.Automap.Services;
using Blacksmith.Automap.Services.FieldAccessors;
using Blacksmith.Automap.Tests.Models;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Data;
using Xunit;
using Blacksmith.Automap.Extensions.FieldAccessors;

namespace Blacksmith.Automap.Tests
{
    public class DictionaryTests
    {
        const string kNombre = "nombre";
        const string kApellidos = "apellidos";
        const string kFecha_nacimiento = "fecha_nacimiento";
        const string kHora_nacimiento = "hora_nacimiento";
        const string kNumero = "numero";
        const string kDouble = "numero_real";
        const string kDecimal = "numero_decimal";

        [Theory]
        [MemberData(nameof(getRows))]
        public void map_row_to_dictionary(DataRow row)
        {
            IDictionary<string, object> dictionary;

            dictionary = row
                .convert()
                .toDictionary();

            dictionary.Should().NotBeNull();
            dictionary.Count.Should().Be(6);
            dictionary[nameof(SimpleTestClass.Name)].Should().Be(row[nameof(SimpleTestClass.Name)]);
            dictionary[nameof(SimpleTestClass.LastName)].Should().Be(row[nameof(SimpleTestClass.LastName)]);
            dictionary[nameof(SimpleTestClass.BirthDate)].Should().Be(row[nameof(SimpleTestClass.BirthDate)]);
            dictionary[nameof(SimpleTestClass.DecimalNumber)].Should().Be(row[nameof(SimpleTestClass.DecimalNumber)]);
            dictionary[kDouble].Should().Be(row[kDouble]);
            dictionary[kNumero].Should().Be(row[kNumero]);
        }

        [Theory]
        [MemberData(nameof(getRows))]
        public void test_row_accesor(DataRow row)
        {
            IFieldAccessor<DataRow> fieldAccessor;

            fieldAccessor = new DataRowFieldAccessor(row);
            fieldAccessor.Instance.Should().BeSameAs(row);
            fieldAccessor.Fields.Should().HaveCount(row.Table.Columns.Count);

            foreach (string key in fieldAccessor.Fields)
                fieldAccessor[key].Should().Be(row[key]);
        }

        [Theory]
        [MemberData(nameof(getRows))]
        public void test_dictionary_accesor(IDictionary<string, object> source)
        {
            IFieldAccessor<IDictionary<string, object>> fieldAccessor;

            fieldAccessor = new DictionaryFieldAccessor(source);
            fieldAccessor.Instance.Should().BeSameAs(source);
            fieldAccessor.Fields.Should().HaveCount(source.Count);

            foreach (string key in fieldAccessor.Fields)
                fieldAccessor[key].Should().Be(source[key]);
        }

        public static IEnumerable<object[]> getRows()
        {
            DataSet dataSet;
            DataTable table;
            DataRow row;

            dataSet = new DataSet();

            table = dataSet.Tables.Add();
            table.Columns.Add(nameof(SimpleTestClass.Name), typeof(string));
            table.Columns.Add(nameof(SimpleTestClass.LastName), typeof(string));
            table.Columns.Add(nameof(SimpleTestClass.BirthDate), typeof(DateTime));
            table.Columns.Add(nameof(SimpleTestClass.DecimalNumber), typeof(decimal));
            table.Columns.Add(kDouble, typeof(double));
            table.Columns.Add(kNumero, typeof(int));

            row = table.NewRow();
            row[nameof(SimpleTestClass.Name)] = "Pepe";
            row[nameof(SimpleTestClass.LastName)] = "De la Rosa Martínez";
            row[nameof(SimpleTestClass.BirthDate)] = new DateTime(1980, 12, 31);
            row[nameof(SimpleTestClass.DecimalNumber)] = decimal.MaxValue;
            row[kNumero] = 666;
            row[kDouble] = Math.PI;
            table.Rows.Add(row);
            yield return new object[] { row };

            row = table.NewRow();
            row[nameof(SimpleTestClass.Name)] = "Tronco";
            row[nameof(SimpleTestClass.LastName)] = "Ramírez Villalobos";
            row[nameof(SimpleTestClass.BirthDate)] = new DateTime(1955, 10, 12);
            row[nameof(SimpleTestClass.DecimalNumber)] = 666m;
            row[kNumero] = 512;
            row[kDouble] = Math.E;
            table.Rows.Add(row);
            yield return new object[] { row };
        }

        public static IEnumerable<object[]> getDictionaries()
        {
            yield return new object[]
            {
                new Dictionary<string, object>
                {
                    { "pepe", 3.14m },
                    { "tronco", "uno que llega" },
                },
            };

            yield return new object[]
            {
                new Dictionary<string, object>
                {
                    { "pepe", 3.14m },
                    { "tronco", "uno que llega" },
                },
            };
        }

    }
}
