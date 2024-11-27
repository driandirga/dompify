using DompifyAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DompifyAPI.Domain.Interfaces
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction?>> GetAllTransactions();
        Task<Transaction?> GetTransactionById(int id);
        Task<Transaction?> GetTransactionByGuid(Guid guid);
        Task<IEnumerable<Transaction?>> GetTransactionsByUserId(int userId);
        Task<IEnumerable<Transaction?>> GetTransactionsByCategoryId(int categoryId);
        Task<IEnumerable<Transaction?>> GetTransactionsByCurrencyId(int currencyId);
        Task<IEnumerable<Transaction?>> GetTransactionsByDate(DateTime date);
        Task<Transaction?> CreateTransaction(Transaction transaction);
        Task<Transaction?> UpdateTransaction(Transaction transaction);
        Task<bool> DeleteTransaction(Transaction transaction);
    }
}
