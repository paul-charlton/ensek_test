using System.ComponentModel.DataAnnotations;
namespace Ensek.EnergyManager.Tests.UnitTests.Dtos;
public class MeterReadingDtoTests
{
    private const string _validAccountId = "12345";
    private const int _validReading = 6754;
    private const int _minValidReading = 0;
    private const int _maxValidReading = 99999;
    private static readonly DateTimeOffset _validDate = DateTimeOffset.Now;


    [Fact]
    public void Validate_InvalidAccountId_FailsValidation()
    {
        // SETUP
        var invalidAccountId = "";

        // TEST
        var reading = new MeterReadingDto(invalidAccountId, _validReading.ToString(), _validDate);
        var validation = reading.Validate(new ValidationContext(this));

        // ASSERT
        validation.Should().NotBeEmpty();

    }
    [Fact]
    public void Validate_NonNumericMeterReading_FailsValidation()
    {
        // SETUP
        var invalidReading = "XXXXX";

        // TEST
        var reading = new MeterReadingDto(_validAccountId, invalidReading, _validDate);
        var validation = reading.Validate(new ValidationContext(this));

        // ASSERT
        validation.Should().NotBeEmpty();
    }
    [Fact]
    public void Validate_NegativeMeterReading_FailsValidation()
    {
        // SETUP
        var negativeReading = "-123";

        // TEST
        var reading = new MeterReadingDto(_validAccountId, negativeReading, _validDate);
        var validation = reading.Validate(new ValidationContext(this));

        // ASSERT
        validation.Should().NotBeEmpty();
    }
    [Fact]
    public void Validate_MeterReadingOver5Characters_FailsValidation()
    {
        // SETUP
        var longReading = "123456";

        // TEST
        var reading = new MeterReadingDto(_validAccountId, longReading, _validDate);
        var validation = reading.Validate(new ValidationContext(this));

        // ASSERT
        validation.Should().NotBeEmpty();
    }
    [Fact]
    public void Validate_ValidMeterReadingAndAccount_PassesValidation()
    {
        // SETUP

        // TEST
        var reading = new MeterReadingDto(_validAccountId, _validReading.ToString(), _validDate);
        var validation = reading.Validate(new ValidationContext(this));

        // ASSERT
        validation.Should().BeEmpty();
    }
    [Fact]
    public void Validate_MinMeterReading_PassesValidation()
    {
        // SETUP

        // TEST
        var reading = new MeterReadingDto(_validAccountId, _minValidReading.ToString(), _validDate);
        var validation = reading.Validate(new ValidationContext(this));

        // ASSERT
        validation.Should().BeEmpty();
    }
    [Fact]
    public void Validate_MaxMeterReading_PassesValidation()
    {
        // SETUP

        // TEST
        var reading = new MeterReadingDto(_validAccountId, _maxValidReading.ToString(), _validDate);
        var validation = reading.Validate(new ValidationContext(this));

        // ASSERT
        validation.Should().BeEmpty();
    }


    [Fact]
    public void GetParsedMeterReading_WithNumericMeterReading_ReturnsCorrectValue()
    {
        // SETUP

        // TEST
        var reading = new MeterReadingDto(_validAccountId, _validReading.ToString(), _validDate);
        var parsed = reading.GetParsedMeterReading();

        // ASSERT
        parsed.Should().Be(_validReading);
    }


    [Fact]
    public void GetParsedMeterReading_NonNumericMeterReading_ThrowsValidationException()
    {
        // SETUP
        var invalidReading = "XXX";

        // TEST
        var reading = new MeterReadingDto(_validAccountId, invalidReading, _validDate);
        var action = () => reading.GetParsedMeterReading();

        // ASSERT
        action.Should().Throw<ValidationException>();
    }


}
