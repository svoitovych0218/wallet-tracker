namespace Wallet.Tracker.Domain.Services.Services.MoralisStreamsApiClient.Models;
internal class CreateErc20TransferStreamApiRequest
{
    public string WebhookUrl { get; set; }
    public string Description { get; set; }
    public string Tag { get; set; }
    public string[] Topic0 { get; } = new string[] { "Transfer(address,address,uint256)" };
    public bool AllAddresses { get; } = false;
    public bool IncludeNativeTxs { get; } = false;
    public bool IncludeContractLogs { get; } = true;
    public bool IncludeInternalTxs { get; } = false;
    public bool IncludeAllTxLogs { get; } = false;
    public string[] GetNativeBalances { get; } = new string[] { };
    public AbiApiRequest[] Abi { get; } = AbiApiRequest.GetErc20TransferAbi();
    public string? AdvancedOptions { get; } = null;
    public bool Demo { get; } = false;
    public string[] ChainIds { get; set; }
    public string[] Triggers { get; } = new string[] { }; 
}
