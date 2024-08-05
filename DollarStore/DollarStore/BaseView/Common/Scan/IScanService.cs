using System.Threading.Tasks;
using Xamarin.Forms;

namespace DollarStore.Common.Scan
{
    public interface IScanService
    {
        Task<string> GetScan(INavigation navigation);
    }
}
