namespace MoneyManager.Models;

/// <summary>
/// 一件の取引
/// </summary>
public class Transaction
{
    /// <summary>>
    /// 取引ID
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 取引タイプ
    /// </summary>
    public TransactionType Type { get; set; }

    /// <summary>
    /// 取引日付
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// 口座（出金元）
    /// 支出・振替時に使用
    /// </summary>
    public Account? FromAccount { get; set; }

    /// <summary>
    /// 口座（入金先）
    /// 収入・振替時に使用
    /// </summary>
    public Account? ToAccount { get; set; }

    /// <summary>
    /// カテゴリー
    /// </summary>
    public Category? Category { get; set; }

    /// <summary>
    /// 金額
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// メモ
    /// </summary>
    public string Note { get; set; }

    public Transaction(DateTime date,
                       TransactionType type,
                       Account? fromAccount,
                       Account? toAccount,
                       Category? category,
                       decimal amount,
                       string note)
    {
        Id = Guid.NewGuid();
        Date = date;
        Type = type;
        FromAccount = fromAccount;
        ToAccount = toAccount;
        Category = category;
        Amount = amount;
        Note = note;
    }
}
