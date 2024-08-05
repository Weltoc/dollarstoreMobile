using DollarStore.Database;
using DollarStore.Models;
using DollarStore.Modules.Admin.Model;
using DollarStore.View.Authentication;
using System.Globalization;
using System.Threading;
using Plugin.CloudFirestore;
using Xamarin.Forms;
using DollarStore.Modules.SearchItems.Services;
using Xamarin.Essentials;
using DollarStore.BaseView.Common.Core.SearchInit;
using DollarStore.Modules.PackagesCenter.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DollarStore
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            Device.SetFlags(new[] { "Shapes_Experimental", "RadioButton_Experimental",
                                    "Expander_Experimental", "DragAndDrop_Experimental",
                                    "SwipeView_Experimental" });
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDk5Mjg3QDMxMzkyZTMyMmUzMEdJVW1HQzAzejJaaUFzRVpLekQ1Nlg0Y3lyK2ovZThmRStPSCtOc2pPZmM9");
            CrossCloudFirestore.Current.Instance.FirestoreSettings = new FirestoreSettings
            {
                CacheSizeBytes = FirestoreSettings.CacheSizeUnlimited,
                IsPersistenceEnabled = true
            };

            VersionTracking.Track();

            var currentVersion = VersionTracking.CurrentVersion;
            var codeVersion = VersionTracking.CurrentBuild;
            Appversion = codeVersion + '.' + currentVersion;

            DevEnv();

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("en-US");

            PreventMainPage();
            GetRoles();
           
            
        }

        #region Variable 
        public static double ScreenWidth;
        public static double ScreenHeight;
        public static string Appversion;
        public static string AppEnv { get; set; }
        public static SearchInit SearchInstance = new SearchInit();
        public static bool IsLoggedIn { get; set; }
        public static string ImgstoragePath { get; set; }
        public static UserProfile LoggedUser { get; set; } = new UserProfile();

        async public void DevEnv()
        {
            IsLoggedIn = false;
            LoggedUser.Email = "";
            LoggedUser.Name = "";
            LoggedUser.Roles = new UserRole { Admin = true};

            await Database.SaveUserRole(LoggedUser.Roles);
            ImgstoragePath = "dollarstore-eab75.appspot.com";

            AppEnv = "Production";
        }

        async public void ProductionEnv()
        {
            VersionTracking.Track();
            AppEnv = "Production";
            ImgstoragePath = "dollarstore-eab75.appspot.com"; 
            LoggedUser.Roles = await Database.GetUserRoles();
        }

        #endregion 

        static LocalDatabase database;
        public static LocalDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new LocalDatabase();
                }
                return database;
            }
        }

        public void PreventLogin()
        {
            MainPage = new NavigationPage(new Login());
        }

        public async void GetRoles()
        {
            if (LoggedUser.Roles == null)
            {
                LoggedUser.Roles = await Database.GetUserRoles();
            }
        }

        public void SearchInit()
        {
            SearchService searchService = new SearchService();
            searchService.GetItemsName();
        }

        public void PreventMainPage()
        {
            MainPage = !IsLoggedIn ? (Page)new NavigationPage(new Login()) : new MainPage();
        }

        protected override void OnStart()
        {
            var isFirst = VersionTracking.IsFirstLaunchForCurrentVersion;

            GetRoles();
            if (!isFirst && Preferences.Get("isInit", false) == true)
            {
                SearchInit();
            }
        }

        protected override void OnSleep()
        {

        }

        protected override void OnResume()
        {

        }

    }
}