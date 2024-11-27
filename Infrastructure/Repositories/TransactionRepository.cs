using DompifyAPI.Domain.Entities;
using DompifyAPI.Domain.Interfaces;
using DompifyAPI.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DompifyAPI.Infrastructure.Repositories
{
    public class TransactionRepository(ApplicationDbContext context): ITransactionRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<IEnumerable<Transaction?>> GetAllTransactions()
        {
            return await _context.Transactions.ToListAsync();
        }

        public async Task<Transaction?> GetTransactionById(int id)
        {
            return await _context.Transactions.FindAsync(id);
        }

        public async Task<Transaction?> GetTransactionByGuid(Guid guid)
        {
            return await _context.Transactions.FirstOrDefaultAsync(t => t.Guid == guid);
        }

        public async Task<IEnumerable<Transaction?>> GetTransactionsByUserId(int userId)
        {
            return await _context.Transactions.Where(t => t.UserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<Transaction?>> GetTransactionsByCategoryId(int categoryId)
        {
            return await _context.Transactions.Where(t => t.CategoryId == categoryId).ToListAsync();
        }

        public async Task<IEnumerable<Transaction?>> GetTransactionsByCurrencyId(int currencyId)
        {
            return await _context.Transactions.Where(t => t.CurrencyId == currencyId).ToListAsync();
        }

        public async Task<IEnumerable<Transaction?>> GetTransactionsByDate(DateTime date)
        {
            return await _context.Transactions.Where(t => t.TransactionDate == date).ToListAsync();
        }

        public async Task<Transaction?> CreateTransaction(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return transaction;
        }

        public async Task<Transaction?> UpdateTransaction(Transaction transaction)
        {
            _context.Transactions.Update(transaction);
            await _context.SaveChangesAsync();

            return transaction;
        }

        public async Task<bool> DeleteTransaction(Transaction transaction)
        {
            _context.Transactions.Remove(transaction);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
