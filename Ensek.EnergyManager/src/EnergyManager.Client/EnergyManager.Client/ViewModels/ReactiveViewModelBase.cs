using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Reactive.Concurrency;

namespace EnergyManager.Client.ViewModels
{
    public abstract class ReactiveViewModelBase : ReactiveObject, IActivatableViewModel
    {
        protected ReactiveViewModelBase()
        {
            MainThreadScheduler = RxApp.MainThreadScheduler;
            BackgroundThreadScheduler = RxApp.TaskpoolScheduler;

            UrlPathSegment = GetType().Name;
        }

        public ViewModelActivator Activator { get; } = new ViewModelActivator();

        [Reactive]
        public bool IsBusy { get; set; }

        public string? UrlPathSegment { get; }

        public IScheduler BackgroundThreadScheduler { get; }

        public IScheduler MainThreadScheduler { get; }

    }
}
