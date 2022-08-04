using EnergyManager.Client.ViewModels;
using Nancy.TinyIoc;

namespace EnergyManager.Client.Services
{
    internal static class ViewModelLocator
    {
        private static TinyIoCContainer _container;

        static ViewModelLocator()
        {
            _container = new TinyIoCContainer();

            // services
            _container.Register<IMeterReadingService, MeterReadingService>();
            _container.Register<IFileLoader, FileLoader>();
            _container.Register<IRequestService, RequestService>();

            // viewmodels
            _container.Register<MeterUploadViewModel>();
            _container.Register<ResultsViewModel>();
        }

        public static TViewModel GetViewModel<TViewModel>() where TViewModel : class
        {
            return _container.Resolve<TViewModel>();
        }
    }
}
