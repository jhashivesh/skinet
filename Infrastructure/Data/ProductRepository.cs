using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {

        private readonly StoreContext _context;

        public ProductRepository(StoreContext context)
        {
            _context = context;

        }

        async Task<IReadOnlyList<ProductBrand>> IProductRepository.GetProductBrandsAsync()
        {
            return await _context.ProductBrands.ToListAsync();
        }

        async Task<Product> IProductRepository.GetProductByIdAsync(int id)
        {
            return await _context.Products
                .Include(p=> p.ProductType)
                .Include(p=>p.ProductBrand)
                .FirstOrDefaultAsync(predicate=> predicate.Id == id);
        }

        async Task<IReadOnlyList<Product>> IProductRepository.GetProductsAsync()
        {
            return await _context.Products
                    .Include(p=> p.ProductType)
                    .Include(p=>p.ProductBrand)
                    .ToListAsync();
        }

        async Task<IReadOnlyList<ProductType>> IProductRepository.GetProductsTypeAsync()
        {
            return await _context.ProductTypes.ToListAsync();
        }
    }
}