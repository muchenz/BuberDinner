using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Domain.Common.Models;


public abstract class ValueObject:IEquatable<ValueObject>
{
    public abstract IEnumerable<object> GetEqualityComponent();
    public override bool Equals(object? obj)
    {
        if(obj is null || obj.GetType() != GetType())
        {
            return false;
        }

        var valueObject = (ValueObject)obj;
        return GetEqualityComponent().SequenceEqual(valueObject.GetEqualityComponent()); 
    }

    public static bool operator ==(ValueObject left, ValueObject right)
    {
        return Equals(left, right);
    }
    public static bool operator !=(ValueObject left, ValueObject right)
    {
        return !Equals(left, right);
    }

    public override int GetHashCode()
    {
        return GetEqualityComponent().Select(a => a?.GetHashCode() ?? 0).Aggregate((a, b) => a ^ b);
    }

    public bool Equals(ValueObject? other)
    {
        return Equals((object?)other); 
    }

    public override string ToString()
    {
        return string.Join(", ", GetEqualityComponent());
    }
}