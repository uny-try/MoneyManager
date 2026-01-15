using MoneyManager.Models.Interfaces;

namespace MoneyManager.Models.Services;

public class InMemoryCategoryRepository : ICategoryRepository
{
    private readonly List<Category> _categories = new List<Category>
    {
        new Category("食費", CategoryType.Expense),
        new Category("交通費", CategoryType.Expense),
        new Category("通信費", CategoryType.Expense),
        new Category("衣服", CategoryType.Expense),
        new Category("生活用品", CategoryType.Expense),
        new Category("娯楽", CategoryType.Expense),
        new Category("その他", CategoryType.Expense),
        new Category("給与", CategoryType.Income),
        new Category("その他", CategoryType.Income)
    };

    public IEnumerable<Category> GetAllCategories()
    {
        return _categories;
    }
}
