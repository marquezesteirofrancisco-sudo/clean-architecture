using CA_InterfaceAdapters_Adapters;
using CA_InterfaceAdapters_Adapters.Dtos;
using System.Text.Json;

namespace CA_FrameworksDrivers_ExtenalService
{
    public class PostService : IExternalService<PostServiceDTO>
    {

        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;


        public PostService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<IEnumerable<PostServiceDTO>> GetContentAsync()
        {
            
            var response = await _httpClient.GetAsync("https://jsonplaceholder.typicode.com/posts");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var posts = JsonSerializer.Deserialize<IEnumerable<PostServiceDTO>>(content, _options);

            return posts ?? Enumerable.Empty<PostServiceDTO>();

        }
    }
}
