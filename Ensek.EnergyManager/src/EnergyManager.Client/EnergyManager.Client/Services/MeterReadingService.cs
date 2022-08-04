using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;

namespace EnergyManager.Client.Services
{
    internal interface IMeterReadingService
    {
        IObservable<Unit> LoadAndSendMeterReadingFile();
    }

    internal class MeterReadingService : IMeterReadingService
    {
        public IObservable<Unit> LoadAndSendMeterReadingFile()
        {
            throw new NotImplementedException();
        }
    }
}
