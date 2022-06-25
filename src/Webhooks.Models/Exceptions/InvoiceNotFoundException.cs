using System.Runtime.Serialization;

namespace Webhooks.Models.Exceptions
{
    [Serializable]
    public class InvoiceNotFoundException : ApplicationException
    {
        public InvoiceNotFoundException() 
        { }

        public InvoiceNotFoundException(string message) : base(message)
        { }

        public InvoiceNotFoundException(string message, Exception inner) : base(message, inner)
        { }

        public InvoiceNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
