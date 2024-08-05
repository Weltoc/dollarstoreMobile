using DollarStore.Common.Report.Model;
using DollarStore.Common.Report.Service;
using DollarStore.Service.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(PurchaseReportService))]
namespace DollarStore.Common.Report.Service
{
    public class PurchaseReportService : IReportService
    {
        public async Task<List<PurchaseReportModel>> GetCurrentPurchaseReport(long pagination, 
            string status=null, string vendor=null, string store=null,string dateFrom=null, string dateTo=null)
        {
            if (string.IsNullOrEmpty(dateFrom) && string.IsNullOrEmpty(dateTo))
            {
                var _mounth = DateTime.Now.Month;
                var _day = DateTime.Now.Day;
                var _year = DateTime.Now.Year;
                if(_mounth == 1)
                {
                    _mounth = 12;
                    _year -= 1;
                }
                else{
                    _mounth -= 1;
                }
                dateFrom = $"{_mounth}/{DateTime.Now.Day}/{_year}";
                dateTo = DateTime.Now.ToString("MM/dd/yyyy");
            }

            var getOrdersProvider = DependencyService.Get<GFirestorePurchaseOrderService>();
            var GetOrders = await getOrdersProvider.GetPurchaseOrder(
                pagination,status,vendor,store,dateFrom,dateTo
                );

            List<BasePurchaseReportModel> purchases = new List<BasePurchaseReportModel>();

            foreach (var itemPos in GetOrders)
            {
                foreach (var itemP in itemPos.Purchases)
                {
                    DateTime? expd = null;
                    if(itemP.ExpirationDate != null)
                    {
                        expd = TimeZoneInfo.ConvertTimeFromUtc((DateTime)itemP.ExpirationDate, TimeZoneInfo.Local);
                    }

                    purchases.Add(new BasePurchaseReportModel
                    {
                        ItemId = itemP.ItemId,
                        ItemName = itemP.Name,
                        InternalId = itemP.InternalId,
                        Quantity = itemP.Quantity,
                        ItemBarcode = itemP.Barcode,
                        ImageUrl = itemP.ImagrUrl,  
                        StoreName = itemPos.StoreName,
                        VendorName = itemPos.VendorName,
                        ItemPrice = itemP.Price,
                        ExpirationDate = expd,
                    });
                }
            }

            var groupingPurchase = purchases.GroupBy(p => p.ItemId)
                                            .Select(p => new PurchaseReportModel
                                            {
                                                ItemId = p.Key,
                                                ItemBarcode = p.First().ItemBarcode,
                                                ItemName = p.First().ItemName,
                                                InternalId = p.First().InternalId,
                                                ImageUrl = p.First().ImageUrl,
                                                Quantity = p.Sum(t => t.Quantity),
                                                prices =  p.Select(s=>new StorePrices
                                                {
                                                    VendorName = s.VendorName,
                                                    storeName = s.StoreName,
                                                    price = s.ItemPrice,
                                                }).ToList(),
                                                Expirables = p.GroupBy(ex=> ex.ExpirationDate).Select(exp => exp.First().ExpirationDate).ToList(),
                                            }).ToList();

          List<PurchaseReportModel> LpurchaseReport = new List<PurchaseReportModel>();

         LpurchaseReport.AddRange(groupingPurchase);

            return LpurchaseReport;

        }
    }
}
