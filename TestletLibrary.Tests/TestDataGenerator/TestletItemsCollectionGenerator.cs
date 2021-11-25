namespace TestletLibrary.Tests.TestDataGenerator
{
    using System.Collections.Generic;
    using System.Linq;

    internal static class TestletItemsCollectionGenerator
    {
        public static List<Item> Generate(int numberOfPretest, int numberOfOperational)
        {
            var result = new Item[numberOfPretest + numberOfOperational];

            FillArray(0, numberOfPretest, ItemTypeEnum.Pretest, result);
            FillArray(numberOfPretest, numberOfOperational, ItemTypeEnum.Operational, result);

            return result.ToList();
        }

        public static List<Item> GenerateOperationalFirst(int numberOfPretest, int numberOfOperational)
        {
            var result = new Item[numberOfPretest + numberOfOperational];

            FillArray(0, numberOfOperational, ItemTypeEnum.Operational, result);
            FillArray(numberOfOperational, numberOfPretest, ItemTypeEnum.Pretest, result);

            return result.ToList();
        }

        private static void FillArray(int offset, int number, ItemTypeEnum type, Item[] array)
        {
            for (int i = offset; i < offset + number; i++)
            {
                array[i] = TestletItemGenerator.Generate(type);
            }
        }
    }
}
