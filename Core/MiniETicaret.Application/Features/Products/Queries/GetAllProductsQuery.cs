using MediatR;
using MiniETicaret.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniETicaret.Application.Features.Products.Queries
{
    public class GetAllProductsQuery : IRequest<List<ProductDto>>
    {
        // Şu an için bu isteğin bir parametresi yok, o yüzden içi boş.
        // Örneğin filtreleme olsaydı, filtre parametreleri burada özellik olarak yer alırdı.
    }
}
