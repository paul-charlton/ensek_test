namespace Ensek.EnergyManager.Api.Dtos;

internal record MeterReadingsInsertResponse(int SuccessfulReadings, int FailedReadings) : DtoResponse;