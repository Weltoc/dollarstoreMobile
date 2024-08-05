using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DollarStore.BaseView.Common.Core.SharedView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PriceReportDetaiItem : ContentView
    {
        public PriceReportDetaiItem()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            detailInfo.IsExpanded = !detailInfo.IsExpanded;

        }

    }
}