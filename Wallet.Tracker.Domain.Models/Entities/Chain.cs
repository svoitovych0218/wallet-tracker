namespace Wallet.Tracker.Domain.Models.Entities;
public class Chain
{
    public Chain(string id, string name)
    {
        Id = id;
        Name = name;
    }
    public string Id { get; set; }
    public string Name { get; set; }
}
