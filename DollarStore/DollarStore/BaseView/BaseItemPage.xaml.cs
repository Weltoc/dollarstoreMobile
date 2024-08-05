using DollarStore.Interface;
using DollarStore.Models;
using DollarStore.View;
using DollarStore.ViewModel;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;
using DollarStore.Modules.SearchItems;
using DollarStore.BaseView.Common.Core.SharedView;
using Rg.Plugins.Popup.Extensions;
using DollarStore.BaseView.Common.Core.Erors;
using Xamarin.Essentials;
using DollarStore.Common.Constants;
using DollarStore.Modules.Admin.ViewModel;

namespace DollarStore.BaseView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BaseItemPage : ContentPage
    {
        private readonly SearchViewModel SearchVm;
        public Command LoadLatestItemsCommand { get; set; }
        public Command<Models.Item> DetailCommand { get; set; }
        readonly AuthorizationViewModel routeVm;


        public BaseItemPage()
        {
            InitializeComponent();
            routeVm = new AuthorizationViewModel(Navigation);
            SearchVm = new SearchViewModel();
            collectionItem.BindingContext = SearchVm;
            searchItem.BindingContext = SearchVm;

            refreshview.BindingContext = this;
            LoadLatestItemsCommand = new Command(async () => await LoadLatesItems());

            LoadLatestItemsCommand.Execute(null);
            refreshview.Command = LoadLatestItemsCommand;

            DetailCommand = new Command<Models.Item>(async (Models.Item _item) =>
            {
                await Navigation.PushAsync(new ItemDetails(_item, LoadLatestItemsCommand));
            });
        }
        

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (width > height)
            {
                if(Device.Idiom == TargetIdiom.Phone)
                {
                    outerStack.Span = 4;
                }
                else
                {
                    outerStack.Span = 5;
                }
            }
            else
            {
                if (Device.Idiom == TargetIdiom.Phone)
                {
                    outerStack.Span = 3;
                }
                else
                {
                    outerStack.Span = 3;
                }
            }
        }

        private void Add_ToolbarItem_Clicked(object sender, EventArgs e)
        {
            routeVm.ItemCreatorCommand.Execute(null);
            //await Navigation.PushAsync(new NewProduct());
        }

        private async void CollectionItem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(e.CurrentSelection.FirstOrDefault() is Item item))
                return;

            try
            {
                await Navigation.PushAsync(new ItemDetails(item, LoadLatestItemsCommand));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Alert", ex.StackTrace, "ok");
                Console.WriteLine(ex.Message);
                _ = Task.Factory.StartNew(() =>
                {
                    _ = DependencyService.Get<IErrorLogService>().saveError(ex.Message, ex.StackTrace);
                });
            }
            finally
            {
                ((CollectionView)sender).SelectedItem = null;
            }
           
        }

        async Task LoadLatesItems()
        {
            try
            {
                IsBusy = true;
                searchItem.Text = "";
                collectionItem.IsVisible = false;
                var items = await  DependencyService.Get<IProductService>()
                            .GetLastestItems();

                SearchVm.AItems.ReplaceRange(items);
            }
            catch (Exception ex)
            {
                var response = await DisplayAlert("Search", ex.Message, "OK", "Details");

                if (!response)
                {
                    await Navigation.PushPopupAsync(new ErrorBox(ex.StackTrace));
                }
                _ = Task.Factory.StartNew(() => 
                {
                    _ = DependencyService.Get<IErrorLogService>().saveError(ex.Message, ex.StackTrace);
                });
            }
            finally
            {
                IsBusy = false;
                collectionItem.IsVisible = true;
            }
        }

        private async void Scan_ImageButton_Clicked(object sender, EventArgs e)
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
                    searchItem.Text = result.Text;
                    SearchVm.SearchCommand.Execute(null);
                });
            };
        }

        private void SearchItem_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(SearchVm.SearchText))
            {
                lastestProd.IsVisible = true;
                LoadLatestItemsCommand.Execute(null);
            }
            else
            {
                lastestProd.IsVisible = false;
            }
        }

        private void SearchItem_SearchButtonPressed(object sender, EventArgs e)
        {
            SearchVm.SearchCommand.Execute(null);
        }
     
        private async void Search_TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new SearchItemsView(DetailCommand, Constants.ItemManagerPage));
        }
        
    }
}