using DollarStore.BaseView.Common.Core.Erors;
using DollarStore.Common.Constants;
using Plugin.CloudFirestore;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(ErrorLogService))]
namespace DollarStore.BaseView.Common.Core.Erors
{
    public class ErrorLogService : IErrorLogService
    {
        public async Task saveError(string errorMessage, string errorTrack)
        {
           await CrossCloudFirestore.Current.Instance.Collection(Constants.Log)
                .AddAsync(new ErrorLog { 
                    CreateAt = DateTime.Now,
                    ErrorTrack = errorTrack,
                    ErrorMessage = errorMessage,
                    Status = false
                });


        }
    }
}

class ErrorLog
{
    public string ErrorMessage { get; set; }
    public string ErrorTrack { get; set; }
    public bool Status { get; set; }
    public DateTime CreateAt { get; set; }
}
