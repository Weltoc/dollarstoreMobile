using DollarStore.Models;
using System;
using Plugin.CloudFirestore;

using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Essentials;
using DollarStore.Modules.SearchItems.Services;
using DollarStore.Service.Firestore;
using DollarStore.View.Authentication;
using DollarStore.Modules.Admin.View;

namespace DollarStore
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();

            masterlistitem.ListView.ItemSelected += OnItemSelected;

            userProfilName.Text = App.LoggedUser.Name;
            userProfilEmail.Text = App.LoggedUser.Email;
            projectProfile.Text = App.AppEnv;
            projecVersion.Text = "Version: "+App.Appversion;

            

            if (App.LoggedUser.Pircture != null)
            {
                userProfilImage.Source = new UriImageSource
                {
                    Uri = new Uri(App.LoggedUser.Pircture.AbsoluteUri),
                    CacheValidity = new TimeSpan(150, 0, 0, 0),
                    CachingEnabled = true
                };
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var isFirst = VersionTracking.IsFirstLaunchForCurrentVersion;
            App.LoggedUser.Roles = await App.Database.GetUserRoles();

            if (isFirst)
            {
                    if (!Preferences.Get("isInit", false))
                    {
                        App.SearchInstance.InitSearch = true;
                        SearchService searchService = new SearchService();
                        searchService.GetItemsName();

                        GFirebaseProductService productService = new GFirebaseProductService();
                        productService.GetDollarToFCFAPrice();
                        Preferences.Set("isInit", true);
                    }
                
            }
        }



        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is MasterMenuItem item)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                masterlistitem.ListView.SelectedItem = null;
                IsPresented = false;
            }
        }


    }
}
