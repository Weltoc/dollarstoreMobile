using DollarStore.BaseView.Common.Core.Filter;
using DollarStore.Models;
using DollarStore.Service.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(FilterService))]
namespace DollarStore.BaseView.Common.Core.Filter
{
    class FilterService : IFilterItem
    {
        public async Task<IEnumerable<PurchaseOrderMaster>> FilterByBarcode(List<PurchaseOrderMaster> PoList, string itemBarcode)
        {

            var result = await Task.Run(() =>
            {
                 var res = PoList.Where((po) => 
                            po.Purchases.Find(item => item.Barcode == itemBarcode) != null 
                        ).ToList();

                return res;

            });

            return result;
        }

        public async Task<IEnumerable<PurchaseOrderMaster>> FilterByName(List<PurchaseOrderMaster> PoList, string itemName)
        {
            var result = await Task.Run(() =>
            {
                var res = PoList.Where((po) =>
                       po.Purchases.Find(item => item.Name.ToUpper().Contains(itemName.ToUpper())) != null
                   ).ToList();

                return res;

            });

            return result;
        }
    }
}
