
using Acr.UserDialogs;
using DollarStore.Interface;
using DollarStore.Models;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DollarStore.Common.Core.Vendor.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewVendor : ContentPage
    {
        private readonly Command _loadCommand;
        public NewVendor(Command command)
        {
            InitializeComponent();
            addVendor.IsEnabled = false;
            _loadCommand = command;
        }

        private async void AddVendor_Clicked(object sender, System.EventArgs e)
        {   
            try
            {
                using (UserDialogs.Instance.Loading("Adding please wait ..."))
                {
                    List<Shop> shops = new List<Shop>
                    {
                        new Shop { Name = locationName.Text.ToUpper() },
                        new Shop{Name = "--ANY--"}
                    };

                    var _vendorService = DependencyService.Get<IVendorService>();

                    await _vendorService.SaveVendor(new Models.Vendor
                    {
                        Name = vendorName.Text.ToUpper(),
                        Stores = shops
                    });
                }

                await Navigation.PopAsync();
                _loadCommand.Execute(null);

            }
            catch (System.Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error adding vendor", ex.Message, "ok");
            }

        }


        private void CheckValidation()
        {
            if (!string.IsNullOrWhiteSpace(vendorName.Text) && !string.IsNullOrWhiteSpace(locationName.Text))
            {
                addVendor.IsEnabled = true;
            }
            else
            {
                addVendor.IsEnabled = false;
            }
        }

        private void LocationName_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckValidation();
        }

        private void VendorName_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckValidation();
        }
    }
}