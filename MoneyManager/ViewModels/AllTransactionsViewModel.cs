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

    private readonly INavigationService _navigationService;

    [ObservableProperty]
    private ObservableCollection<Transaction> allTransactions;
    [ObservableProperty]
    private ObservableCollection<Transaction> monthlyTransactions;
    [ObservableProperty]
    private decimal monthlyTotalIncome;
    [ObservableProperty]
    private decimal monthlyTotalExpense;
    [ObservableProperty]
    private DateTime currentMonth;

    public AllTransactionsViewModel(ITransactionRepository transactionRepository, INavigationService navigationService)
    {
        _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
        _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));

        AllTransactions = new ObservableCollection<Transaction>();
        MonthlyTransactions = new ObservableCollection<Transaction>();
        CurrentMonth = DateTime.Now;
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
    public void DeleteTransaction(Transaction transaction)
    {
        _transactionRepository.DeleteTransaction(transaction);
        LoadAllTransactions();
        LoadMonthlyTransactions();
    }

    [RelayCommand]
    private void LoadMonthlyTransactions()
    {
        MonthlyTransactions = new ObservableCollection<Transaction>(
            AllTransactions.Where(t => t.Date.Month == CurrentMonth.Month && t.Date.Year == CurrentMonth.Year)
        );

        MonthlyTotalIncome = MonthlyTransactions
            .Where(t => t.Type == TransactionType.Income)
            .Sum(t => t.Amount);

        MonthlyTotalExpense = MonthlyTransactions
            .Where(t => t.Type == TransactionType.Expense)
            .Sum(t => t.Amount);
    }

    [RelayCommand]
    private void GoToPreviousMonth()
    {
        CurrentMonth = CurrentMonth.AddMonths(-1);
        LoadMonthlyTransactions();
    }

    [RelayCommand]
    private void GoToNextMonth()
    {
        CurrentMonth = CurrentMonth.AddMonths(1);
        LoadMonthlyTransactions();
    }
}
