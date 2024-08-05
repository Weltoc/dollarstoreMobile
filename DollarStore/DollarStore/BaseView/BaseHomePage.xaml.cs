using DollarStore.Common.Report;
using DollarStore.Modules.Admin.ViewModel;
using DollarStore.Modules.Inventory.View;
using DollarStore.Modules.Receiving.View;
using DollarStore.Modules.PriceRepot.View;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DollarStore.BaseView.Common.Core.SharedView;
using Acr.UserDialogs;
using DollarStore.Modules.OrderForm.View;
using DollarStore.Modules.OrderForm.ViewModel;
using DollarStore.Modules.Expenses.View;
using DollarStore.BaseView.Common.Core.PoFunction;
using DollarStore.Modules.OutOfStock.View;
using DollarStore.Modules.CartPurchases.View;
using DollarStore.Modules.PackagesCenter.View;
using DollarStore.Modules.Item.View;
using System.Net.Http;

namespace DollarStore.BaseView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BaseHomePage : ContentPage
    {
        readonly AuthorizationViewModel routeVm;
        private static readonly HttpClient client = new HttpClient();
        //private readonly OrderFormViewModel vm;
        public BaseHomePage()
        {
            InitializeComponent();
            search_init.BindingContext = App.SearchInstance;
            routeVm = new AuthorizationViewModel(Navigation);
            //vm = new OrderFormViewModel(Navigation);
            CheckPendingActionStatus();

           
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            
        }

       

        private void PurchaseOrder_Tapped(object sender, EventArgs e)
        {
            routeVm.PurchaseOrderReaderCommand.Execute(null);
            fab.IsExpanded = false;
        }
        private void PurchaseOrder_Lome(object sender, EventArgs e)
        {
            routeVm.PurchaseOrder_LomeReaderCommand.Execute(null);
            fab.IsExpanded = false;
        }
        
        private void Shipping_Tapped(object sender, EventArgs e)
        {
            routeVm.ShippingReaderCommand.Execute(null);
            fab.IsExpanded = false;
        }

        private void Autocomplet_Tapped(object sender, EventArgs e)
        {
            routeVm.AutocompleteSearchCommande.Execute(null);
            fab.IsExpanded = false;
        }

        private void NewOrder_Tapped(object sender, EventArgs e)
        {
            routeVm.PurchaseOrderEditorCommand.Execute(null);
            fab.IsExpanded = false;
        }

        private void NewShipping_Tapped(object sender, EventArgs e)
        {
            routeVm.ShippingEditorCommand.Execute(null);
            fab.IsExpanded = false;
        }

        private void ExpandBtn_Clicked(object sender, EventArgs e)
        {
            fab.IsExpanded = !fab.IsExpanded;
        }

        private async void PurchaseReport_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PurchaseReport());
            fab.IsExpanded = false;
        }


        private async void Receiving_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ValideReceived());
            fab.IsExpanded = false;
        }

        private async void Item_Manager_tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BaseItemPage());
        }


        async void CheckPendingActionStatus()
        {
            var purchase = await App.Database.GetPurchaseHeader();

            if(purchase != null)
            {
                var response = await DisplayAlert("Alert", "you have a suspended purchase, do you want to Contunue ? \n" +
                       "\n PO Number : " + purchase.PoRef +
                       "\n Status : " + purchase.ActivityStatus +
                       "\n Vendor : " + purchase.VendorName +
                       "\n Location Name : " + purchase.StoreName +
                       "\n Create At : " + purchase.CreateAt.ToShortDateString() +
                       "\n Notes  : " + purchase.Notes
                       , "Yes", "Not now");

                if (response)
                {
                    await Navigation.PushAsync(new NewPurchase(purchase.Currency));
                }
                else
                {
                    await DependencyService.Get<IPoFunctions>().AddOrder();
                    App.Database.DeletePurchaseHeader();
                }
                    
            }
        }

        private async void Inventory_tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Inventory());
        }

        private async void PriceReport_Tapped(object sender, EventArgs e)
        { 
            await Navigation.PushAsync(new PriceReportHomePage());

        }
        private async void Order_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new OrderFormTabbedPage());
        }
        private async void Expence_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ExpensePage());
        } 
        private async void Out_Of_Stock(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PackageCenter());
        }
        private async void Purchase_Cart(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CartPurchases());
        }


        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CartsUrgents());
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SaveForLater());
        }

        private async void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ForValidation());
        }

        private async void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ExpirationItems());
        }

        //private async void TapGestureRecognizer_Tapped_4(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync (new GestionRayons());
        //}

        private void TapGestureRecognizer_Tapped_5(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new AllInventory());
            routeVm.InventoryEditorCommand.Execute(null);
            fab.IsExpanded = false;
        }
    }
}