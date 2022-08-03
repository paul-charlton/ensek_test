using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;

namespace Ensek.EnergyManager.Tests.IntegrationTests;

public class MeterReadingTests
{
    private readonly HttpClient _client;

    public MeterReadingTests()
    {
        _client = new WebApplicationFactory<Program>().CreateClient();
    }

    /// <summary>
    /// From user story: As an energya account manager, I want to be able to load a CSV of customer meter readings so that we can monitor
    /// their energy consumption and charge them accordingly
    /// </summary>
    [Fact]
    public async Task POST_MeterReadings_ReturnsSuccessResponse()
    {
        // SETUP
        using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Ensek.EnergyManager.Tests.IntegrationTests.Resources.Meter_Reading.csv");
        using var filecontent = new StreamContent(stream!);
        filecontent.Headers.ContentType = new MediaTypeHeaderValue("text/csv");
        var content = new MultipartFormDataContent
        {
            {filecontent, "filename", "Meter_Reading.csv" }
        };

        // TEST
        var response = await _client.PostAsync("/meter-reading-uploads", content);
        var responseContent = await response.Content.ReadAsStringAsync();

        // ASSERT
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        responseContent.Should().NotBeNullOrEmpty();
    }


}
