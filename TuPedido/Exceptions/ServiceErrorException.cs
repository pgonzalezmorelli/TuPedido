using System;
using System.Runtime.Serialization;

namespace TuPedido.Exceptions
{
    public class ServiceErrorException : Exception
    {
        public int StatusCode { get; private set; }

        public ServiceErrorException(int statusCode)
        {
            StatusCode = statusCode;
        }

        public ServiceErrorException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }

        public ServiceErrorException(int statusCode, string message, Exception innerException) : base(message, innerException)
        {
            StatusCode = statusCode;
        }

        protected ServiceErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
