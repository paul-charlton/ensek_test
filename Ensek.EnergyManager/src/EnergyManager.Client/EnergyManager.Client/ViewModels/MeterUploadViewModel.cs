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
            _meterReadingService = meterReadingService;
            SelectFileCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                FileResult = await FilePicker.PickAsync(PickOptions.Default).ConfigureAwait(true); // back on main thread
                return Unit.Default;
            });
            UploadFileCommand = ReactiveCommand.CreateFromObservable(UploadFile, canExecute: this.WhenAnyValue(x=> x.FileResult).Select(x=> x!= null));


            this.WhenActivated(disposables =>
            {
                // clear after it's been set
                this.WhenAnyValue(x => x.UploadResponse)
                    .WhereNotNull()
                    .Throttle(TimeSpan.FromMilliseconds(500))
                    .Subscribe(_ => UploadResponse = null)
                    .DisposeWith(disposables);
            });

        }


        public ReactiveCommand<Unit, Unit> SelectFileCommand { get; }

        public ReactiveCommand<Unit, string> UploadFileCommand { get; }

        [Reactive]
        public FileResult? FileResult { get; private set; }

        [Reactive]
        public string? UploadResponse { get; private set; }

        private IObservable<string> UploadFile()
        {
            // load the file stream and then send to endpoint - nb - the service will close the stream when finished
            string filename = FileResult!.FileName;
            return FileResult!
                .OpenReadAsync()
                .ToObservable()
                .FirstAsync()
                .SelectMany(x => _meterReadingService.LoadAndSendMeterReadingFile(x, filename))
                .ObserveOn(MainThreadScheduler)
                .Select(x=> x.ToString())
                .Do(x=> UploadResponse = x);
        }
    }


}
