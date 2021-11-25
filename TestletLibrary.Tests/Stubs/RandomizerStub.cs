namespace TestletLibrary.Tests.Stubs
{
    using System;

    internal class RandomizerStub : IRandomizer
    {
        private Random rnd;

        public RandomizerStub(int seed)
        {
            this.rnd = new Random(seed);
        }

        public int NextInt()
        {
            return this.rnd.Next();
        }
    }
}
