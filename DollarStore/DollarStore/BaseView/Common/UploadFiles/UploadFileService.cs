using DollarStore.Common.UploadFiles;
using Firebase.Storage;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly : Dependency(typeof(UploadFileService))]
namespace DollarStore.Common.UploadFiles
{
    public class UploadFileService : IUploadFileService
    {
        private readonly string storageId = App.ImgstoragePath;

        public async Task DeleteFile(string url)
        {
            var x = new Uri(url);
            await new FirebaseStorage(storageId)
                  .Child(x.LocalPath).DeleteAsync();
        }

        public async Task<string> UploadFile(Stream streamImage, string SubFoldName, string fileName)
        {

            var storageImage = await new FirebaseStorage(storageId)
                                .Child(SubFoldName)
                                .Child(fileName+".jpg")                               
                                .PutAsync(streamImage);

            return storageImage;
        }
    }
}
