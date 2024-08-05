using DollarStore.Common.Report.Model;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DollarStore.Common.Report
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModalReportBox : PopupPage
    {
        public ModalReportBox(PurchaseReportModel purchaseReportModel)
        {
            InitializeComponent();
            BindingContext = purchaseReportModel;
        }

        private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            Navigation.PopPopupAsync(true);
        }
    }
}