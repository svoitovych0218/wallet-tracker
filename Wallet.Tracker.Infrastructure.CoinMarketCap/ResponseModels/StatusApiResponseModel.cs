namespace Wallet.Tracker.Infrastructure.CoinMarketCap.ResponseModels;
using System;
using System.Text.Json.Serialization;

internal class CoinMarketCapApiResponseStatusModel
{
    public DateTime Timestamp { get; set; }

    [JsonPropertyName("error_code")]
    public string ErrorCode { get; set; }

    [JsonPropertyName("error_message")]
    public string? ErrorMessage { get; set; }
}
