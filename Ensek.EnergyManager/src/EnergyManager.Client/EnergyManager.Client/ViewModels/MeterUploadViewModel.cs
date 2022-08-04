using EnergyManager.Client.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using Xamarin.Essentials;

namespace EnergyManager.Client.ViewModels
{
    internal class MeterUploadViewModel : ReactiveViewModelBase
    {
        private readonly IMeterReadingService _meterReadingService;

        public MeterUploadViewModel(IMeterReadingService meterReadingService)
        {
            SelectFileCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                FileResult = await FilePicker.PickAsync(PickOptions.Default).ConfigureAwait(true);
                return Unit.Default;
            });
            UploadFileCommand = ReactiveCommand.CreateFromObservable(UploadFile, canExecute: this.WhenAnyValue(x=> x.FileResult).Select(x=> x!= null));

            _meterReadingService = meterReadingService;
        }


        public ReactiveCommand<Unit, Unit> SelectFileCommand { get; }

        public ReactiveCommand<Unit, string> UploadFileCommand { get; }

        [Reactive]
        public FileResult? FileResult { get; private set; }

        private IObservable<string> UploadFile()
        {
            // load the file stream and then send to endpoint - nb - the service will close the stream when finished
            return FileResult!
                .OpenReadAsync()
                .ToObservable()
                .FirstAsync()
                .SelectMany(x => _meterReadingService.LoadAndSendMeterReadingFile(x));
        }
    }


}
