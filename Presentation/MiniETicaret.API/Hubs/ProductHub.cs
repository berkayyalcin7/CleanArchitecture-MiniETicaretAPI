using Microsoft.AspNetCore.SignalR;

namespace MiniETicaret.API.Hubs
{
    public class ProductHub : Hub
    {
        // Bu hub üzerinden istemcilere mesaj göndereceğiz.
        // Şimdilik içi boş kalabilir, çünkü istemciden sunucuya bir metot çağırmayacağız.
        // Sadece sunucudan istemciye mesaj göndereceğiz.
    }
}
