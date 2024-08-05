using DollarStore.Common.Core.Bill.Model;
using DollarStore.Models;
using Plugin.CloudFirestore;
using System.Threading.Tasks;

namespace DollarStore.Common.Core.Bill.Service
{
    public class FirestoreBillService : IBillService
    {
        public async Task SaveBill(FirestoreBill firestoreBill, string PurchaseId)
        {
            var Billreference = CrossCloudFirestore.Current.Instance
                   .Collection("bills")
                   .Document();
                   
            await Billreference.SetAsync(firestoreBill);

            var Billdocument = await Billreference.GetAsync();

            var purchaseReference = CrossCloudFirestore.Current.Instance
                                    .Collection("purchase_orders")
                                    .Document(PurchaseId);

            await CrossCloudFirestore.Current.Instance
                    .RunTransactionAsync((transac) =>
                    {
                        var Purchasedocument = transac.Get(purchaseReference);
                        var TPurchase = Purchasedocument.ToObject<PurchaseOrderMaster>();

                        TPurchase.Bills = Billdocument.Id;

                        transac.Update(purchaseReference, TPurchase);
                    });

        }
    }
}
