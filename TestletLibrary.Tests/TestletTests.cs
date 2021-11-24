namespace TestletLibrary.Tests
{
    using System;

    using FluentAssertions;

    using TestletLibrary.Tests.TestDataGenerator;

    using Xunit;

    public class TestletTests
    {
        [Fact]
        public void Constructor_InvokedWithValidArguments_CreateInstance()
        {
            // Arrange
            var testletId = Guid.NewGuid().ToString();
            var items = TestletItemsCollectionGenerator.Generate(4, 6);

            // Act
            var subject = new Testlet(testletId, items);

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
            var subject = new Testlet(Guid.NewGuid().ToString(), items);

            // Act
            var result = subject.Randomize();

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(10);
        }
    }
}
