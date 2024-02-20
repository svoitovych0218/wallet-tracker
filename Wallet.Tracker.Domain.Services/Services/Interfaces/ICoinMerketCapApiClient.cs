namespace Wallet.Tracker.Domain.Services.Services.Interfaces;

using Wallet.Tracker.Infrastructure.CoinMarketCap.Contracts;

public interface ICoinMerketCapApiClient
{
    Task<CoinMarketCapTokenInfoResponse> GetTokenInfo(string tokenAddress);
}
