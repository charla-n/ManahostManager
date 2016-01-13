using ManahostManager.Domain.Entity;
using System;

namespace ManahostManager.Utils
{
    public static class ComputePrice
    {
        public static Decimal ComputePriceFromPercentOrAmount(Decimal basePrice, EValueType type, Decimal value)
        {
            return EValueType.PERCENT == type ? basePrice + ((basePrice * value) / 100M) : basePrice + value;
        }
    }
}