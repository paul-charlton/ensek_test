using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Ensek.EnergyManager.Api.Commands;

internal interface IMeterReadingsInsertCommand
{
    Task<MeterReadingsInsertResponse> InsertMultipleMeterReadingsAsync(MeterReadingsInsertRequest request);
}
internal class MeterReadingsInsertCommand : IMeterReadingsInsertCommand
{
    private readonly ApiContext _apiContext;

    public MeterReadingsInsertCommand(ApiContext apiContext)
    {
        _apiContext = apiContext;
    }

    public async Task<MeterReadingsInsertResponse> InsertMultipleMeterReadingsAsync(MeterReadingsInsertRequest request)
    {
        int success = 0, fail = 0;

        // loop through the meter readings
        foreach (var reading in request.MeterReadings)
        {
            try
            {
                // validate the reading
                if (!reading.TryValidate(this, out _))
                {
                    fail++;
                    continue;
                }

                // load the relvant account and then add the reading
                var account = await _apiContext.Accounts
                    .Where(x=> x.AccountId == reading.AccountId)
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);

                if (account == null)
                {
                    fail++;
                    continue;
                }
                account.AddMeterReading(reading.GetParsedMeterReading(), reading.MeterReadingDateTime);

                // mark success
                success++;
            }
            catch (ValidationException)
            {
                fail++;
            }
        }

        // save all of our changes
        await _apiContext.SaveChangesAsync().ConfigureAwait(false);

        return new MeterReadingsInsertResponse(success, fail);
    }
}
