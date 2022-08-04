using EnergyManager.Client.ViewModels;
using ReactiveUI.XamForms;
using Xamarin.CommunityToolkit.Markup;
using Xamarin.Forms;

namespace EnergyManager.Client.Views
{
    internal partial class MeterUpload : ReactiveContentPage<MeterUploadViewModel>
    {
        private Button _selectFileButton = null!, _uploadButton = null!;
        private Label _filename = null!;

        private void Build()
        {
            Content = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.Center,

                Children =
                {
                    // select file button
                    new Button{Text = "Select File", TextTransform = TextTransform.None}
                    .Assign(out _selectFileButton),

                    // file name
                    new Label()
                    .Assign(out _filename),

                    // upload file button
                    new Button{Text = "Upload", TextTransform = TextTransform.None}
                    .Assign(out _uploadButton),
                }
            };
        }
    }
}