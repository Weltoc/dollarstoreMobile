using DollarStore.Models;
using DollarStore.Modules.Admin.Model;
using DollarStore.Modules.Shipping.Models;
using DollarStore.Modules.SearchItems.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DollarStore.Database
{

    public class LocalDatabase
    {
        private readonly SQLiteAsyncConnection _database;
        public LocalDatabase()
        {
           if(_database == null)
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"DollarStoreApp.db3");

                _database = new SQLiteAsyncConnection(dbPath);
            }
            _database.CreateTableAsync<DOLLARTOFCFA>().Wait();
            _database.CreateTableAsync<UserProfile>().Wait();
            _database.CreateTableAsync<PurchaseItem>().Wait();
            _database.CreateTableAsync<PurchaseHeader>().Wait();
            _database.CreateTableAsync<ShippedPoHeader>().Wait();
            _database.CreateTableAsync<UserRole>().Wait();
            _database.CreateTableAsync<SearchItemsModel>().Wait(); 
            _database.CreateTableAsync<PackedItem>().Wait(); 
            _database.CreateTableAsync<ItemFamily>().Wait(); 
            
        }

        public Task<UserProfile> GetUserProfile()
        {
            return _database.Table<UserProfile>().FirstOrDefaultAsync();
        }

        public Task<int> SaveUserProfile(UserProfile user)
        {
            SaveUserRole(user.Roles);
            return _database.InsertAsync(user);
        }

        public void DeleteUserProfile (UserProfile user)
        {
            _database.DeleteAsync(user);
            DeleteUserRoles();
        }

        public Task<UserRole> GetUserRoles()
        {
            return _database.Table<UserRole>().FirstOrDefaultAsync();
        }

        public Task<List<SearchItemsModel>> GetSearchItems()
        {
            return _database.Table<SearchItemsModel>().ToListAsync();
        }
        public Task<DOLLARTOFCFA> GetDollatToFCFAValue()
        {
            return _database.Table<DOLLARTOFCFA>().FirstOrDefaultAsync();
        }
        public void CreateSearchItems()
        {
            _database.DropTableAsync<SearchItemsModel>().Wait();
            _database.CreateTableAsync<SearchItemsModel>().Wait();
        }
        public void CreateDollarToFCFAValue()
        {
            _database.DropTableAsync<DOLLARTOFCFA>().Wait();
            _database.CreateTableAsync<DOLLARTOFCFA>().Wait();
        }

        public Task<int> SaveSearchItem(List<SearchItemsModel> searchItem)
        {
            CreateSearchItems();
            return _database.InsertAllAsync(searchItem);
        }

        public Task<int> SaveDollarToFCFA(DOLLARTOFCFA dollartofcfa)
        {
            CreateDollarToFCFAValue();
            return _database.InsertAsync(dollartofcfa);
        }
        public Task<int> SaveUserRole(UserRole userRoles)
        {
            return _database.InsertAsync(userRoles);
        }

        public void DeleteUserRoles()
        {
            _database.DropTableAsync<UserRole>().Wait();
            _database.CreateTableAsync<UserRole>().Wait();
        }
        
        public void DeleteDollarToFCFA()
        {
            _database.DropTableAsync<DOLLARTOFCFA>().Wait();
            _database.CreateTableAsync<DOLLARTOFCFA>().Wait();
        }

        public Task<List<PurchaseItem>> GetPurchases()
        {
            return _database.Table<PurchaseItem>().ToListAsync();
        } 
        public Task<List<PurchaseItem>> GetPurchaseslome()
        {
            return _database.Table<PurchaseItem>().ToListAsync();
        }

        public Task<int>SavePurchase(PurchaseItem purchase)
        {
            return _database.InsertAsync(purchase);
        }

        public Task<int>SaveListPurchaseItems(List<PurchaseItem> purchases)
        {
            return _database.InsertAllAsync(purchases);
        }

        public Task<int>UpdatePurchase(PurchaseItem purchase)
        {
            return _database.UpdateAsync(purchase);
        }

        public Task<int> UpdatePurchaseHeader(PurchaseHeader ph)
        {
            return _database.UpdateAsync(ph);
        }


        public async void DeletePurchase(PurchaseItem purchase)
        {
            await _database.DeleteAsync(purchase);
        }  
        
        public async void DeletePurchaseById(int purchaseId)
        {
           await _database.ExecuteAsync("delete from PurchaseItem where id= " + purchaseId);
        }  
        

        public void DeleteOldPurchase()
        {
            _database.DropTableAsync<PurchaseItem>().Wait();
            _database.CreateTableAsync<PurchaseItem>().Wait();
        }
    
        public Task<PurchaseHeader> GetPurchaseHeader()
        {
            return _database.Table<PurchaseHeader>().FirstOrDefaultAsync();
        }

      
        public Task<int> SaveEditinPurchaseHeader(PurchaseHeader ph)
        {
            return _database.InsertOrReplaceAsync(ph);
        }

        public Task<int> SavePurchaseHeader(PurchaseHeader purchaseHeader)
        {
            _database.DropTableAsync<PurchaseHeader>().Wait();
            _database.CreateTableAsync<PurchaseHeader>().Wait();

            return _database.InsertOrReplaceAsync(purchaseHeader);
        }

        public void DeletePurchaseHeader(PurchaseHeader purchaseHeader)
        {
            _database.DeleteAsync(purchaseHeader);
        }

        public void DeletePurchaseHeader()
        {
            _database.DropTableAsync<PurchaseHeader>().Wait();
            _database.CreateTableAsync<PurchaseHeader>().Wait();
        }

        public Task SavePurchaseOrder(ShippedPoHeader po)
        {
            return _database.InsertOrReplaceAsync(po);
        }


        public void CreateItemPacked()
        {
            _database.DropTableAsync<PackedItem>().Wait();
            _database.CreateTableAsync<PackedItem>().Wait();
        } 
        public void CreateItemFamily()
        {
            _database.DropTableAsync<ItemFamily>().Wait();
            _database.CreateTableAsync<ItemFamily>().Wait();
        } 
        public Task<List<PackedItem>> GetItemsPackedItem()
        { 
            return _database.Table<PackedItem>().ToListAsync();
        } 
        public Task<List<ItemFamily>> GetItemsItemFamily()
        { 
            return _database.Table<ItemFamily>().ToListAsync();
        } 

        public void DeleteItemFamily()
        {
            _database.DropTableAsync<ItemFamily>().Wait();
            _database.CreateTableAsync<ItemFamily>().Wait();
        } 
        public void DeletePaketItem()
        {
            _database.DropTableAsync<PackedItem>().Wait();
            _database.CreateTableAsync<PackedItem>().Wait();
        } 

        public Task<int> SaveListPackeItem(List<PackedItem> packeList)
        {
            return _database.InsertAllAsync(packeList);
        }
        public Task<int> SaveListItemFamily(List<ItemFamily> familyList)
        {
            return _database.InsertAllAsync(familyList);
        }

        public Task<int> SaveItemPacked(List<PackedItem> packeList)
        {
            CreateItemPacked();
            return _database.InsertAllAsync(packeList);
        }
        public Task<int> SaveItemFamily(List<ItemFamily> familyList)
        {
            CreateItemFamily();
            return _database.InsertAllAsync(familyList);
        }




    }
}
