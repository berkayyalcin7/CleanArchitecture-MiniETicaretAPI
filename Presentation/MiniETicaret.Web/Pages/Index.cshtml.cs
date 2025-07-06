using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace MiniETicaret.Web.Pages
{
    public record ProductDto(Guid Id, string Name, int Stock, decimal Price);

    public class IndexModel(IHttpClientFactory httpClientFactory, IConfiguration configuration) : PageModel
    {
        public List<ProductDto> DisplayedProducts { get; set; } = [];
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        // YENÝ EKLENEN PROPERTY'LER: Hangi sayfa aralýðýný göstereceðimizi belirler.
        public int StartPage { get; set; }
        public int EndPage { get; set; }

        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        public async Task OnGetAsync()
        {
            var httpClient = httpClientFactory.CreateClient();
            var apiBaseUrl = configuration["ApiSettings:BaseUrl"];
            const int pageSize = 10;

            try
            {
                var response = await httpClient.GetAsync($"{apiBaseUrl}/api/products");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var allProducts = JsonSerializer.Deserialize<List<ProductDto>>(content, options) ?? [];

                    TotalPages = (int)Math.Ceiling(allProducts.Count / (double)pageSize);
                    CurrentPage = PageNumber;

                    // Akýllý sayfalama mantýðý
                    int maxPagesToShow = 5; // Ortada gösterilecek maksimum sayfa sayýsý
                    StartPage = CurrentPage - (maxPagesToShow / 2);
                    EndPage = CurrentPage + (maxPagesToShow / 2);

                    if (StartPage < 1)
                    {
                        EndPage = EndPage - (StartPage - 1);
                        StartPage = 1;
                    }

                    if (EndPage > TotalPages)
                    {
                        StartPage = StartPage - (EndPage - TotalPages);
                        EndPage = TotalPages;
                    }

                    if (StartPage < 1) StartPage = 1;


                    DisplayedProducts = allProducts
                        .Skip((PageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"API'ye baðlanýrken hata oluþtu: {ex.Message}");
            }
        }
    }

}
