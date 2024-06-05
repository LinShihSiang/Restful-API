using Restful_API.Modules;
using System.Runtime.Serialization;

namespace Restful_API.Exceptions
{
    public class OrderException : Exception
    {
        public Order? Order { get; set; }

        public OrderException()
        {
        }

        public OrderException(string? message) : base(message)
        {
        }

        public OrderException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected OrderException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
