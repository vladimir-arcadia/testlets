namespace TestletLibrary.Tests.TestDataGenerator
{
    using System.Collections.Generic;
    using System.Linq;

    internal static class TestletItemsCollectionGenerator
    {
        public static List<Item> Generate(int numberOfPretest, int numberOfOperational)
        {
            var result = new Item[numberOfPretest + numberOfOperational];

            FillArray(0, numberOfPretest, ItemType.Pretest, result);
            FillArray(numberOfPretest, numberOfOperational, ItemType.Operational, result);

            return result.ToList();
        }

        public static List<Item> GenerateOperationalFirst(int numberOfPretest, int numberOfOperational)
        {
            var result = new Item[numberOfPretest + numberOfOperational];

            FillArray(0, numberOfOperational, ItemType.Operational, result);
            FillArray(numberOfOperational, numberOfPretest, ItemType.Pretest, result);

            return result.ToList();
        }

        private static void FillArray(int offset, int number, ItemType type, Item[] array)
        {
            for (var i = offset; i < offset + number; i++) array[i] = TestletItemGenerator.Generate(type);
        }
    }
}