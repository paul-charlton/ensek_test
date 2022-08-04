using System;

namespace EnergyManager.Client.Services
{
    /// <summary>
    /// Send requests to an endpoint.  Will handle retries etc
    /// </summary>
    internal interface IRequestService
    {
        IObservable<TResponse> PostData<TRequest, TResponse>(Uri uri, TRequest request);
    }

    internal class RequestService : IRequestService
    {
        public IObservable<TResponse> PostData<TRequest, TResponse>(Uri uri, TRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
