using System;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface ICurrenciesService
    {
        Currency GetCurrencyByCode(string code);

        IEnumerable<Currency> GetAllCurrencies();

    }
}