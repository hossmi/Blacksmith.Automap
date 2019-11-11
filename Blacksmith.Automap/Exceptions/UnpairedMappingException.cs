using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Blacksmith.Automap.Exceptions
{
    [Serializable]
    public class UnpairedMappingException : MappingException
    {
        public UnpairedMappingException(Type sourceType, Type targetType, IEnumerable<PropertyInfo> properties
            , string message) 
            : base(sourceType, targetType, message)
        {
            this.Properties = properties
                .ToList()
                .AsReadOnly();
        }

        public UnpairedMappingException(Type sourceType, Type targetType, IEnumerable<PropertyInfo> properties
            , string message, Exception innerException) 
            : base(sourceType, targetType, message, innerException)
        {
            this.Properties = properties
                .ToList()
                .AsReadOnly();
        }

        protected UnpairedMappingException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }

        public ReadOnlyCollection<PropertyInfo> Properties { get; }
    }
}