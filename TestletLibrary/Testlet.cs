namespace TestletLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Testlet
    {
        private const int NumberOfPretestExpected = 4;

        private const int NumberOfOperationalExpected = 6;

        private const int LeadingPretestNumber = 2;

        public string TestletId { get; }

        private readonly List<Item> items;

        private readonly IRandomizer rnd;

        public Testlet(string testletId, List<Item> items, IRandomizer rnd)
        {
            this.TestletId = testletId ?? throw new ArgumentNullException(nameof(testletId));

            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }
            else
            {
                ValidateItemQuantity(items, ItemTypeEnum.Pretest, NumberOfPretestExpected);
                ValidateItemQuantity(items, ItemTypeEnum.Operational, NumberOfOperationalExpected);
                this.items = items;
            }

            // When we use some concrete implementation of the randomizer we can not control test environment.
            // To prevent occasional test fails we should delegate randomization to something that we can control.
            this.rnd = rnd ?? throw new ArgumentNullException(nameof(rnd));
        }

        public List<Item> Randomize()
        {
            var randomized = this.items.OrderBy(item => this.rnd.NextInt()).ToList();

            var result = new List<Item>();
            result.AddRange(randomized.Where(item => item.ItemType == ItemTypeEnum.Pretest).Take(LeadingPretestNumber));
            result.AddRange(randomized.Where(item => !result.Contains(item)));
            return result;
        }

        private static void ValidateItemQuantity(IEnumerable<Item> items, ItemTypeEnum itemType, int expectedNumber)
        {
            if (items.Where(item => item.ItemType == itemType).ToList().Count != expectedNumber)
            {
                throw new ArgumentException(
                    $"Number of {itemType} items should be {expectedNumber}.");
            }
        }
    }
}
