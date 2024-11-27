using DompifyAPI.Application.Helpers;
using DompifyAPI.Application.Interfaces;
using DompifyAPI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DompifyAPI.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController(ITransactionUseCase transactionUseCase) : Controller
    {
        private readonly ITransactionUseCase _transactionUseCase = transactionUseCase;

        [HttpGet]
        public async Task<IActionResult> GetAllTransactions()
        {
            try
            {
                var transactions = await _transactionUseCase.GetAllTransactionsAsync();
                if (transactions == null || !transactions.Any())
                    return ResponseFormatter.Success(StatusCodes.Status200OK, "Transactions not found", null);

                return ResponseFormatter.Success(StatusCodes.Status200OK, "Transactions found", transactions);
            }
            catch (Exception ex)
            {
                return ResponseFormatter.Failure(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransactionById(int id)
        {
            try
            {
                var transaction = await _transactionUseCase.GetTransactionByIdAsync(id);
                if (transaction == null)
                    return ResponseFormatter.Success(StatusCodes.Status200OK, "Transaction not found", null);

                return ResponseFormatter.Success(StatusCodes.Status200OK, "Transaction found", transaction);
            }
            catch (Exception ex)
            {
                return ResponseFormatter.Failure(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetTransactionsByUserId(int userId)
        {
            try
            {
                var transactions = await _transactionUseCase.GetTransactionsByUserIdAsync(userId);
                if (transactions == null || !transactions.Any())
                    return ResponseFormatter.Success(StatusCodes.Status200OK, "Transactions not found", null);

                return ResponseFormatter.Success(StatusCodes.Status200OK, "Transactions found", transactions);
            }
            catch (Exception ex)
            {
                return ResponseFormatter.Failure(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetTransactionsByCategoryId(int categoryId)
        {
            try
            {
                var transactions = await _transactionUseCase.GetTransactionsByCategoryIdAsync(categoryId);
                if (transactions == null || !transactions.Any())
                    return ResponseFormatter.Success(StatusCodes.Status200OK, "Transactions not found", null);

                return ResponseFormatter.Success(StatusCodes.Status200OK, "Transactions found", transactions);
            }
            catch (Exception ex)
            {
                return ResponseFormatter.Failure(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpGet("currency/{currencyId}")]
        public async Task<IActionResult> GetTransactionsByCurrencyId(int currencyId)
        {
            try
            {
                var transactions = await _transactionUseCase.GetTransactionsByCurrencyIdAsync(currencyId);
                if (transactions == null || !transactions.Any())
                    return ResponseFormatter.Success(StatusCodes.Status200OK, "Transactions not found", null);

                return ResponseFormatter.Success(StatusCodes.Status200OK, "Transactions found", transactions);
            }
            catch (Exception ex)
            {
                return ResponseFormatter.Failure(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpGet("date/{date}")]
        public async Task<IActionResult> GetTransactionsByDate(DateTime date)
        {
            try
            {
                var transactions = await _transactionUseCase.GetTransactionsByDateAsync(date);
                if (transactions == null || !transactions.Any())
                    return ResponseFormatter.Success(StatusCodes.Status200OK, "Transactions not found", null);

                return ResponseFormatter.Success(StatusCodes.Status200OK, "Transactions found", transactions);
            }
            catch (Exception ex)
            {
                return ResponseFormatter.Failure(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] Transaction transaction)
        {
            try
            {
                var createdTransaction = await _transactionUseCase.CreateTransactionAsync(transaction);

                return ResponseFormatter.Success(StatusCodes.Status201Created, "Transaction created", createdTransaction);
            }
            catch (Exception ex)
            {
                return ResponseFormatter.Failure(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransaction(int id, [FromBody] Transaction transaction)
        {
            try
            {
                transaction.Id = id;
                var updatedTransaction = await _transactionUseCase.UpdateTransactionAsync(transaction);
                if (updatedTransaction == null)
                    return ResponseFormatter.Success(StatusCodes.Status200OK, "Transaction not found", null);

                return ResponseFormatter.Success(StatusCodes.Status200OK, "Transaction updated", updatedTransaction);
            }
            catch (Exception ex)
            {
                return ResponseFormatter.Failure(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            try
            {
                var deleted = await _transactionUseCase.DeleteTransactionAsync(id);
                if (!deleted)
                    return ResponseFormatter.Success(StatusCodes.Status200OK, "Transaction not found", null);

                return ResponseFormatter.Success(StatusCodes.Status200OK, "Transaction deleted", null);
            }
            catch (Exception ex)
            {
                return ResponseFormatter.Failure(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
    }
}
