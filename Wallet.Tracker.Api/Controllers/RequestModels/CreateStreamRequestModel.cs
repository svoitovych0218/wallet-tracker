namespace Wallet.Tracker.Api.Controllers.RequestModels;

public class CreateStreamRequestModel
{
    public string WalletAddress { get; set; }
    public string Title { get; set; }
    public string[] ChainIds { get; set; }
}
