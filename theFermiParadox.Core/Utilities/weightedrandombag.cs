using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace theFermiParadox.Core.Utilities
{
    /// <summary>
    /// Represents a bag of weighted values, allowing random selection based on their respective weights.
    /// </summary>
    /// <typeparam name="T">The type of items contained in the bag.</typeparam>
    class WeightedRandomBag<T>
    {
        private struct Entry
        {
            public double accumulatedWeight;
            public T item;
        }

        private List<Entry> entries = new List<Entry>();
        private double accumulatedWeight;
        private Random rand = new Random();

        /// <summary>
        /// Adds an item to the bag with a specified weight.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <param name="weight">The weight of the item, used to determine its probability of being selected.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the weight is less than or equal to zero.</exception>
        public void AddEntry(T item, double weight)
        {
            if (weight <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(weight), "Weight must be greater than zero.");
            }
            accumulatedWeight += weight;
            entries.Add(new Entry { item = item, accumulatedWeight = accumulatedWeight });
        }

        /// <summary>
        /// Randomly selects an item from the bag based on their respective weights.
        /// </summary>
        /// <returns>The selected item, or the default value of <typeparamref name="T"/> if the bag is empty.</returns>
        public T GetRandom()
        {
            if (!entries.Any())
            {
                return default;
            }
            double r = rand.NextDouble() * accumulatedWeight;

            foreach (Entry entry in entries)
            {
                if (entry.accumulatedWeight >= r)
                {
                    return entry.item;
                }
            }
            return default(T); //should only happen when there are no entries
        }
    }
}
