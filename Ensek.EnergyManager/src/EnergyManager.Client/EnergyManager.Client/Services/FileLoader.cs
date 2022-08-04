using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EnergyManager.Client.Services
{
    internal interface IFileLoader
    {
        Task<string> PickFileAsync();
    }

    internal class FileLoader : IFileLoader
    {
        public Task<string> PickFileAsync()
        {
            throw new NotImplementedException();
        }
    }
}
