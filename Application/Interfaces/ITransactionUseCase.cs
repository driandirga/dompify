using DompifyAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DompifyAPI.Application.Interfaces
{
    public interface ITransactionUseCase
    {
        Task<IEnumerable<Transaction?>> GetAllTransactionsAsync();
        Task<Transaction?> GetTransactionByIdAsync(int id);
        Task<IEnumerable<Transaction?>> GetTransactionsByUserIdAsync(int userId);
        Task<IEnumerable<Transaction?>> GetTransactionsByCategoryIdAsync(int categoryId);
        Task<IEnumerable<Transaction?>> GetTransactionsByCurrencyIdAsync(int currencyId);
        Task<IEnumerable<Transaction?>> GetTransactionsByDateAsync(DateTime date);
        Task<Transaction?> CreateTransactionAsync(Transaction transaction);
        Task<Transaction?> UpdateTransactionAsync(Transaction transaction);
        Task<bool> DeleteTransactionAsync(int id);
    }
}
