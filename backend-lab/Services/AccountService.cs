using Microsoft.EntityFrameworkCore;
using Models;

namespace backend_lab.Services;

public class AccountService
{
    private readonly RepoPull _repoPull;

    public AccountService(RepoPull repoPull)
    {
        _repoPull = repoPull;
    }

    public Task AddAccount(Guid userId)
    {
        return _repoPull.AccountRepo.AddAsync(new Account()
        {
            UserId = userId,
            Money = 0
        });
    }

    public Task<Account> GetAccount(Guid userId)
    {
        return _repoPull.AccountRepo.Context
            .Set<Account>()
            .FirstOrDefaultAsync(acc => acc.UserId.Equals(userId));
    }


    public async Task TopUpAccount(Guid userId, float moreMoney)
    {
        var account = await GetAccount(userId);
        if (account is null)
        {
            await AddAccount(userId);
            account = await GetAccount(userId);
        }
        if (account is null)
            return;
        account.Money += moreMoney;
        await _repoPull.AccountRepo.UpdateAsync(account);
    }

    public async Task TopDownAccount(Guid userId, float lessMoney)
    {
        var account = await GetAccount(userId);
        //even if no money => create acc
        if (account is null)
        {
            await AddAccount(userId);
            account = await GetAccount(userId);
        }
        if (account is null)
            return;
        account.Money -= lessMoney;
        await _repoPull.AccountRepo.UpdateAsync(account);
    }
}