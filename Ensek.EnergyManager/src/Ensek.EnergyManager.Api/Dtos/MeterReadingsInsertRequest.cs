using System.ComponentModel.DataAnnotations;

namespace Ensek.EnergyManager.Api.Dtos;

internal record MeterReadingsInsertRequest : DtoRequest
{
    public MeterReadingsInsertRequest(IEnumerable<MeterReadingDto> meterReadings)
    {
        MeterReadings = meterReadings;
    }

    public IEnumerable<MeterReadingDto> MeterReadings { get; init; } = Enumerable.Empty<MeterReadingDto>();

}
