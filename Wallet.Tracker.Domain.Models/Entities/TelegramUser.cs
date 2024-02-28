namespace Wallet.Tracker.Domain.Models.Entities;
public class TelegramUser
{
    public TelegramUser(long id, bool isSubscribed, string? userName)
    {
        Id = id;
        IsSubscribed = isSubscribed;
        UserName = userName;
    }
    public long Id { get; set; }
    public long RowVersion { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsSubscribed { get; set; }
    public string? UserName { get; set; }
}
