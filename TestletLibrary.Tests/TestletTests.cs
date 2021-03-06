namespace TestletLibrary.Tests
{
    using System;
    using System.Linq;

    using FluentAssertions;

    using TestletLibrary.Tests.Stubs;
    using TestletLibrary.Tests.TestDataGenerator;

    using Xunit;

    public class TestletTests
    {
        private readonly int RandomSeed = 8; // Just because I like the number.

        [Fact]
        public void Constructor_IdIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            var items = TestletItemsCollectionGenerator.Generate(4, 6);
            var randomizer = new RandomizerStub(this.RandomSeed);

            // Act
            Action act = () => new Testlet(null, items, randomizer);

            // Assert
            act.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'testletId')");
        }

        [Fact]
        public void Constructor_InvokedWithValidArguments_CreateInstance()
        {
            // Arrange
            var testletId = Guid.NewGuid().ToString();
            var items = TestletItemsCollectionGenerator.Generate(4, 6);
            var randomizer = new RandomizerStub(this.RandomSeed);

            // Act
            var subject = new Testlet(testletId, items, randomizer);

            // Assert
            subject.Should().NotBeNull();
            subject.TestletId.Should().Be(testletId);
        }

        [Fact]
        public void Constructor_ItemCollectionIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            var testletId = Guid.NewGuid().ToString();
            var randomizer = new RandomizerStub(this.RandomSeed);

            // Act
            Action act = () => new Testlet(testletId, null, randomizer);

            // Assert
            act.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'items')");
        }

        [Theory]
        [InlineData(ItemType.Pretest, 3, 6)]
        [InlineData(ItemType.Pretest, 5, 6)]
        [InlineData(ItemType.Operational, 4, 5)]
        [InlineData(ItemType.Operational, 4, 7)]
        public void Constructor_NumberOfItemsIsWrong_ThrowsArgumentException(
            ItemType itemType,
            int numberOfPretest,
            int numberOfOperational)
        {
            // Arrange
            var expectedNumber = itemType == ItemType.Pretest ? 4 : 6;

            var testletId = Guid.NewGuid().ToString();
            var items = TestletItemsCollectionGenerator.Generate(numberOfPretest, numberOfOperational);
            var randomizer = new RandomizerStub(this.RandomSeed);

            // Act
            Action act = () => new Testlet(testletId, items, randomizer);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage($"Number of {itemType} items should be {expectedNumber}.");
        }

        [Fact]
        public void Constructor_RandomizerIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            var testletId = Guid.NewGuid().ToString();
            var items = TestletItemsCollectionGenerator.Generate(4, 6);

            // Act
            Action act = () => new Testlet(testletId, items, null);

            // Assert
            act.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'randomizer')");
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

        [Fact]
        public void Randomize_Invoke_ReturnsSetOfItems()
        {
            // Arrange
            var items = TestletItemsCollectionGenerator.Generate(4, 6);
            var subject = new Testlet(Guid.NewGuid().ToString(), items, new RandomizerStub(this.RandomSeed));

            // Act
            var result = subject.Randomize();

            // Assert
            result.Should().NotBeNull().And.HaveCount(10);
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
            result[0].ItemType.Should().Be(ItemType.Pretest);
            result[1].ItemType.Should().Be(ItemType.Pretest);
        }

        [Fact]
        public void Randomize_Invoke_LastEightAreTwoPretestAndSixOperational()
        {
            // Arrange
            var items = TestletItemsCollectionGenerator.GenerateOperationalFirst(4, 6);
            var subject = new Testlet(Guid.NewGuid().ToString(), items, new RandomizerStub(this.RandomSeed));

            // Act
            var result = subject.Randomize();

            // Assert
            var lastEight = result.Skip(2);
            lastEight.Count(item => item.ItemType == ItemType.Pretest).Should().Be(2);
            lastEight.Count(item => item.ItemType == ItemType.Operational).Should().Be(6);
        }
    }
}