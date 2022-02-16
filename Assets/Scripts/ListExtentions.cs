using System.Collections.Generic;
using Interfaces;

public static class ListExtentions
{
    private static System.Random _random = new System.Random();
    public static T Substitute<T>(this List<T> from, List<T> to, T element)
    {
        if (from.Contains(element) == false) return element;
        from.Remove(element);
        to.Add(element);
        return element;
    }

    public static T RandomByChance<T>(this List<T> vals) where T: IRandom
    {
        var total = 0f;
        var probs = new float[vals.Count];

        for (int i = 0; i < probs.Length; i++)
        {
            probs[i] = vals[i].ReturnChance;
            total += probs[i];
        }

        var randomPoint = (float) _random.NextDouble() * total;
        
        for (int i = 0; i < probs.Length; i++)
        {
            if (randomPoint< probs[i]) return vals[i];
            randomPoint -= probs[i];
        }

        return vals[0];
    }
}
