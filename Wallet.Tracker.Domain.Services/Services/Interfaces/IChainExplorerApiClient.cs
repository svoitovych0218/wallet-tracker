namespace Wallet.Tracker.Domain.Services.Services.Interfaces;
using Wallet.Tracker.Infrastruction.ChainExplorer.Contracts;

public interface IChainExplorerApiClient
{
    Task<ContractVerified> GetContractVerified(string address);
}
