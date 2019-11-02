using System;
using System.Runtime.Serialization;
using Blacksmith.Validations.Exceptions;

namespace Blacksmith.Automap.Exceptions
{
    public class PropertyAccessorException : DomainException
    {
        public PropertyAccessorException(string message) : base(message)
        {
        }

        public PropertyAccessorException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PropertyAccessorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}