using Ensek.EnergyManager.Api.Validation;
using System.ComponentModel.DataAnnotations;

namespace Ensek.EnergyManager.Tests.UnitTests.Domain;
public class AccountEntityTests
{
    private readonly AccountEntity _account;

    public AccountEntityTests()
    {
        _account = new AccountEntity("12345", new Name("Test", "Test"));
    }

    [Fact]
    public void AddMeterReading_WithNegativeMeterReading_ThrowsValidationException()
    {
        // SETUP
        var readng = -667;

        // TEST
        var action = () => _account.AddMeterReading(readng, DateTimeOffset.Now);

        // ASSERT
        action.Should().Throw<ValidationException>();
    }


    [Fact]
    public void AddMeterReading_WithValidMeterReading_AddsToCollection()
    {
        // SETUP
        var readng = 567;

        // TEST
        _account.AddMeterReading(readng, DateTimeOffset.Now);

        // ASSERT
        _account.MeterReadings.Should().HaveCount(1);
        _account.MeterReadings.First().MeterReadingValue.Should().Be(readng); 
    }


    [Fact]
    public void AddMeterReading_WithMinMeterReading_AddsToCollection()
    {
        // SETUP
        var readng = MeterReadingConstants.MinimumMeterReading;

        // TEST
        _account.AddMeterReading(readng, DateTimeOffset.Now);

        // ASSERT
        _account.MeterReadings.Should().HaveCount(1);
        _account.MeterReadings.First().MeterReadingValue.Should().Be(readng);
    }
    [Fact]
    public void AddMeterReading_WithMaxMeterReading_AddsToCollection()
    {
        // SETUP
        var readng = MeterReadingConstants.MaximumMeterReading;

        // TEST
        _account.AddMeterReading(readng, DateTimeOffset.Now);

        // ASSERT
        _account.MeterReadings.Should().HaveCount(1);
        _account.MeterReadings.First().MeterReadingValue.Should().Be(readng);
    }
    [Fact]
    public void AddMeterReading_With1UnderMinMeterReading_ThrowsValidationException()
    {
        // SETUP
        var readng = MeterReadingConstants.MinimumMeterReading -1;

        // TEST
        var action = ()=> _account.AddMeterReading(readng, DateTimeOffset.Now);

        // ASSERT
        action.Should().Throw<ValidationException>();
    }
    [Fact]
    public void AddMeterReading_With1OverMaxMeterReading_ThrowsValidationException()
    {
        // SETUP
        var readng = MeterReadingConstants.MaximumMeterReading + 1;

        // TEST
        var action = () => _account.AddMeterReading(readng, DateTimeOffset.Now);

        // ASSERT
        action.Should().Throw<ValidationException>();
    }

}
