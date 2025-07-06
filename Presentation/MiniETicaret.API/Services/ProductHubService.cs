using Microsoft.AspNetCore.SignalR;
using MiniETicaret.API.Hubs;
using MiniETicaret.Application.DTOs;
using MiniETicaret.Application.Interfaces.Hubs;

namespace MiniETicaret.API.Services
{
    public class ProductHubService(IHubContext<ProductHub> hubContext) : IProductHubService
    {
        // Metot imzasını ve gönderilen veriyi güncelleyin
        public async Task ProductAddedAsync(ProductDto product)
        {
            await hubContext.Clients.All.SendAsync("ReceiveProductAddedMessage", product);
        }
    }
}
