using DollarStore.BaseView;
using DollarStore.Models;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DollarStore.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterList : ContentView
    {
        public ListView ListView { get { return masterList; } }
        public List<MasterMenuItem> items;

        public MasterList()
        {
            InitializeComponent();
            SetItem();
        }

        void SetItem()
        {
            items = new List<MasterMenuItem>
            {
                new MasterMenuItem("Home", "home.png", Color.White, typeof(BaseHomePage)),
                new MasterMenuItem("Item", "itemm.png", Color.White, typeof(BaseItemPage)),
                new MasterMenuItem("Order", "cart.png", Color.White, typeof(AllPurchaseOrder))
                
            };

            ListView.ItemsSource = items;
        }

    }
}