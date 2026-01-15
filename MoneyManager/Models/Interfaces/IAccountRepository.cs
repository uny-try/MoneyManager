namespace MoneyManager.Models.Interfaces;

/// <summary>
/// 口座リポジトリインターフェース
/// </summary>
public interface IAccountRepository
{
    /// <summary>
    /// すべての口座を取得する
    /// </summary>
    IEnumerable<Account> GetAllAccounts();
}
