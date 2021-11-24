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
    }
}
