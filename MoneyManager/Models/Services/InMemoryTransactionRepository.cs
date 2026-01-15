using Microsoft.Maui.Controls.PlatformConfiguration;
using MoneyManager.Models.Interfaces;

namespace MoneyManager.Models.Services;

public class InMemoryTransactionRepository : ITransactionRepository
{
    private readonly List<Transaction> _transactions = new List<Transaction>();

    public IEnumerable<Transaction> GetAllTransactions()
    {
        return _transactions;
    }

    public Transaction? GetTransactionById(Guid id)
    {
        return _transactions.FirstOrDefault(t => t.Id == id);
    }

    public void AddTransaction(Transaction transaction)
    {
        _transactions.Add(transaction);
    }

    public void UpdateTransaction(Transaction transaction)
    {
        var index = _transactions.FindIndex(t => t.Id == transaction.Id);
        if (index != -1)
        {
            _transactions[index] = transaction;
        }
    }

    public void DeleteTransaction(Transaction transaction)
    {
        _transactions.Remove(transaction);
    }
}