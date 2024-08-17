using System.Collections.Generic;
using Godot;

using static ExtendedRandom.ExtendedRandom;

namespace ListExtensions {

    /// <summary>
    /// Extensions for IList.
    /// </summary>
    public static class IListExtensions {

        /// <summary>
        /// Randomly select an item from <paramref name="list" />.
        /// </summary>
        /// <param name="list">List of items to choose from.</param>
        /// <param name="rng">RandomNumberGenerator to use. If null, one will be created.</param>
        /// <typeparam name="T">Type of item in <paramref name="list" />.</typeparam>
        /// <returns>Selected item from <paramref name="list" /> or default(<typeparamref name="T" />) if <paramref name="list" /> is null or empty.</returns>
        public static T GetRandomItem<T> (this IList<T> list, RandomNumberGenerator rng = null) {
            if (list == null || list.Count == 0) return default;
            if (list.Count == 1) return list[0];

            RandomNumberGenerator randomNumberGenerator = rng ?? CreateRNG();

            return list[randomNumberGenerator.RandiRange(0, list.Count - 1)];
        }

        /// <summary>
        /// Randomly select an item from <paramref name="list" /> using the weights in <paramref name="weights" /> given for each object. An object with a non-positive weight (weight &lt;= 0) will not be selected.
        /// </summary>
        /// <param name="list">List of items of type :<typeparamref name="T" />.</param>
        /// <param name="weights">Weights for each object.</param>
        /// <param name="rng">RandomNumberGenerator to use. If null, one will be created.</param>
        /// <typeparam name="T">Type of items in <paramref name="list" />.</typeparam>
        /// <returns>Selected item from <paramref name="list" /> or default(<typeparamref name="T" />) if <paramref name="list" /> or <paramref name="weights" /> is null or empty, the list lengths do not match, or all items have non-positive weight (weight &lt;= 0).</returns>
        public static T GetRandomItemWeighted<T> (this IList<T> list, IList<int> weights, RandomNumberGenerator rng = null) {
            if (list == null || weights == null || list.Count == 0 || weights.Count == 0 || list.Count != weights.Count) return default;
            if (list.Count == 1) return weights[0] > 0 ? list[0] : default;

            RandomNumberGenerator randomNumberGenerator = rng ?? CreateRNG();

            int totalWeight = 0;
            for (int index = 0; index < weights.Count; index++) {
                if (weights[index] <= 0) continue;
                totalWeight += weights[index];
            }

            if (totalWeight == 0) return default;
            int resultValue = randomNumberGenerator.RandiRange(0, totalWeight - 1);
            int resultIndex = 0;

            while (resultIndex < list.Count && resultValue >= weights[resultIndex]) {
                if (weights[resultIndex] <= 0) continue;
                resultValue -= weights[resultIndex];
                resultIndex++;
            }

            if (resultIndex >= list.Count) return default;
            return list[resultIndex];
        }
    }
}