using System;
using System.Collections.Generic;
using System.Text;

namespace DollarStore.Common.Report.Model
{
    public class BasePurchaseReportModel
    {
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemBarcode { get; set; }
        public string InternalId { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
        public string StoreName { get; set; }
        public string VendorName { get; set; }
        public double ItemPrice { get; set; }
        public DateTime? ExpirationDate { get; set; }
      
    }

    public class StorePrices
    {
        public double price { get; set; }
        public string storeName { get; set; }
        public string VendorName { get; set; }
    }


    public class PurchaseReportModel : BasePurchaseReportModel
    {
        public List<StorePrices> prices { get; set; }
        public List<DateTime?> Expirables { get; set; }
    }
}
