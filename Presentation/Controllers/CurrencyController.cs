using DompifyAPI.Application.Interfaces;
using DompifyAPI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DompifyAPI.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrenciesController(ICurrencyUseCase currencyUseCase) : Controller
    {
        private readonly ICurrencyUseCase _currencyUseCase = currencyUseCase;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Currency>>> GetCurrencies()
        {
            var currencies = await _currencyUseCase.GetCurrenciesAsync();

            return Ok(currencies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Currency>> GetCurrencyById(int id)
        {
            var currency = await _currencyUseCase.GetCurrencyByIdAsync(id);
            if (currency == null) 
                return NotFound();

            return Ok(currency);
        }

        [HttpPost]
        public async Task<ActionResult<Currency>> CreateCurrency([FromBody] Currency currency)
        {
            var createdCurrency = await _currencyUseCase.CreateCurrencyAsync(currency);

            return CreatedAtAction(nameof(GetCurrencyById), new { id = createdCurrency?.Id }, createdCurrency);
        }

        [HttpPost]
        public async Task<ActionResult<Currency>> UpdateCurrency(int id, [FromBody] Currency currency)
        {
            currency.Id = id;
            var updatedCurrency = await _currencyUseCase.UpdateCurrencyAsync(currency);
            if (updatedCurrency == null) 
                return NotFound();

            return Ok(updatedCurrency);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCurrency(int id)
        {
            var result = await _currencyUseCase.DeleteCurrencyAsync(id);
            if (!result) 
                return NotFound();

            return NoContent();
        }
    }
}
