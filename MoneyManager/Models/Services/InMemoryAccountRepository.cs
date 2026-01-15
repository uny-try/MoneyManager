using MoneyManager.Models.Interfaces;

namespace MoneyManager.Models.Services;

public class InMemoryAccountRepository : IAccountRepository
{
    private readonly List<Account> _accounts = new List<Account>
    {
        new Account("現金"),
        new Account("楽天Edy"),
        new Account("楽天カード"),
        new Account("秋田銀行"),
        new Account("ゆうちょ銀行"),
        new Account("楽天証券")
    };

    public IEnumerable<Account> GetAllAccounts()
    {
        return _accounts;
    }
}
