using System.Text.Json;
using Domain.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.External
{
    public class CurrenciesApi(IMemoryCache cache, HttpClient httpClient)
    {
        private readonly IMemoryCache _cache = cache;
        private readonly HttpClient _httpClient = httpClient;

        private const string Cachekey = "currencyData";

        public List<Currency> GetCurrencies()
        {
            if (!_cache.TryGetValue(Cachekey, out List<Currency>? currencies))
            {
                var response = _httpClient.GetStringAsync("https://economia.awesomeapi.com.br/all").GetAwaiter().GetResult();
                var result = JsonSerializer.Deserialize<Dictionary<string, Currency>>(response);
                currencies = result.Values.Select(c => new Currency
                {
                    Code = c.Code,
                    codein = c.codein,
                    name = c.name,
                    high = c.high,
                    low = c.low,
                    varBid = c.varBid,
                    pctChange = c.pctChange,
                    bid = c.bid,
                    ask = c.ask,
                    create_date = c.create_date
                }).ToList() ?? new List<Currency>();

                var expiration = DateTime.Today.AddDays(1).AddSeconds(-1) - DateTime.Now;
                _cache.Set(Cachekey, currencies, expiration);
            }
            return currencies;
        }
    }
}