using carvedrock_admin.Data;

namespace carvedrock_admin.Repository
{
    public interface ICarvedRockRepository
    {

        Task<List<Product>> GetAllProductsAsync();
        Task<Product?> GetProductByIdAsync(int productId);
        Task<Product> AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task RemoveProductAsync(int productIdToRemove);
    }
}
