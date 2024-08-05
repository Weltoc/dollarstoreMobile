using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DollarStore.Core.Services
{
    public class ProductService : IProductService
    {
        public async Task CreateItem(FirestoreItem item)
        {
            await CrossCloudFirestore.Current.Instance
                      .GetCollection("items")
                      .AddDocumentAsync(item);
        }

        public async Task<FirestoreItem> GetItem(string itemId)
        {
            var query = await CrossCloudFirestore.Current.Instance.GetCollection("items")
                               .GetDocument(itemId).GetDocumentAsync();

            var model = query.ToObject<FirestoreItem>();

            return model;
        }

        #region Search
            public async Task<IEnumerable<FirestoreItem>> SearchItems(string SearchText)
            {
                var CollectionRef = CrossCloudFirestore.Current.Instance
                                   .GetCollection("items");

                var queryText = await CollectionRef.
                                WhereGreaterThanOrEqualsTo("Name", SearchText)
                                .LimitTo(10).GetDocumentsAsync();

                var queryScan = await CollectionRef.WhereEqualsTo("Barcode", SearchText)
                                .GetDocumentsAsync();

                List<FirestoreItem> Items = new List<FirestoreItem>();
                if (queryScan.IsEmpty)
                {
                    var myModel = queryText.ToObjects<FirestoreItem>();
                    Items.AddRange(myModel);
                    return Items;
                }
                else
                {
                    var myScanModel = queryScan.ToObjects<FirestoreItem>();
                    Items.AddRange(myScanModel);
                    return Items;
                }

            }

        #endregion

    }
}
