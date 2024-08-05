using DollarStore.Interface;
using DollarStore.Models;
using DollarStore.Service.Firestore;
using Plugin.CloudFirestore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(FirestoreVendorService))]
namespace DollarStore.Service.Firestore
{
    public class FirestoreVendorService : IVendorService
    {
        public async Task<List<Vendor>> GetVendors()
        {

            var query = await CrossCloudFirestore.Current.Instance
                        .Collection("vendors")
                        .OrderBy("Name", false).GetAsync();

            var myModel = query.ToObjects<Vendor>();
            List<Vendor> vendors = new List<Vendor>();
            vendors.AddRange(myModel);

            return vendors;

        }

        public async Task SaveShop(string vendorID, Shop shop)
        {
               var reference =  CrossCloudFirestore.Current.Instance.Collection("vendors")
                                         .Document(vendorID);
                await CrossCloudFirestore.Current.Instance.RunTransactionAsync((transac) =>
                {
                    var docuument = transac.Get(reference);
                    var Order = docuument.ToObject<Vendor>();

                    Order.Stores.Add(shop);
                    transac.Update(reference, Order);
                });
         
        }

        public async Task SaveVendor(Vendor vendor)
        {
            await CrossCloudFirestore.Current.Instance.Collection("vendors")
                                       .AddAsync(vendor);
        }
    

    }
}
