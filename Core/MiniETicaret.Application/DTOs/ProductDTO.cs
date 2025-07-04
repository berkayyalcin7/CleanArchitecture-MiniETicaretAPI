using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniETicaret.Application.DTOs
{
    // .NET 8 ve sonrası için DTO'larda record kullanmak best practice'dir.
    // Veri taşımak için tasarlanmıştır, değişmez (immutable) ve daha az kod gerektirir.
    public record ProductDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public int Stock { get; init; }
        public decimal Price { get; init; }
    }
}
