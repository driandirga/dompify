using DompifyAPI.Application.Interfaces;
using DompifyAPI.Domain.Interfaces;
using DompifyAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DompifyAPI.Application.UseCases
{
    public class CurrencyUseCase(ICurrencyRepository currencyRepository) : ICurrencyUseCase
    {
        private readonly ICurrencyRepository _currencyRepository = currencyRepository;

        public async Task<IEnumerable<Currency?>> GetCurrenciesAsync()
        {
            return await _currencyRepository.GetCurrencies();
        }

        public async Task<Currency?> GetCurrencyByIdAsync(int id)
        {
            return await _currencyRepository.GetCurrencyById(id);
        }

        public async Task<Currency?> CreateCurrencyAsync(Currency currency)
        {
            if (string.IsNullOrEmpty(currency.Name))
                throw new ArgumentException("Currency name cannot be empty.");

            var existingCurrency = await _currencyRepository.GetCurrencyByName(currency.Name);
            if (existingCurrency != null)
                throw new ArgumentException("Currency name already exists.");

            currency.CreatedAt = DateTime.UtcNow;
            currency.CreatedBy = "SYSTEM";

            return await _currencyRepository.CreateCurrency(currency);
        }

        public async Task<Currency?> UpdateCurrencyAsync(Currency currency)
        {
            var existingCurrency = await _currencyRepository.GetCurrencyById(currency.Id);
            if (existingCurrency == null) 
                return null;

            var existingCurrencyByName = await _currencyRepository.GetCurrencyByName(currency.Name!); ;
            if (existingCurrencyByName != null)
                throw new ArgumentException("Currency name already exists.");

            existingCurrency.Name = currency.Name ?? existingCurrency.Name;
            existingCurrency.Code = currency.Code ?? existingCurrency.Code;
            existingCurrency.Symbol = currency.Symbol ?? existingCurrency.Symbol;
            existingCurrency.ExchangeRate = currency.ExchangeRate ?? existingCurrency.ExchangeRate;
            existingCurrency.IsActive = currency.IsActive ?? existingCurrency.IsActive;
            existingCurrency.UpdatedAt = DateTime.UtcNow;
            existingCurrency.UpdatedBy = "SYSTEM";

            return await _currencyRepository.UpdateCurrency(existingCurrency);
        }

        public async Task<bool> DeleteCurrencyAsync(int id)
        {
            var currency = await _currencyRepository.GetCurrencyById(id);
            if (currency == null) 
                return false;

            return await _currencyRepository.DeleteCurrency(currency);
        }
    }
}
