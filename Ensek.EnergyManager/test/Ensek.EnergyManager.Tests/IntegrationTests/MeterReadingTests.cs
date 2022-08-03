
using Microsoft.AspNetCore.Mvc.Testing;

namespace Ensek.EnergyManager.Tests.IntegrationTests;

public class MeterReadingTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    internal MeterReadingTests(WebApplicationFactory<Program> applicationFactory)
    {
        _client = applicationFactory.CreateClient();
    }

    /// <summary>
    /// From user story: As an energya account manager, I want to be able to load a CSV of customer meter readings so that we can monitor
    /// thier energy consumption and charge them accordingly
    /// </summary>
    [Fact]
    public void POST_MeterReadings_ReturnsSuccessResponse()
    {
        true.Should().BeFalse();
        // SETUP

        // TEST

        // ASSERT
    }

}
