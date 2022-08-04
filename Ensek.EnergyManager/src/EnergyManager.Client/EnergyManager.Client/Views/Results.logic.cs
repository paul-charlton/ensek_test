using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Text;

namespace EnergyManager.Client.Views
{
    internal partial class Results
    {
        public Results()
        {
            Build();

            //PopulateViewModelFromType();

            _ = this.WhenActivated(disposables =>
            {
                disposables.Add(Disposable.Empty);
            });
        }
    }
}