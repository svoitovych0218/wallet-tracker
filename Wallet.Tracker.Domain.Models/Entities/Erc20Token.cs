namespace Wallet.Tracker.Domain.Models.Entities;
public class Erc20Token
{
    public Erc20Token(
        string address,
        string chainId,
        string name,
        string symbol,
        bool existAtCoinmarketCap,
        bool contractCodePublished
        )
    {
        Address = address;
        ChainId = chainId;
        Name = name;
        Symbol = symbol;
        ExistAtCoinmarketCap = existAtCoinmarketCap;
        ContractCodePublished = contractCodePublished;
    }

    public string Address { get; set; }
    public string ChainId { get; set; }
    public string Name { get; set; }
    public string Symbol { get; set; }
    public bool ExistAtCoinmarketCap { get; set; }
    public bool ContractCodePublished { get; set; }

    public Chain Chain { get; set; }
}
