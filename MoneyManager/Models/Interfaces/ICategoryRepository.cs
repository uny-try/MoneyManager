namespace MoneyManager.Models.Interfaces;

/// <summary>
/// カテゴリーリポジトリインターフェース
/// </summary>
public interface ICategoryRepository
{
    /// <summary>
    /// すべてのカテゴリーを取得する
    /// </summary>
    IEnumerable<Category> GetAllCategories();
}
