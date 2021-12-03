namespace TestletLibrary.Tests.Stubs
{
    using System;

    internal class RandomizerStub : IRandomizer
    {
        private readonly Random random;

        public RandomizerStub(int seed)
        {
            this.random = new Random(seed);
        }

        public int NextInt()
        {
            return this.random.Next();
        }
    }
}