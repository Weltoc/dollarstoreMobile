using DollarStore.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DollarStore.Core.Services.Interfaces
{
    public interface IVendorService
    {
        Task<IEnumerable<FirestoreVendor>> GetAllVendors();
        Task CreateVendor(FirestoreVendor vendor);
        Task<FirestoreVendor> GetVendor(string vendorId);
    }
}
