using DompifyAPI.Application.Interfaces;
using DompifyAPI.Domain.Entities;
using DompifyAPI.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DompifyAPI.Application.UseCases
{
    public class TransactionUseCase(ITransactionRepository transactionRepository) : ITransactionUseCase
    {
        private readonly ITransactionRepository _transactionRepository = transactionRepository;

        public async Task<IEnumerable<Transaction?>> GetAllTransactionsAsync()
        {
            return await _transactionRepository.GetAllTransactions();
        }

        public async Task<Transaction?> GetTransactionByIdAsync(int id)
        {
            return await _transactionRepository.GetTransactionById(id);
        }

        public async Task<IEnumerable<Transaction?>> GetTransactionsByUserIdAsync(int userId)
        {
            return await _transactionRepository.GetTransactionsByUserId(userId);
        }

        public async Task<IEnumerable<Transaction?>> GetTransactionsByCategoryIdAsync(int categoryId)
        {
            return await _transactionRepository.GetTransactionsByCategoryId(categoryId);
        }

        public async Task<IEnumerable<Transaction?>> GetTransactionsByCurrencyIdAsync(int currencyId)
        {
            return await _transactionRepository.GetTransactionsByCurrencyId(currencyId);
        }

        public async Task<IEnumerable<Transaction?>> GetTransactionsByDateAsync(DateTime date)
        {
            return await _transactionRepository.GetTransactionsByDate(date);
        }

        public async Task<Transaction?> CreateTransactionAsync(Transaction transaction)
        {
            if (transaction.UserId == 0)
                throw new ArgumentException("User ID cannot be empty.");

            if (transaction.CategoryId == 0)
                throw new ArgumentException("Category ID cannot be empty.");

            if (transaction.CurrencyId == 0)
                throw new ArgumentException("Currency ID cannot be empty.");

            if (transaction.Amount == 0)
                throw new ArgumentException("Amount cannot be empty.");

            if (transaction.TransactionDate == DateTime.MinValue)
                throw new ArgumentException("Date cannot be empty.");

            transaction.CreatedAt = DateTime.UtcNow;
            transaction.CreatedBy = "SYSTEM";

            return await _transactionRepository.CreateTransaction(transaction);
        }

        public async Task<Transaction?> UpdateTransactionAsync(Transaction transaction)
        {
            var existingTransaction = await _transactionRepository.GetTransactionById(transaction.Id);
            if (existingTransaction == null)
                return null;

            existingTransaction.UserId = transaction.UserId == 0 ? existingTransaction.UserId : transaction.UserId;
            existingTransaction.CategoryId = transaction.CategoryId == 0 ? existingTransaction.CategoryId : transaction.CategoryId;
            existingTransaction.CurrencyId = transaction.CurrencyId == 0 ? existingTransaction.CurrencyId : transaction.CurrencyId;
            existingTransaction.Amount = transaction.Amount == 0 ? existingTransaction.Amount : transaction.Amount;
            existingTransaction.Remarks = transaction.Remarks ?? existingTransaction.Remarks;
            existingTransaction.UpdatedAt = DateTime.UtcNow;
            existingTransaction.UpdatedBy = "SYSTEM";

            return await _transactionRepository.UpdateTransaction(existingTransaction);
        }

        public async Task<bool> DeleteTransactionAsync(int id)
        {
            var transaction = await _transactionRepository.GetTransactionById(id);
            if (transaction == null)
                return false;

            return await _transactionRepository.DeleteTransaction(transaction);
        }
    }
}
