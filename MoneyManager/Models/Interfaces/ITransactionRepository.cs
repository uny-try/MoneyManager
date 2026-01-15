namespace MoneyManager.Models.Interfaces;

/// <summary>
/// 取引リポジトリインターフェース
/// </summary>
public interface ITransactionRepository
{
    /// <summary>
    /// すべての取引を取得する
    /// </summary>
    IEnumerable<Transaction> GetAllTransactions();

    /// <summary>
    /// IDで取引を取得する
    /// </summary>
    Transaction? GetTransactionById(Guid id);

    /// <summary>
    /// 取引を追加する
    /// </summary>
    void AddTransaction(Transaction transaction);

    /// <summary>
    /// 取引を更新する
    /// </summary>
    void UpdateTransaction(Transaction transaction);

    /// <summary>
    /// 取引を削除する
    /// </summary>
    void DeleteTransaction(Transaction transaction);
}
