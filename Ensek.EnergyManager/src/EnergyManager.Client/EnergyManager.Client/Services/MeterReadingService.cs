using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reactive.Linq;

namespace EnergyManager.Client.Services
{
    internal interface IMeterReadingService
    {
        IObservable<string> LoadAndSendMeterReadingFile(Stream fileStream);
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

        public IObservable<string> LoadAndSendMeterReadingFile(Stream fileStream)
        {
            // build the mulitpart message
            var filecontent = new StreamContent(fileStream);
            filecontent.Headers.ContentType = new MediaTypeHeaderValue("text/csv");
            var content = new MultipartFormDataContent
            {
                {filecontent, "filename", "Meter_Reading.csv" }
            };

            // send
            return _requestService
                .PostData<string>(_uri, content)
                .Select(_ => "Upload completed")
                .Catch(Observable.Return("Error sending data"))
                .Do(_ =>
                {
                    // close the stream
                    filecontent.Dispose();
                    fileStream.Dispose();
                });
        }
    }
}
