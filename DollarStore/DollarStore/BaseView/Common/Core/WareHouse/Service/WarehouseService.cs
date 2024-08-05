using DollarStore.Common.Core.WareHouse.Model;
using DollarStore.Common.Core.WareHouse.Service;
using DollarStore.Common.Core.WareHouse.Service.Interface;
using Plugin.CloudFirestore;
using System.Collections.Generic;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(WarehouseService))]
namespace DollarStore.Common.Core.WareHouse.Service
{
    public class WarehouseService : IWarehouseService
    {
        public async Task<List<Warehouse>> GetWarehouse()
        {
            var query = await CrossCloudFirestore.Current.Instance
                        .Collection("warehouses")
                        .GetAsync();

            var model = query.ToObjects<FirestoreWarehouse>();
            List<Warehouse> warehouses = new List<Warehouse>();
            foreach (var item in model)
            {
                Warehouse warehouse = new Warehouse
                {
                    Id = item.Id,
                    Name = item.Name,
                    Location = item.Location
                };
                warehouses.Add(warehouse);
            }

            return warehouses;
        }
    }
}
