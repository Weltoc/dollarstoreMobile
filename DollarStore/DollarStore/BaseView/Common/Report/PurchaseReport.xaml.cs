
using DollarStore.Common.Report.Model;
using DollarStore.Models;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace DollarStore.Common.Report
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PurchaseReport : ContentPage
    {
        private readonly PurchaseReportVeiwModel mv;
        public PurchaseReport()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            mv = new PurchaseReportVeiwModel();
            BindingContext = mv;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Currentbtnradio.IsChecked = true;
            mv.GetReportCommand.Execute(null);
            mv.LoadVendorCommand.Execute(null);
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {

            filterfab.IsExpanded = !filterfab.IsExpanded;
            if (!filterfab.IsExpanded)
            {
                mv.LoadVendorCommand.Execute(null);
            }
        }


        private async void BackButton_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void Scan_ImageButton_Clicked(object sender, System.EventArgs e)
        {
            var overLay = new ZXingDefaultOverlay()
            {
                ShowFlashButton = false,
                TopText = "Sweep the barcode with the scan line",
            };

            var scan = new ZXingScannerPage(null, overLay);

            await Navigation.PushAsync(scan);

            scan.OnScanResult += (result) =>
            {
                scan.IsScanning = false;

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopAsync();
                    searchReport.Text = result.Text;
                    mv.SearchItemCommand.Execute(searchReport.Text);
                });
            };
        }

        private void SearchReport_SearchButtonPressed(object sender, System.EventArgs e)
        {
            mv.SearchItemCommand.Execute(searchReport.Text);
        }

        private void SearchReport_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                mv.SearchEmpty();
            }
        }

        private void Back_Button_Tapped(object sender, EventArgs e)
        {
            filterfab.IsExpanded = false;
        }

        private async void Report_collectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(e.CurrentSelection.FirstOrDefault() is PurchaseReportModel order))
                return;
            await Navigation.PushPopupAsync(new ModalReportBox(order));
           ((CollectionView)sender).SelectedItem = null;
        }

        private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var rButton = sender as RadioButton;
            if(rButton.Text == "All")
            {
                mv.Status = "";
            }
            else
            {
                mv.Status = rButton.Text;
            }
            
        }

        private void Apply_Clicked(object sender, EventArgs e)
        {
            mv.GetReportCommand.Execute(null);
            filterfab.IsExpanded = false;
        }

        private void PickerStore_SelectedIndexChanged(object sender, EventArgs e)
        {
            var shop = (Shop)pickerStore.SelectedItem;
            var vendor = (Vendor)pickerVendor.SelectedItem;

            if (shop != null)
            {
                var index = vendor.Stores.IndexOf(shop);
                mv.StoreId = index.ToString();
            }

        }

        private void DateFrom_DateSelected(object sender, DateChangedEventArgs e)
        {
            var dateFrom = (sender) as DatePicker;
            // ordersMv.DateFrom = dateFrom.Date;
            DateFromEntry.Text = dateFrom.Date.ToShortDateString();
            mv.MinimumDate = dateFrom.Date;
        }

        private void Dateto_DateSelected(object sender, DateChangedEventArgs e)
        {
            var dateTo = (sender) as DatePicker;
            // vm.DateTo = dateTo.Date;
            DateToEntry.Text = dateTo.Date.ToShortDateString();
        }

        private void BoxViewCalc_Tapped(object sender, EventArgs e)
        {
            filterfab.IsExpanded = false;
        }

        private void ClearDate_Tapped(object sender, EventArgs e)
        {
            mv.ClearDate();
        }

        private void ClearVendor_Tapped(object sender, EventArgs e)
        {
            mv.ClearVendor();
        }
    }
}