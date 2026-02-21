using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MoneyManager.Models;
using MoneyManager.Models.Interfaces;

namespace MoneyManager.ViewModels;

public partial class AccountSummaryViewModel : ObservableObject
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IAccountRepository _accountRepository;
    [ObservableProperty]
    private decimal totalBalance;
    [ObservableProperty]
    private ObservableCollection<Account> accounts;

    public AccountSummaryViewModel(
        ITransactionRepository transactionRepository,
        IAccountRepository accountRepository)
    {
        _transactionRepository = transactionRepository;
        _accountRepository = accountRepository;

        Accounts = [];
    }

    [RelayCommand]
    public void CulculateAccountSummary()
    {
        var today = DateTime.Today + new TimeSpan(23, 59, 59);

        var transactions = _transactionRepository.GetAllTransactions();
        Accounts = new ObservableCollection<Account>(_accountRepository.GetAllAccounts());

        foreach (var account in Accounts)
        {
            account.Balance = 0;
            account.UnsettledAmount = 0;
        }

        foreach (var transaction in transactions)
        {
            if (transaction.FromAccount is not null)
            {
                if (transaction.Date <= today)
                {
                    transaction.FromAccount.Balance -= transaction.Amount;
                }
                else
                {
                    transaction.FromAccount.UnsettledAmount -= transaction.Amount;
                }
            }

            if (transaction.ToAccount is not null)
            {
                if (transaction.Date <= today)
                {
                    transaction.ToAccount.Balance += transaction.Amount;
                }
                else
                {
                    transaction.ToAccount.UnsettledAmount += transaction.Amount;
                }
            }
        }

        // 総残高の計算
        TotalBalance = Accounts.Sum(a => a.Balance);
    }
}
