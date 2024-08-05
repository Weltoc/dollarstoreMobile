
using System.Threading.Tasks;

namespace DollarStore.BaseView.Common.Core.Erors
{
    interface IErrorLogService
    {
        Task saveError(string errorMessage, string errorTrack);
    }
}
