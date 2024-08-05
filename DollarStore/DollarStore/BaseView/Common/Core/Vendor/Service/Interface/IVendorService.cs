using DollarStore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DollarStore.Interface
{
    public interface IVendorService
    {
        Task<List<Vendor>> GetVendors();
        Task SaveVendor(Vendor vendor);
        Task SaveShop(string vendorID, Shop shop);
    }
}
