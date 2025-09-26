using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Transporter.API.Services
{
    public class FakeStoreService
    {
        private readonly HttpClient _httpClient;

        public FakeStoreService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<FakeProduct>> GetProductsAsync()
        {
            var response = await _httpClient.GetAsync("https://fakestoreapi.com/products");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<FakeProduct>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }

    public class FakeProduct
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }
    }
}
