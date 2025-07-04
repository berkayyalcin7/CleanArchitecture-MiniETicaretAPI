using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniETicaret.Application.Features.Products.Commands;
using MiniETicaret.Application.Features.Products.Queries;

namespace MiniETicaret.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllProductsQuery();
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductCommand command)
        {
            var productId = await mediator.Send(command);
            // Oluşturulan kaynağın konumunu ve ID'sini döndürmek best practice'dir.
            return CreatedAtAction(nameof(GetAll), new { id = productId }, command);
        }
    }

}
