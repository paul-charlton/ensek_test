using EnergyManager.Client.ViewModels;
using ReactiveUI.XamForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.CommunityToolkit.Markup;
using Xamarin.Forms;
using static Xamarin.CommunityToolkit.Markup.GridRowsColumns;

namespace EnergyManager.Client.Views
{
    internal partial class Results : ReactiveContentPage<ResultsViewModel>
    {
        private void Build()
        {
            var rowcnt = 0;
            Content = new Grid
            {
                RowDefinitions = Rows.Define(/*BaseStyles.ProfileHeaderGridLength*/),

                Children =
                {
                    // profile header
                    //new ProfileTitleView{Title = "", ShowAvatar = false}
                    //.Row(rowcnt++),
                }
            };
        }
    }
}