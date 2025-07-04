using Microsoft.EntityFrameworkCore;
using MiniETicaret.Application.Interfaces.Repositories;
using MiniETicaret.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniETicaret.Infrastructure.Persistence.Repositories
{
    // AbstractRepository<T> gibi generic bir yapı da kurulabilir. Şimdilik öğrenme odaklı.
    public class ProductRepository(ApplicationDbContext dbContext) : IProductRepository
    {
        public async Task AddAsync(Product product)
        {
            await dbContext.Products.AddAsync(product);
        }

        public void Delete(Product product)
        {
            dbContext.Products.Remove(product);
        }

        public async Task<IReadOnlyList<Product>> GetAllAsync()
        {
            return await dbContext.Products.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await dbContext.Products.FindAsync(id);
        }

        public void Update(Product product)
        {
            dbContext.Products.Update(product);
        }
    }

}
