using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniETicaret.Application.Features.Products.Commands
{
    public class CreateProductCommand : IRequest<Guid> // Geriye oluşturulan ürünün ID'sini dönecek
    {
        public string Name { get; set; } = default!;
        public int Stock { get; set; }
        public decimal Price { get; set; }
    }
}
