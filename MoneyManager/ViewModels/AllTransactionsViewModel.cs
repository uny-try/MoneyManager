using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MoneyManager.Models;
using MoneyManager.Models.Interfaces;
using MoneyManager.ViewModels.Interfaces;

namespace MoneyManager.ViewModels;

public partial class AllTransactionsViewModel : ObservableObject
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly INavigationService _navigationService;

    [ObservableProperty]
    private ObservableCollection<Transaction> allTransactions;
    [ObservableProperty]
    private ObservableCollection<Transaction> filteredTransactions;
    [ObservableProperty]
    private decimal filteredTotalIncome;
    [ObservableProperty]
    private decimal filteredTotalExpense;
    [ObservableProperty]
    private DateTime currentMonth;
    [ObservableProperty]
    private ObservableCollection<Account> accounts;
    [ObservableProperty]
    private Account accountFilter;

    public AllTransactionsViewModel(
        ITransactionRepository transactionRepository,
        IAccountRepository accountRepository,
        INavigationService navigationService)
    {
        _transactionRepository = transactionRepository
            ?? throw new ArgumentNullException(nameof(transactionRepository));
        _accountRepository = accountRepository
            ?? throw new ArgumentNullException(nameof(accountRepository));
        _navigationService = navigationService
            ?? throw new ArgumentNullException(nameof(navigationService));

        AllTransactions = new ObservableCollection<Transaction>();
        FilteredTransactions = new ObservableCollection<Transaction>();
        CurrentMonth = DateTime.Now;

        Accounts = new ObservableCollection<Account>(_accountRepository.GetAllAccounts());
        Accounts.Insert(0, new Account("すべて"));
        AccountFilter = Accounts[0];
    }

    [RelayCommand]
    private void LoadAllTransactions()
    {
        AllTransactions.Clear();
        var transactions = _transactionRepository.GetAllTransactions();
        foreach (var transaction in transactions)
        {
            AllTransactions.Add(transaction);
        }
    }

    [RelayCommand]
    public Task GoToEditTransaction(Guid? transactionId = null)
    {
        var route = "EditTransactionPage";
        if (transactionId.HasValue)
        {
            route += $"?TransactionId={transactionId.Value}";
        }
        return _navigationService.NavigateToAsync(route);
    }

    [RelayCommand]
    public Task GoToAccountSummary()
    {
        return _navigationService.NavigateToAsync("AccountSummaryPage");
    }

    [RelayCommand]
    public void DeleteTransaction(Transaction transaction)
    {
        _transactionRepository.DeleteTransaction(transaction);
        LoadAllTransactions();
        LoadFilteredTransactions();
    }

    [RelayCommand]
    private void LoadFilteredTransactions()
    {
        FilteredTransactions = new ObservableCollection<Transaction>(
            AllTransactions
                .Where(
                    t => t.Date.Month == CurrentMonth.Month
                    && t.Date.Year == CurrentMonth.Year)
                .Where(
                    t => AccountFilter.Name == "すべて"
                    || t.FromAccount?.Name == AccountFilter.Name
                    || t.ToAccount?.Name == AccountFilter.Name)
        );

        FilteredTotalIncome = FilteredTransactions
            .Where(t => t.Type == TransactionType.Income)
            .Sum(t => t.Amount);

        FilteredTotalExpense = FilteredTransactions
            .Where(t => t.Type == TransactionType.Expense)
            .Sum(t => t.Amount);
    }

    [RelayCommand]
    private void GoToPreviousMonth()
    {
        CurrentMonth = CurrentMonth.AddMonths(-1);
        LoadFilteredTransactions();
    }

    [RelayCommand]
    private void GoToNextMonth()
    {
        CurrentMonth = CurrentMonth.AddMonths(1);
        LoadFilteredTransactions();
    }

    partial void OnAccountFilterChanged(Account value)
    {
        LoadFilteredTransactions();
    }
}
