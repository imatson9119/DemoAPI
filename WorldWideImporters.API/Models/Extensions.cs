using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WideWorldImporters.API.Models
{
#pragma warning disable CS1591
    public static class WideWorldImportersDbContextExtensions
    {
        public static IQueryable<StockItem> GetStockItems(this WideWorldImportersDbContext dbContext, int pageSize = 10, int pageNumber = 1, int? lastEditedBy = null, int? colorID = null, int? outerPackageID = null, int? supplierID = null, int? unitPackageID = null)
        {
            // Get query from DbSet
            var query = dbContext.StockItems.AsQueryable();

            // Filter by: 'LastEditedBy'
            if (lastEditedBy.HasValue)
                query = query.Where(item => item.LastEditedBy == lastEditedBy);

            // Filter by: 'ColorID'
            if (colorID.HasValue)
                query = query.Where(item => item.ColorID == colorID);

            // Filter by: 'OuterPackageID'
            if (outerPackageID.HasValue)
                query = query.Where(item => item.OuterPackageID == outerPackageID);

            // Filter by: 'SupplierID'
            if (supplierID.HasValue)
                query = query.Where(item => item.SupplierID == supplierID);

            // Filter by: 'UnitPackageID'
            if (unitPackageID.HasValue)
                query = query.Where(item => item.UnitPackageID == unitPackageID);

            return query;
        }

        public static async Task<StockItem> GetStockItemsAsync(this WideWorldImportersDbContext dbContext, StockItem entity)
            => await dbContext.StockItems.FirstOrDefaultAsync(item => item.StockItemID == entity.StockItemID);

        public static async Task<StockItem> GetStockItemsByStockItemNameAsync(this WideWorldImportersDbContext dbContext, StockItem entity)
            => await dbContext.StockItems.FirstOrDefaultAsync(item => item.StockItemName == entity.StockItemName);
    }

    public static class IQueryableExtensions
    {
        public static IQueryable<TModel> Paging<TModel>(this IQueryable<TModel> query, int pageSize = 0, int pageNumber = 0) where TModel : class
            => pageSize > 0 && pageNumber > 0 ? query.Skip((pageNumber - 1) * pageSize).Take(pageSize) : query;
    }
    public static class ProductsDbContextExtentions
    {
        public static async Task<Product> GetProductAsync(this ProductsDbContext dbContext, Product entity)
            => await dbContext.Products.FirstOrDefaultAsync(item => item.ID == entity.ID);
        public static async Task<Product> GetProductByProductNameAsync(this ProductsDbContext dbContext, Product entity)
            => await dbContext.Products.FirstOrDefaultAsync(item => item.Name == entity.Name);
        public static IQueryable<Product> GetProducts(this ProductsDbContext dbContext)
        {
            return dbContext.Products.AsQueryable();
        }
    }

#pragma warning restore CS1591
}