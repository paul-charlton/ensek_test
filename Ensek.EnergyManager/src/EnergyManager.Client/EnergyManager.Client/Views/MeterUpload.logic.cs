using EnergyManager.Client.Services;
using EnergyManager.Client.ViewModels;
using ReactiveUI;
using System;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace EnergyManager.Client.Views
{
    internal partial class MeterUpload
    {
        public MeterUpload()
        {
            Build();

            ViewModel = ViewModelLocator.GetViewModel<MeterUploadViewModel>();

            _ = this.WhenActivated(disposables =>
            {
                this.BindCommand(ViewModel, x => x.SelectFileCommand, x => x._selectFileButton)
                    .DisposeWith(disposables);

                this.BindCommand(ViewModel, x => x.UploadFileCommand, x => x._uploadButton)
                    .DisposeWith(disposables);

                this.OneWayBind(ViewModel, x => x.FileResult, x => x._filename.Text, x => x?.FileName)
                    .DisposeWith(disposables);

                this.WhenAnyValue(x => x.ViewModel!.UploadResponse)
                    .Do(x=> Debug.WriteLine(x))
                    .WhereNotNull()
                    .SelectMany(async x =>
                    {
                        await DisplayAlert("Upload Response", x, "ok").ConfigureAwait(false);
                        return Unit.Default;
                    })
                    .Subscribe()
                    .DisposeWith(disposables);

            });
        }
    }
}