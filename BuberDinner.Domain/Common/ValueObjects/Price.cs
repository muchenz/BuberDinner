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
    public decimal Amount { get; }
    public Currency Currency { get; }

    private Price(decimal value, Currency currency)
    {
        Amount = value;
        Currency = currency;
    }

    public static Price CreateNew(decimal value, Currency currency) => new Price(value, currency);

    public override IEnumerable<object> GetEqualityComponent()
    {
        yield return Amount;
        yield return Currency;
    }
}

