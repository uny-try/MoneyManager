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
    private ObservableCollection<Transaction> transactions;

    public AllTransactionsViewModel(ITransactionRepository transactionRepository, INavigationService navigationService)
    {
        _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
        _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));

        Transactions = new ObservableCollection<Transaction>();
    }

    [RelayCommand]
    private void LoadAllTransactions()
    {
        Transactions.Clear();
        var transactions = _transactionRepository.GetAllTransactions();
        foreach (var transaction in transactions)
        {
            Transactions.Add(transaction);
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
    }
}
