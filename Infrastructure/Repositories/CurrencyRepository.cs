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
    public class CurrencyRepository(ApplicationDbContext context) : ICurrencyRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<IEnumerable<Currency?>> GetCurrencies()
        {
            return await _context.Currencies.ToListAsync();
        }

        public async Task<Currency?> GetCurrencyById(int id)
        {
            return await _context.Currencies.FindAsync(id);
        }

        public async Task<Currency?> GetCurrencyByName(string name)
        {
            return await _context.Currencies.FirstOrDefaultAsync(c => c.Name == name);
        }

        public async Task<Currency?> CreateCurrency(Currency currency)
        {
            _context.Currencies.Add(currency);
            await _context.SaveChangesAsync();

            return currency;
        }

        public async Task<Currency?> UpdateCurrency(Currency currency)
        {
            _context.Currencies.Update(currency);
            await _context.SaveChangesAsync();

            return currency;
        }

        public async Task<bool> DeleteCurrency(Currency currency)
        {
            _context.Currencies.Remove(currency);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
