using Ensek.EnergyManager.Api.Commands;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Reflection.Metadata;

namespace Ensek.EnergyManager.Tests.UnitTests.Commands;
public class MeterReadingsInsertCommandTests
{
    private readonly MeterReadingsInsertCommand _meterReadingsInsertCommand;

    public MeterReadingsInsertCommandTests()
    {
        var data = new List<AccountEntity>
        {
            new AccountEntity("12345", new Name("Test", "Testing"))
        }.AsQueryable();

        var mockSet = new Mock<DbSet<AccountEntity>>();

        mockSet.As<IQueryable<AccountEntity>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<AccountEntity>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<AccountEntity>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<AccountEntity>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());


        var mockContext = new Mock<ApiContext>();
        mockContext.Setup(m => m.Accounts).Returns(mockSet.Object);

        _meterReadingsInsertCommand = new MeterReadingsInsertCommand(mockContext.Object);
    }

    [Fact]
    public void InsertMultipleMeterReadings_WithInvalidReadings_CompletesWithoutException()
    {
        true.Should().BeFalse();
        // SETUP

        // TEST

        // ASSERT
    }


    [Fact]
    public void InsertMultipleMeterReadings_WithMixedReadings_ReturnsCorrectResponseValues()
    {
        true.Should().BeFalse();
        // SETUP

        // TEST

        // ASSERT
    }


}
