using Acr.UserDialogs;
using System;
using System.Runtime.CompilerServices;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DollarStore.Common.Core.SharedView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImageViewForItemModel : ContentView
    {
        public string IsRotationChange;
        public ImageViewForItemModel()
        {
            InitializeComponent();
        }



        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            detailInfo.IsExpanded = !detailInfo.IsExpanded;
            
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {

        }

        private async void CopieCodebareTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Clipboard.SetTextAsync(codebareName.Text);
            UserDialogs.Instance.Toast(new ToastConfig("barcode copied")
                                .SetDuration(TimeSpan.FromSeconds(3))
                                .SetPosition(ToastPosition.Bottom));
        }
    }
}