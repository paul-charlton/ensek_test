using FileHelpers;

namespace Ensek.EnergyManager.Api.Services;
internal interface ICsvParser
{
    IEnumerable<TData> ParseCsvFile<TData>(TextReader reader) where TData: class;
}

internal class CsvParser : ICsvParser
{
    public IEnumerable<TData> ParseCsvFile<TData>(TextReader reader) where TData : class
    {
        var engine = new FileHelperEngine<TData>();
        return engine.ReadStream(reader);
    }
}
