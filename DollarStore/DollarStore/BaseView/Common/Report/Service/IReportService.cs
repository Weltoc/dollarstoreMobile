using DollarStore.Common.Report.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DollarStore.Common.Report.Service
{
    public interface IReportService
    {
        Task<List<PurchaseReportModel>> GetCurrentPurchaseReport(long pagination, string status=null,
                                        string vendor=null,string store=null,
                                        string dateFrom=null, string dateTo=null);
    }
}
