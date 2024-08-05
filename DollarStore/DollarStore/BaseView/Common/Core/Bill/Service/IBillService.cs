using DollarStore.Common.Core.Bill.Model;
using System.Threading.Tasks;

namespace DollarStore.Common.Core.Bill.Service
{
    public interface IBillService
    {
        Task SaveBill(FirestoreBill firestoreBill, string purchaseId);
    }
}
