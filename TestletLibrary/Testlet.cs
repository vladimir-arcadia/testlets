namespace TestletLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Testlet
    {
        private const int LeadingPretestNumber = 2;

        private const int NumberOfOperationalExpected = 6;

        private const int NumberOfPretestExpected = 4;

        private readonly List<Item> items;

        private readonly IRandomizer randomizer;

        public Testlet(string testletId, List<Item> items, IRandomizer randomizer)
        {
            this.TestletId = testletId ?? throw new ArgumentNullException(nameof(testletId));

            if (items == null) throw new ArgumentNullException(nameof(items));

            ValidateItemQuantity(items, ItemType.Pretest, NumberOfPretestExpected);
            ValidateItemQuantity(items, ItemType.Operational, NumberOfOperationalExpected);
            this.items = items;

            // When we use some concrete implementation of the randomizer we can not control test environment.
            // To prevent occasional test fails we should delegate randomization to something that we can control.
            this.randomizer = randomizer ?? throw new ArgumentNullException(nameof(randomizer));
        }

        public string TestletId { get; }

        public List<Item> Randomize()
        {
            var randomized = this.items.OrderBy(item => this.randomizer.NextInt()).ToList();

            var result = new List<Item>();
            result.AddRange(randomized.Where(item => item.ItemType == ItemType.Pretest).Take(LeadingPretestNumber));
            result.AddRange(randomized.Where(item => !result.Contains(item)));
            return result;
        }

        private static void ValidateItemQuantity(IEnumerable<Item> items, ItemType itemType, int expectedNumber)
        {
            if (items.Where(item => item.ItemType == itemType).ToList().Count != expectedNumber)
                throw new ArgumentException($"Number of {itemType} items should be {expectedNumber}.");
        }
    }
}