using EnergyManager.Client.ViewModels;
using EnergyManager.Client.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace EnergyManager.Client
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}
