using DollarStore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DollarStore.BaseView.Common.Core.Filter
{
    interface IFilterItem
    {
        Task<IEnumerable<PurchaseOrderMaster>> FilterByName(List<PurchaseOrderMaster> PoList, string itemName);
        Task<IEnumerable<PurchaseOrderMaster>> FilterByBarcode(List<PurchaseOrderMaster> PoList, string itemBarcode);
    }
}
