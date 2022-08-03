using Ensek.EnergyManager.Api.Validation;
using System.ComponentModel.DataAnnotations;

namespace Ensek.EnergyManager.Api.Domain;

internal class AccountEntity : IAggregateRoot
{
    private readonly List<MeterReadingEntity> _meterReadings = new();

    private AccountEntity()
    {

    }

    public AccountEntity(string accountId, Name name)
    {
        AccountId = accountId;
        Name = name;
    }

    public string AccountId { get; } = null!;

    public Name Name { get; } = null!;

    public IReadOnlyCollection<MeterReadingEntity> MeterReadings => _meterReadings;

    public AccountEntity AddMeterReading(int meterReading, DateTimeOffset readingDateTimeOffset)
    {
        // validate our state
        var reading = new MeterReadingEntity(this, readingDateTimeOffset, meterReading);
        if (!reading.TryValidate(this, out var error))
        {
            throw new ValidationException(error);
        }

        if (_meterReadings.Count >= MeterReadingConstants.ReadingsPerAccountLimit)
        {
            throw new ValidationException("Reached meter reading limit");
        }

        // add the meter reading
        _meterReadings.Add(reading);

        return this;
    }
}
