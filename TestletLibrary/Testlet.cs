namespace TestletLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Testlet
    {
        private const int LeadingPretestNumber = 2;

        public string TestletId;

        private List<Item> Items;

        public Testlet(string testletId, List<Item> items)
        {
            TestletId = testletId;
            Items = items;
        }

        public List<Item> Randomize()
        {
            var rnd = new Random();

            var pretest = this.Items
                .Where(item => item.ItemType == ItemTypeEnum.Pretest)
                .OrderBy(item => rnd.Next()).ToList(); // Randomizing first two of the Pretests.

            var ending = new List<Item>();
            ending.AddRange(pretest.Skip(LeadingPretestNumber));
            ending.AddRange(this.Items.Where(item => item.ItemType == ItemTypeEnum.Operational));


            var result = new List<Item>();
            result.AddRange(pretest.Take(LeadingPretestNumber));
            result.AddRange(ending.OrderBy(item => rnd.Next())); // Mixing the rest of Pretest and all the operational.
            return result;
        }
    }
}
