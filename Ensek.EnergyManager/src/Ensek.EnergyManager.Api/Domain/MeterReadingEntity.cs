using System.ComponentModel.DataAnnotations;

namespace Ensek.EnergyManager.Api.Domain;

internal class MeterReadingEntity : IValidatableObject
{
    private MeterReadingEntity()
    {

    }
    public MeterReadingEntity(AccountEntity account, DateTimeOffset readingDateTimeOffset, int meterReadingValue)
    {
        Account = account;
        ReadingDateTimeOffset = readingDateTimeOffset;
        MeterReadingValue = meterReadingValue;

        if (!this.TryValidate(this, out var error))
        {
            throw new ValidationException(error);
        }
    }

    public Guid MeterReadingId { get; }

    public AccountEntity Account { get; } = null!;

    public DateTimeOffset ReadingDateTimeOffset { get; }

    public int MeterReadingValue { get; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (MeterReadingValue < 0)
            yield return new ValidationResult("Meter Reading is negative");
    }
}