namespace Wallet.Tracker.Domain.Services.Commands;

using MediatR;

public class AddTelegramUserReportSubscriptionCommand : IRequest<Unit>
{
    public AddTelegramUserReportSubscriptionCommand(long id, string? userName)
    {
        Id = id;
        UserName = userName;
    }

    public long Id { get; }
    public string? UserName { get; }
}
