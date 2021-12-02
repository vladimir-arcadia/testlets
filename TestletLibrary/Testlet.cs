namespace TestletLibrary
{
    using System.Collections.Generic;
    using System.Linq;

    public class Testlet
    {
        private const int LeadingPretestNumber = 2;

        public string TestletId;

        private readonly List<Item> items;

        private readonly IRandomizer rnd;

        public Testlet(string testletId, List<Item> items, IRandomizer rnd)
        {
            this.TestletId = testletId;
            this.items = items;

            // When we use some concrete implementation of the randomizer we can not control test environment.
            // To prevent occasional test fails we should delegate randomization to something that we can control.
            this.rnd = rnd;
        }

        public List<Item> Randomize()
        {
            var randomized = this.items.OrderBy(item => this.rnd.NextInt()).ToList();

            var result = new List<Item>();
            result.AddRange(randomized.Where(item => item.ItemType == ItemTypeEnum.Pretest).Take(LeadingPretestNumber));
            result.AddRange(randomized.Where(item => !result.Contains(item)));
            return result;
        }
    }
}
