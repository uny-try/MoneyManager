using System;
using System.Text.Json;
using MoneyManager.Models.Interfaces;

namespace MoneyManager.Models.Services;

public class FileTransactionRepository : ITransactionRepository
{
    private readonly string _filePath;
    private readonly IAccountRepository _accountRepository;
    private readonly ICategoryRepository _categoryRepository;
    public FileTransactionRepository(string folderPath,
                                     IAccountRepository accountRepository,
                                     ICategoryRepository categoryRepository)
    {
        _filePath = Path.Combine(folderPath, "transactions.json");
        _accountRepository = accountRepository;
        _categoryRepository = categoryRepository;
    }

    public void AddTransaction(Transaction transaction)
    {
        var transactions = GetAllTransactions().ToList();
        transactions.Add(transaction);
        SaveTransactions(transactions);
    }

    public void DeleteTransaction(Transaction transaction)
    {
        var transactions = GetAllTransactions().ToList();
        transactions.RemoveAll(t => t.Id == transaction.Id);
        SaveTransactions(transactions);
    }

    public IEnumerable<Transaction> GetAllTransactions()
    {
        if (!File.Exists(_filePath))
        {
            return [];
        }

        var json = File.ReadAllText(_filePath);
        var transactions = JsonSerializer.Deserialize<List<Transaction>>(json) ?? [];

        var accounts = _accountRepository.GetAllAccounts().ToList();
        var categories = _categoryRepository.GetAllCategories().ToList();

        foreach (var transaction in transactions)
        {
            transaction.FromAccount = accounts.FirstOrDefault(a => a.Name == transaction.FromAccount?.Name);
            transaction.ToAccount = accounts.FirstOrDefault(a => a.Name == transaction.ToAccount?.Name);
            transaction.Category = categories.FirstOrDefault(c => c.Name == transaction.Category?.Name);
        }

        return transactions;
    }

    public Transaction? GetTransactionById(Guid id)
    {
        var transactions = GetAllTransactions();
        return transactions.FirstOrDefault(t => t.Id == id);
    }

    public void UpdateTransaction(Transaction transaction)
    {
        var transactions = GetAllTransactions().ToList();
        var index = transactions.FindIndex(t => t.Id == transaction.Id);
        if (index != -1)
        {
            transactions[index] = transaction;
            SaveTransactions(transactions);
        }
    }

    private void SaveTransactions(List<Transaction> transactions)
    {
        var json = JsonSerializer.Serialize(transactions, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        File.WriteAllText(_filePath, json);
    }
}
