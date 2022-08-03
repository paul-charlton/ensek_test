

using Ensek.EnergyManager.Api.Commands;
using System.ComponentModel.DataAnnotations;

namespace Ensek.EnergyManager.Api.Services;

internal interface IMeterReadingService
{
    /// <summary>
    /// Validates and processes the raw csv file
    /// </summary>
    /// <param name="formFile"></param>
    /// <returns></returns>
    Task<MeterReadingsInsertResponse> ProcessMeterReadingsCsvFileAsync(IFormFile formFile);
}

internal class MeterReadingService : IMeterReadingService
{
    private readonly IMeterReadingsInsertCommand _meterReadingsInsertCommand;
    private readonly ICsvParser _csvParser;

    public MeterReadingService(IMeterReadingsInsertCommand meterReadingsInsertCommand, ICsvParser csvParser)
    {
        _meterReadingsInsertCommand = meterReadingsInsertCommand;
        _csvParser = csvParser;
    }

    public Task<MeterReadingsInsertResponse> ProcessMeterReadingsCsvFileAsync(IFormFile formFile)
    {
        if (!formFile.TryValidateFile(".csv", out var errors))
            throw new ValidationException(errors);

        // open the file and parse using csv parser
        using var stream = formFile.OpenReadStream();
        using var reader = new StreamReader(stream);
        var meterReadings = _csvParser.ParseCsvFile<MeterReadingDto>(reader);

        return _meterReadingsInsertCommand.InsertMultipleMeterReadingsAsync(new MeterReadingsInsertRequest(meterReadings));
    }
}
