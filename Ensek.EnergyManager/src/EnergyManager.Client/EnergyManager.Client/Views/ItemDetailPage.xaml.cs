using EnergyManager.Client.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace EnergyManager.Client.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}