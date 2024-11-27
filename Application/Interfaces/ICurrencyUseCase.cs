using DompifyAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DompifyAPI.Application.Interfaces
{
    public interface ICurrencyUseCase
    {
        Task<IEnumerable<Currency?>> GetCurrenciesAsync();
        Task<Currency?> GetCurrencyByIdAsync(int id);
        Task<Currency?> CreateCurrencyAsync(Currency currency);
        Task<Currency?> UpdateCurrencyAsync(Currency currency);
        Task<bool> DeleteCurrencyAsync(int id);
    }
}
