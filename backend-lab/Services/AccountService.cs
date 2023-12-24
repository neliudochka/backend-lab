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
        if (moreMoney < 0)
        {
            throw new Exception("Cant top up by negative amount of money");    
        }
        
        var account = await GetAccount(userId);
        if (account is null)
        {
            await AddAccount(userId);
            account = await GetAccount(userId);
        }
        if (account is null)
            throw new Exception("No user with such ID");

        if (account.Money + moreMoney > 100000)
        {
            throw new Exception("Account cant hold more than 100000");    
        }
        account.Money += moreMoney;
        await _repoPull.AccountRepo.UpdateAsync(account);
    }

    public async Task TopDownAccount(Guid userId, float lessMoney)
    {
        if (lessMoney < 0)
        {
            throw new Exception("Cant top down by negative amount of money");    
        }
        
        var account = await GetAccount(userId);
        //even if no money => create acc
        if (account is null)
        {
            await AddAccount(userId);
            account = await GetAccount(userId);
        }
        if (account is null)
            throw new Exception("No user with such ID");

        if (account.Money - lessMoney < 0)
        {
            throw new Exception("There are not enough funds in the account");    
        }
        account.Money -= lessMoney;
        await _repoPull.AccountRepo.UpdateAsync(account);
    }
}