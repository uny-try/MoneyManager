using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MoneyManager.Models;
using MoneyManager.Models.Interfaces;
using MoneyManager.ViewModels.Interfaces;

namespace MoneyManager.ViewModels;

public partial class EditTransactionViewModel : ObservableObject
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly INavigationService _navigationService;

    [ObservableProperty]
    private Guid? transactionId;
    [ObservableProperty]
    private TransactionType selectedTransactionType;
    [ObservableProperty]
    private DateTime date;
    [ObservableProperty]
    private Account? fromAccount;
    [ObservableProperty]
    private Account? toAccount;
    [ObservableProperty]
    private Category? category;
    [ObservableProperty]
    private decimal amount;
    [ObservableProperty]
    private string? note;

    // 選択肢のリスト
    [ObservableProperty]
    private List<Category> incomeCategories;
    [ObservableProperty]
    private List<Category> expenseCategories;
    [ObservableProperty]
    private List<Account> accounts;

    public EditTransactionViewModel(
        ITransactionRepository transactionRepository,
        ICategoryRepository categoryRepository,
        IAccountRepository accountRepository,
        INavigationService navigationService)
    {
        _transactionRepository = transactionRepository;
        _categoryRepository = categoryRepository;
        _accountRepository = accountRepository;
        _navigationService = navigationService;

        var categories = _categoryRepository.GetAllCategories().ToList();
        IncomeCategories = categories.Where(c => c.Type == CategoryType.Income).ToList();
        ExpenseCategories = categories.Where(c => c.Type == CategoryType.Expense).ToList();

        Accounts = _accountRepository.GetAllAccounts().ToList();

        Date = DateTime.Now;
        SelectedTransactionType = TransactionType.Expense;
    }

    public void LoadTransaction(Guid? id = null)
    {
        if (id is null)
        {
            // 新規取引の場合はデフォルト値を設定
            Date = DateTime.Now;
            SelectedTransactionType = TransactionType.Expense;
            FromAccount = null;
            ToAccount = null;
            Category = null;
            Amount = 0;
            Note = string.Empty;
            return;
        }

        var transaction = _transactionRepository.GetTransactionById(id.Value);
        if (transaction != null)
        {
            // 既存取引の場合は値を読み込む
            TransactionId = transaction.Id;
            SelectedTransactionType = transaction.Type;
            Date = transaction.Date;
            FromAccount = transaction.FromAccount;
            ToAccount = transaction.ToAccount;
            Category = transaction.Category;
            Amount = transaction.Amount;
            Note = transaction.Note;
        }
    }

    [RelayCommand]
    public Task SaveTransaction()
    {
        var newTransaction = new Transaction(
            date: Date,
            type: SelectedTransactionType,
            fromAccount: FromAccount,
            toAccount: ToAccount,
            category: Category,
            amount: Amount,
            note: Note ?? string.Empty
            );

        // 入力のチェック
        if (SelectedTransactionType == TransactionType.Expense)
        {
            newTransaction.ToAccount = null;
        }
        else if (SelectedTransactionType == TransactionType.Income)
        {
            newTransaction.FromAccount = null;
        }

        if (TransactionId is null)
        {
            _transactionRepository.AddTransaction(newTransaction);
        }
        else
        {
            newTransaction.Id = TransactionId.Value;
            _transactionRepository.UpdateTransaction(newTransaction);
        }

        return _navigationService.GoBackAsync();
    }

    [RelayCommand]
    public Task Cancel()
    {
        return _navigationService.GoBackAsync();
    }
}
