using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DollarStore.Common.UploadFiles
{
    public interface IUploadFileService
    {
        Task<string> UploadFile(Stream stream, string SubFoldName, string FileName);
        Task DeleteFile(string url);
    }
}
