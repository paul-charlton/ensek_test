
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

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
    public async Task POST_MeterReadings_ReturnsSuccessResponse()
    {
        true.Should().BeFalse();
        // SETUP

        // TEST

        var response = await _client.PostAsync("/meter-reading-uploads", new MultipartFormDataContent());
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        // ASSERT
    }

}
