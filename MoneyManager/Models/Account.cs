namespace MoneyManager.Models;

/// <summary>
/// 口座
/// </summary>
public class Account
{
    /// <summary>
    /// 口座名
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 残高   
    /// </summary>
    public decimal Balance { get; set; }
    /// <summary>
    /// 未決済金額
    /// </summary>
    public decimal UnsettledAmount { get; set; }

    public Account(string name)
    {
        Name = name;
    }
}
