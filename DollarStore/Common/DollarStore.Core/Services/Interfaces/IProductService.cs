using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DollarStore.Core.Services
{
    public interface IProductService
    {
        Task <FirestoreItem> GetItem(string itemId);
        Task <IEnumerable<FirestoreItem>> SearchItems(string SearchText);
        Task CreateItem(FirestoreItem item);

       // Task SeedLocalDataAsync();

        //bool LocalDBExists { get; }

        //bool IsSeeded { get; }

      //  Task SynchronizeCategoriesAsync();

       // Task SynchronizeProductsAsync();

        //Task<IEnumerable<Category>> GetCategoriesAsync(string parentCategoryId = null);

        //Task<IEnumerable<BaseItem>> GetProductsAsync(string categoryId);

        //Task<IEnumerable<Product>> GetAllChildProductsAsync(string topLevelCategoryId);

        //Task<Category> GetTopLevelCategory(string categoryId);

        //Task<Product> GetProductByNameAsync(string productName);

        //Task<IEnumerable<Product>> SearchAsync(string searchTerm);

    }
}
