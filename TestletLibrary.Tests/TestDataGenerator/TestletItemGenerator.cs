namespace TestletLibrary.Tests.TestDataGenerator
{
    using System;

    internal static class TestletItemGenerator
    {
        public static Item Generate(ItemTypeEnum type)
        {
            return new() { ItemId = Guid.NewGuid().ToString(), ItemType = type };
        }
    }
}
