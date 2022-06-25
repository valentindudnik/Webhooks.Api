using System.Runtime.Serialization;

namespace Webhooks.Models.Exceptions
{
    public class InvoiceInvalidException : ApplicationException
    {
        public InvoiceInvalidException()
        { }

        public InvoiceInvalidException(string message) : base(message)
        { }

        public InvoiceInvalidException(string message, Exception inner) : base(message, inner)
        { }

        public InvoiceInvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
