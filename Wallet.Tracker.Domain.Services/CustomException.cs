namespace Wallet.Tracker.Domain.Services;
using System;

public class CustomException : Exception
{
    public CustomException(string message, int statusCode)
        : base(message)
    {
        StatusCode = statusCode;
    }
    public int StatusCode { get; set; }
}
