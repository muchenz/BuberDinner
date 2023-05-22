using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Dinner.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BuberDinner.Domain.Common.Enums.Enums;

namespace BuberDinner.Domain.Common.ValueObjects;
public sealed class Price : ValueObject
{
    public decimal Amount { get; private set; }
    public Currency Currency { get; private set; }

    private Price(decimal amount, Currency currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public static Price CreateNew(decimal value, Currency currency) => new Price(value, currency);

    public override IEnumerable<object> GetEqualityComponent()
    {
        yield return Amount;
        yield return Currency;
    }
}

