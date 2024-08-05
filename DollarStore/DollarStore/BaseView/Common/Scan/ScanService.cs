using DollarStore.Common.Scan;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;


[assembly : Dependency(typeof(ScanService))]
namespace DollarStore.Common.Scan
{
    public class ScanService : IScanService
    {
        public async Task<string> GetScan(INavigation navigation)
        {
            string ScanText = string.Empty;

            var overLay = new ZXingDefaultOverlay()
            {
                ShowFlashButton = false,
                TopText = "Sweep the barcode with the scan line",
            };

            var scan = new ZXingScannerPage(null, overLay);

            await navigation.PushAsync(scan);

            scan.OnScanResult += (result) =>
            {
                scan.IsScanning = false;

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await navigation.PopAsync();
                    ScanText = result.Text;
                });
            };
            return ScanText;

        }
    }
}
