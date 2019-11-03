using Blacksmith.Automap.Exceptions;
using Blacksmith.Validations;
using Blacksmith.Validations.Localizations;
using System;
using System.Collections.Generic;

namespace Blacksmith.Automap.Models
{
    public class DictionaryAccessor : IPropertyAccessor
    {
        private readonly IDictionary<string, object> values;
        private readonly string key;
        private readonly IValidator validate;

        public DictionaryAccessor(IDictionary<string, object> values, string key)
        {
            this.validate = new Validator<PropertyAccessorException>(new EnValidatorStrings(), prv_buildException);
            this.validate.isNotNull(values);
            this.validate.stringIsNotEmpty(key);

            this.values = values;
            this.key = key;
        }

        private static PropertyAccessorException prv_buildException(string message)
        {
            return new PropertyAccessorException(message);
        }

        public Type Type
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Name => throw new NotImplementedException();
        public Type ObjectType => throw new NotImplementedException();

        public object getValue(object obj)
        {
            throw new NotImplementedException();
        }

        public void setValue(object obj, object value)
        {
            throw new NotImplementedException();
        }
    }
}