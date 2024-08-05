using DollarStore.Common.Core.Bill.Model;
using DollarStore.Common.Core.Bill.Service;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly  : Dependency(typeof(LocalBillService))]
namespace DollarStore.Common.Core.Bill.Service
{
    public class LocalBillService
    {
        private readonly SQLiteAsyncConnection _database;
        public LocalBillService()
        {

            if (_database == null)
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DollarStoreApp.db3");

                _database = new SQLiteAsyncConnection(dbPath);
            }
            _database.CreateTableAsync<PurchaseBillItem>().Wait();

        }


        public Task<int> SavePurchaseBill(PurchaseBillItem purchaseBill)
        {
            return _database.InsertAsync(purchaseBill);
        }

        public Task<List<PurchaseBillItem>> GetPurchaseBills()
        {
            return _database.Table<PurchaseBillItem>().ToListAsync();
        }

        public void DeleteOnePurchaseBill(PurchaseBillItem purchaseBill)
        {
            _database.DeleteAsync(purchaseBill);
        }

        public void DeleteOldPurchaseBills()
        {
            _database.DropTableAsync<PurchaseBillItem>().Wait();
            _database.CreateTableAsync<PurchaseBillItem>().Wait();
        }
    }
}
