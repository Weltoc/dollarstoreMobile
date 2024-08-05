using Acr.UserDialogs;
using DollarStore.BaseView.Common.Core.PoFunction;
using DollarStore.Common.Constants;
using DollarStore.Interface;
using DollarStore.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(PoFunctions))]
namespace DollarStore.BaseView.Common.Core.PoFunction
{
    internal class PoFunctions : IPoFunctions
    {

        public async Task AddOrder()
        {
            try
            {
                using (UserDialogs.Instance.Loading(""))
                {
                    var fbAdding = DependencyService.Get<IPurchaseOrderService>();

                    var pH = await App.Database.GetPurchaseHeader();

                    bool AddStatus = false;

                    if (pH.ActivityStatus == Constants.New)
                    {
                        AddStatus = await fbAdding.AddPurchaseToFirestore();
                    }

                    else if (pH.EditingStatus == Constants.Edit)
                    {
                        AddStatus = await fbAdding.UpdatePurchaseToFirestoe();
                    }

                    if (AddStatus)
                    {
                        App.Database.DeleteOldPurchase();
                        App.Database.DeletePurchaseHeader(await App.Database.GetPurchaseHeader());
                    }
                }
            }
            catch (Exception ex)
            {
                var response = await Application.Current.MainPage.DisplayAlert("Save P.O", ex.Message, "OK", "Details");

            }
        }
    }
}
