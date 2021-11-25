namespace TestletLibrary
{
    using System.Collections.Generic;
    using System.Linq;

    public class Testlet
    {
        private const int LeadingPretestNumber = 2;

        public string TestletId;

        private List<Item> Items;

        private IRandomizer rnd;

        public Testlet(string testletId, List<Item> items, IRandomizer rnd)
        {
            TestletId = testletId;
            Items = items;

            // When we use some concrete implementation of the randomizer we can not control test environment.
            // To prevent occasional test fails we should delegate randomization to something that we can control.
            this.rnd = rnd;
        }

        public List<Item> Randomize()
        {
            var pretest = this.Items
                .Where(item => item.ItemType == ItemTypeEnum.Pretest)
                .OrderBy(item => this.rnd.NextInt()).ToList(); // Randomizing first two of the Pretests.

            var ending = new List<Item>();
            ending.AddRange(pretest.Skip(LeadingPretestNumber));
            ending.AddRange(this.Items.Where(item => item.ItemType == ItemTypeEnum.Operational));


            var result = new List<Item>();
            result.AddRange(pretest.Take(LeadingPretestNumber));
            result.AddRange(ending.OrderBy(item => this.rnd.NextInt())); // Mixing the rest of Pretest and all the operational.
            return result;
        }
    }
}
