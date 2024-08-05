using DollarStore.Core.Models;
using DollarStore.Core.Services.Interfaces;
using Plugin.CloudFirestore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DollarStore.Core.Services
{
    public class VendorService : IVendorService
    {
        public async Task CreateVendor(FirestoreVendor vendor)
        {
            await CrossCloudFirestore.Current.Instance
                   .GetCollection("vendors").AddDocumentAsync(vendor);
        }

        public async Task<IEnumerable<FirestoreVendor>> GetAllVendors()
        {
            var query = await CrossCloudFirestore.Current.Instance
                        .GetCollection("vendors")
                        .OrderBy("Name", false).GetDocumentsAsync();

            var myModel = query.ToObjects<FirestoreVendor>();
            List<FirestoreVendor> vendors = new List<FirestoreVendor>();
            vendors.AddRange(myModel);

            return vendors;
        }

        public async Task<FirestoreVendor> GetVendor(string vendorId)
        {
            var queryVendor = await CrossCloudFirestore.Current.Instance.GetCollection("vendors")
                        .GetDocument(vendorId).GetDocumentAsync();

            var Vendor = queryVendor.ToObject<FirestoreVendor>();

            return Vendor;
        }
    }

}
