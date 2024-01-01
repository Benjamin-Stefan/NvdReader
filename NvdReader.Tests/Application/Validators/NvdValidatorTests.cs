using NvdReader.Application.Validators;
using NvdReader.Infrastructure.Exceptions;

namespace NvdReader.Tests.Application.Validators;

public class NvdValidatorTests
{
    [Fact]
    public void IsDateInRangeTrueTest()
    {
        // Arrange
        var dateTime = DateTime.Now;

        // Act
        var result = NvdValidator.IsDateInRange(dateTime);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsDateInRangeTrueWithExceptionParamTest()
    {
        // Arrange
        var dateTime = DateTime.Now;

        // Act
        var result = NvdValidator.IsDateInRange(dateTime, true);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsDateInRangeFalseTest()
    {
        // Arrange
        var dateTime = DateTime.Now.AddDays(-121);

        // Act
        var result = NvdValidator.IsDateInRange(dateTime);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void IsDateInRangeFalseWithExceptionParamTest()
    {
        // Arrange
        var dateTime = DateTime.Now.AddDays(-121);

        // Act and Assert
        Assert.Throws<DateNotInRangeException>(() => NvdValidator.IsDateInRange(dateTime, true));
    }
}