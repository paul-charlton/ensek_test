using Ensek.EnergyManager.Api.Commands;
using Microsoft.EntityFrameworkCore;
using Moq.EntityFrameworkCore;

namespace Ensek.EnergyManager.Tests.UnitTests.Commands;
public class MeterReadingsInsertCommandTests
{
    private readonly MeterReadingsInsertCommand _meterReadingsInsertCommand;
    private const string _testAccountId = "12345";
    private DateTime _validDateTime = DateTime.Now;

    public MeterReadingsInsertCommandTests()
    {
        var data = new List<AccountEntity>
        {
            new AccountEntity(_testAccountId, new Name("Test", "Testing"))
        };

        var mockContext = new Mock<ApiContext>();
        mockContext.Setup(m => m.Accounts).ReturnsDbSet(data);

        _meterReadingsInsertCommand = new MeterReadingsInsertCommand(mockContext.Object);
    }

    [Fact]
    public async Task InsertMultipleMeterReadings_WithInvalidReadings_CompletesWithoutException()
    {
        // SETUP
        var request = new MeterReadingsInsertRequest(new[]
        {
            new MeterReadingDto(_testAccountId, "NONSENSE", _validDateTime)
        });

        // TEST
        var action = async () => await _meterReadingsInsertCommand.InsertMultipleMeterReadingsAsync(request);

        // ASSERT
        await action.Should().NotThrowAsync();
    }


    [Fact]
    public async Task InsertMultipleMeterReadings_WithMixedReadings_ReturnsCorrectResponseValues()
    {
        // SETUP
        var request = new MeterReadingsInsertRequest(new[]
        {
            new MeterReadingDto(_testAccountId, "NONSENSE", _validDateTime),
            new MeterReadingDto(_testAccountId, "12345", _validDateTime),
        });

        // TEST
        var response = await _meterReadingsInsertCommand.InsertMultipleMeterReadingsAsync(request);

        // ASSERT
        response.SuccessfulReadings.Should().Be(1);
        response.FailedReadings.Should().Be(1);
    }


}
