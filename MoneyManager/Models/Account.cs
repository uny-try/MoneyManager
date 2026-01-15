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

    public Account(string name)
    {
        Name = name;
    }
}
