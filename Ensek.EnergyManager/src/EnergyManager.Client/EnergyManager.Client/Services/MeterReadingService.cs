using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reactive.Linq;

namespace EnergyManager.Client.Services
{
    internal interface IMeterReadingService
    {
        IObservable<MeterReadingResponse> LoadAndSendMeterReadingFile(Stream fileStream, string fileName);
    }

    internal class MeterReadingService : IMeterReadingService
    {
        private readonly IRequestService _requestService;
        private readonly Uri _uri;

        public MeterReadingService(IRequestService requestService)
        {
            _requestService = requestService;

            // hardcoded endpoint for now...
            _uri = new Uri("https://10.0.2.2:7011/meter-reading-uploads");
        }

        public IObservable<MeterReadingResponse> LoadAndSendMeterReadingFile(Stream fileStream, string fileName)
        {
            // build the mulitpart message
            var filecontent = new StreamContent(fileStream);
            filecontent.Headers.ContentType = new MediaTypeHeaderValue("text/csv");
            var content = new MultipartFormDataContent
            {
                {filecontent, "filename", fileName }
            };

            // send
            return _requestService
                .PostData<MeterReadingResponse>(_uri, content)
                .Catch(Observable.Return(MeterReadingResponse.AsError("Error sending data")))
                .Select(x=> x!=null ? x : MeterReadingResponse.AsError("Unknown error"))
                .Do(_ =>
                {
                    // close the stream
                    filecontent.Dispose();
                    fileStream.Dispose();
                });
        }
    }
    internal class MeterReadingResponse
    {
        public static MeterReadingResponse AsError(string error) => new MeterReadingResponse(0, 0) { Error = error };

        public MeterReadingResponse()
        {

        }

        public MeterReadingResponse(int successfulReadings, int failedReadings)
        {
            SuccessfulReadings = successfulReadings;
            FailedReadings = failedReadings;
        }

        public int SuccessfulReadings { get; set; }

        public int FailedReadings { get; set; }

        public string? Error { get; set; }

        public bool IsSuccess => string.IsNullOrWhiteSpace(Error);

        public override string ToString()
        {
            return IsSuccess
                ? $"Successful readings: {SuccessfulReadings}\r\nFailed Readings: {FailedReadings}"
                : Error!;
        }
    }
}
