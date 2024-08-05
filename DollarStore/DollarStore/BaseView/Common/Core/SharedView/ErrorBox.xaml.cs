using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;

namespace DollarStore.BaseView.Common.Core.SharedView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ErrorBox : PopupPage
    {


        public ErrorBox(string Error)
        {
            InitializeComponent();
            errorMessage.Text = Error;
        }

        private async void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            await Navigation.PopPopupAsync(true);
        }

        private async void Button_Clicked(object sender, System.EventArgs e)
        {
            await Clipboard.SetTextAsync(errorMessage.Text);
            copyName.Text = "Copy done";
            copyName.BackgroundColor = Xamarin.Forms.Color.Green;
        }
    }
}