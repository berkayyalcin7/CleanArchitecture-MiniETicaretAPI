using Bogus;
using Microsoft.EntityFrameworkCore;
using MiniETicaret.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniETicaret.Infrastructure.Persistence.Seed
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            // Eğer veritabanında zaten ürün varsa, tohumlamayı yapma.
            if (await context.Products.AnyAsync())
            {
                return;
            }

            // Bogus ile sahte veri üretme kurallarını tanımla
            var productFaker = new Faker<Product>("tr") // 'tr' ile Türkçe'ye yakın veriler üretir
                .RuleFor(p => p.Id, f => f.Random.Guid())
                .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                .RuleFor(p => p.Stock, f => f.Random.Number(1, 1000))
                .RuleFor(p => p.Price, f => f.Finance.Amount(100, 50000))
                .RuleFor(p => p.CreatedDate, f => f.Date.Past(2)); // Son 2 yıl içindeki bir tarih

            // 1000 adet sahte ürün verisi oluştur
            var productList = productFaker.Generate(2000);

            await context.Products.AddRangeAsync(productList);
            await context.SaveChangesAsync();
        }
    }
}
