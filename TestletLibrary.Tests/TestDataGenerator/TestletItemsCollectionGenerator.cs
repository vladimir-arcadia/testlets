namespace TestletLibrary.Tests.TestDataGenerator
{
    using System.Collections.Generic;
    using System.Linq;

    internal static class TestletItemsCollectionGenerator
    {
        public static List<Item> Generate(int numberOfPretest, int numberOfOperational)
        {
            var result = new Item[numberOfPretest + numberOfOperational];

            for (int i = 0; i < numberOfPretest; i++)
            {
                result[i] = TestletItemGenerator.Generate(ItemTypeEnum.Pretest);
            }

            for (int i = 0; i < numberOfOperational; i++)
            {
                result[i + numberOfPretest] = TestletItemGenerator.Generate(ItemTypeEnum.Operational);
            }

            return result.ToList();
        }
    }
}
