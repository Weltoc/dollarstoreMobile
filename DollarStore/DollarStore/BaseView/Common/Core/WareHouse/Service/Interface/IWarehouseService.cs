using DollarStore.Common.Core.WareHouse.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DollarStore.Common.Core.WareHouse.Service.Interface
{
    public interface IWarehouseService
    {
        Task<List<Warehouse>> GetWarehouse();
    }
}
