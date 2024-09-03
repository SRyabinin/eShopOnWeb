using System;

namespace Microsoft.eShopWeb.ApplicationCore.Exceptions;

public class OrderFunctionUnavailableException : Exception
{
    public OrderFunctionUnavailableException()
        : base($"Order function is unavailable")
    {
    }

    protected OrderFunctionUnavailableException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
    {
    }

    public OrderFunctionUnavailableException(string message) : base(message)
    {
    }

    public OrderFunctionUnavailableException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
