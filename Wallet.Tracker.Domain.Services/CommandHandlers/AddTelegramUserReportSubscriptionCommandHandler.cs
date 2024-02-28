namespace Wallet.Tracker.Domain.Services.CommandHandlers;

using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Wallet.Tracker.Domain.Models.Entities;
using Wallet.Tracker.Domain.Services.Commands;

public class AddTelegramUserReportSubscriptionCommandHandler : IRequestHandler<AddTelegramUserReportSubscriptionCommand, Unit>
{
    private readonly IDbContext _dbContext;

    public AddTelegramUserReportSubscriptionCommandHandler(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(AddTelegramUserReportSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var tgUser = await _dbContext.GetQuery<TelegramUser>().FirstOrDefaultAsync(s => s.Id == request.Id);
        if (tgUser != null)
        {
            return Unit.Value;
        }

        tgUser = new TelegramUser(request.Id, true, request.UserName);
        _dbContext.Add(tgUser);
        await _dbContext.SaveChangesAsync();
        return Unit.Value;
    }
}
