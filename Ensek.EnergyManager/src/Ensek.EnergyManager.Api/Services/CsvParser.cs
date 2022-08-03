namespace Ensek.EnergyManager.Api.Services;
internal interface ICsvParser
{
    Task<IEnumerable<MeterReadingDto>> ParseCsvFileAsync(StreamReader reader);
}

internal class CsvParser : ICsvParser
{
    private const char _propertyDelimiter = ',';
    private const string _dateTimeFormat = "dd/MM/yyyy HH:mm";
    public async Task<IEnumerable<MeterReadingDto>> ParseCsvFileAsync(StreamReader reader)
    {
        // assume header is the first line
        await reader.ReadLineAsync().ConfigureAwait(false);
        var data = new List<MeterReadingDto>();

        // then read the rest of the file
        while (!reader.EndOfStream)
        {
            var values = await readAndSplitAsync().ConfigureAwait(false);

            // skip the id
            data.Add(new MeterReadingDto(values[0], values[2], DateTime.ParseExact(values[1], _dateTimeFormat, provider: null)));
        }

        // return our parsed object (skip the id property)
        return data;

        async Task<string[]> readAndSplitAsync()
        {
            var line = await reader.ReadLineAsync().ConfigureAwait(false);
            return line?.Split(_propertyDelimiter) ?? Array.Empty<string>();
        }
    }
}
