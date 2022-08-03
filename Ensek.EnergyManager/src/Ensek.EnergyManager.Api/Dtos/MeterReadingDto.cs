using Ensek.EnergyManager.Api.Validation;
using FileHelpers;
using System.ComponentModel.DataAnnotations;

namespace Ensek.EnergyManager.Api.Dtos;

internal record MeterReadingDto() : IValidatableObject
{
    public MeterReadingDto(string accountId, string meterReadValue, DateTime meterReadingDateTime) : this()
    {
        AccountId = accountId;
        MeterReadValue = meterReadValue;
        MeterReadingDateTime = meterReadingDateTime;
    }
    public string AccountId { get; init; } = "";

    public string MeterReadValue { get; init; } = "";

    public DateTime MeterReadingDateTime { get; init; }


    public int GetParsedMeterReading()
    {
        if (int.TryParse(MeterReadValue, out var result))
            return result;

        throw new ValidationException("Meter Reading can't be processed");
    }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(AccountId))
            yield return new ValidationResult("Account Id is invalid");

        if (!int.TryParse(MeterReadValue, out var reading))
            yield return new ValidationResult("Meter Reading can't be processed");

        if (reading < MeterReadingConstants.MinimumMeterReading)
            yield return new ValidationResult("Meter Reading is too low");

        if (reading > MeterReadingConstants.MaximumMeterReading)
            yield return new ValidationResult("Meter Reading is too high");
    }
}