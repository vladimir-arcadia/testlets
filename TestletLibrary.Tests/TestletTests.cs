namespace TestletLibrary.Tests
{
    using System;

    using FluentAssertions;

    using TestletLibrary.Tests.Stubs;
    using TestletLibrary.Tests.TestDataGenerator;

    using Xunit;

    public class TestletTests
    {
        private int RandomSeed = 8; // Just because I like the number.

        [Fact]
        public void Constructor_InvokedWithValidArguments_CreateInstance()
        {
            // Arrange
            var testletId = Guid.NewGuid().ToString();
            var items = TestletItemsCollectionGenerator.Generate(4, 6);

            // Act
            var subject = new Testlet(testletId, items, new RandomizerStub(this.RandomSeed));

            // Assert
            subject.Should()
                .NotBeNull()
                .And.BeOfType<Testlet>();
            subject.TestletId.Should().Be(testletId);
        }
        
        [Fact]
        public void Randomize_Invoke_ReturnsSetOfItems()
        {
            // Arrange
            var items = TestletItemsCollectionGenerator.Generate(4, 6);
            var subject = new Testlet(Guid.NewGuid().ToString(), items, new RandomizerStub(this.RandomSeed));

            // Act
            var result = subject.Randomize();

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(10);
        }

        [Fact]
        public void Randomize_Invoke_TwoFirstArePretest()
        {
            // Arrange
            var items = TestletItemsCollectionGenerator.GenerateOperationalFirst(4, 6);
            var subject = new Testlet(Guid.NewGuid().ToString(), items, new RandomizerStub(this.RandomSeed));

            // Act
            var result = subject.Randomize();

            // Assert
            result[0].ItemType.Should().Be(ItemTypeEnum.Pretest);
            result[1].ItemType.Should().Be(ItemTypeEnum.Pretest);
        }

        [Fact]
        public void Randomize_Invoke_RandomizesItems()
        {
            // Arrange
            var items = TestletItemsCollectionGenerator.Generate(4, 6);
            var subject = new Testlet(Guid.NewGuid().ToString(), items, new RandomizerStub(this.RandomSeed));

            // Act
            var result = subject.Randomize();

            // Assert
            result.Should().BeEquivalentTo(items).And.NotBeEquivalentTo(items, opt => opt.WithStrictOrdering());
        }
    }
}
