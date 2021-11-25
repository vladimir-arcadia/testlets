namespace TestletLibrary
{
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
            var pretest = this.Items.Where(item => item.ItemType == ItemTypeEnum.Pretest).ToList();
            var operational = this.Items.Where(item => item.ItemType == ItemTypeEnum.Operational);

            var result = new List<Item>();
            result.AddRange(pretest.Take(LeadingPretestNumber));
            result.AddRange(pretest.Skip(LeadingPretestNumber));
            result.AddRange(operational);
            return result;

            //Items private collection has 6 Operational and 4 Pretest Items. Randomize the order of these items as per the requirement (with TDD)
            //The assignment will be reviewed on the basis of – Tests written first, Correct logic, Well structured & clean readable code.
        }
    }
}
