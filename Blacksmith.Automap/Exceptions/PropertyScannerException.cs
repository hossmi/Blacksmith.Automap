using System;
using System.Runtime.Serialization;

namespace Blacksmith.Automap.Exceptions
{
    [Serializable]
    public class PropertyScannerException : Exception
    {
        private Type type;

        public PropertyScannerException(Type type, string message) : base(message)
        {
            this.type = type;
        }

        protected PropertyScannerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}