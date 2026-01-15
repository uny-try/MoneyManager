namespace MoneyManager.Models;

/// <summary>
/// カテゴリー
/// </summary>
/// <example>
/// { "Name": "給料", "Type": "Income" },
/// { "Name": "食費", "Type": "Expense" }
/// </example>
public class Category
{
    /// <summary>
    /// カテゴリー名
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// カテゴリータイプ
    /// </summary>
    public CategoryType Type { get; set; }

    public Category(string name, CategoryType type)
    {
        Name = name;
        Type = type;
    }
}
