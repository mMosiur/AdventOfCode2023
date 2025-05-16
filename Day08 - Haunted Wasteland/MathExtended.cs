using System.Diagnostics.Contracts;

namespace AdventOfCode.Year2023.Day08;

internal static class MathExtended
{
    [Pure]
    public static ulong LeastCommonMultiple(IEnumerable<ulong> values)
    {
        return values.Aggregate(LeastCommonMultiple);
    }

    [Pure]
    public static ulong LeastCommonMultiple(ulong a, ulong b)
    {
        return a * (b / GreatestCommonDivisor(a, b));
    }

    [Pure]
    public static ulong GreatestCommonDivisor(ulong u, ulong v)
    {
        // Base cases: gcd(n, 0) = gcd(0, n) = n
        if (u == 0)
            return v;
        if (v == 0)
            return u;

        // Using identities 2 and 3:
        // gcd(2ⁱ u, 2ʲ v) = 2ᵏ gcd(u, v) with u, v odd and k = min(i, j)
        // 2ᵏ is the greatest power of two that divides both 2ⁱ u and 2ʲ v
        int i = (int)ulong.TrailingZeroCount(u);
        u >>= i;
        int j = (int)ulong.TrailingZeroCount(v);
        v >>= j;
        int k = Math.Min(i, j);

        while (true)
        {
            // Swap if necessary so u ≤ v
            if (u > v)
            {
                (u, v) = (v, u);
            }

            // Identity 4: gcd(u, v) = gcd(u, v-u) as u ≤ v and u, v are both odd
            v -= u;
            // v is now even

            if (v == 0)
            {
                // Identity 1: gcd(u, 0) = u
                // The shift by k is necessary to add back the 2ᵏ factor that was removed before the loop
                return u << k;
            }

            // Identity 3: gcd(u, 2ʲ v) = gcd(u, v) as u is odd
            v >>= (int)ulong.TrailingZeroCount(v);
        }
    }
}
