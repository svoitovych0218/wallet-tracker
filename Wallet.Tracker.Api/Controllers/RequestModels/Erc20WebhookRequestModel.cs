namespace Wallet.Tracker.Api.Controllers.RequestModels;

public class MoralisWebhookBlockModel
{
    public string Hash { get; set; }
    public string Timestamp { get; set; }
    public string Number { get; set; }
}

public class Erc20WebhookTransferModel
{
    public string TransactionHash { get; set; }
    public string Contract { get; set; }
    public string From { get; set; }
    public string To { get; set; }
    public string Value { get; set; }
    public string TokenName { get; set; }
    public string TokenSymbol { get; set; }
    public string TokenDecimals { get; set; }
    public string ValueWithDecimals { get; set; }
    public bool PossibleSpam { get; set; }
}

public class Erc20WebhookRequestModel
{
    public bool Confirmed { get; set; }
    public string ChainId {  get; set; }
    public MoralisWebhookBlockModel Block { get; set; }
    public List<Erc20WebhookTransferModel> Erc20Transfers { get; set; } 
}
