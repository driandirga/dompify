using DompifyAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DompifyAPI.Domain.Interfaces
{
    public interface ICurrencyRepository
    {
        Task<IEnumerable<Currency?>> GetCurrencies();
        Task<Currency?> GetCurrencyById(int id);
        Task<Currency?> GetCurrencyByName(string name);
        Task<Currency?> CreateCurrency(Currency currency);
        Task<Currency?> UpdateCurrency(Currency currency);
        Task<bool> DeleteCurrency(Currency currency);
    }
}
