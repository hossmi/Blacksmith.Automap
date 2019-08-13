using System;
using System.Runtime.Serialization;

namespace blaxpro.Automap.Exceptions
{
    [Serializable]
    public class MappingException : Exception
    {
        public MappingException(Type sourceType, Type targetType, string message) : base(message)
        {
            this.SourceType = sourceType ?? throw new ArgumentNullException(nameof(sourceType));
            this.TargetType = targetType ?? throw new ArgumentNullException(nameof(targetType));
        }

        public MappingException(Type sourceType, Type targetType, string message, Exception innerException) : base(message, innerException)
        {
            this.SourceType = sourceType ?? throw new ArgumentNullException(nameof(sourceType));
            this.TargetType = targetType ?? throw new ArgumentNullException(nameof(targetType));
        }

        protected MappingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public Type SourceType { get; }
        public Type TargetType { get; }
    }
}