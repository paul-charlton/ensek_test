using System.ComponentModel.DataAnnotations;

namespace Ensek.EnergyManager.Tests.UnitTests.Domain;

public class MeterReadingEntityTests
{
    private readonly AccountEntity _account;

    public MeterReadingEntityTests()
    {
        _account = new AccountEntity("12345", new Name("Test", "Test"));
    }

    [Fact]
    public void Constructor_WithNegativeMeterReading_ThrowsException()
    {
        // SETUP
        var reading = -556;

        // TEST
        var action = () => new MeterReadingEntity(_account, DateTimeOffset.Now, reading);

        // ASSERT
        action.Should().Throw<ValidationException>();
    }

    [Fact]
    public void Constructor_WithValidMeterReading_ExecutesSuccessfully()
    {
        // SETUP
        var reading = 556;

        // TEST
        var meter = new MeterReadingEntity(_account, DateTimeOffset.Now, reading);

        // ASSERT
        meter.Should().NotBeNull();
        meter.MeterReadingValue.Should().Be(reading);
    }

}