using CA_ApplicationLayer;
using CA_InterfaceAdapters_Adapters.Dtos;
using CL_EnterpriseLayer;

namespace CA_InterfaceAdapters_Adapters
{
    public class PostExternalServiceAdapter : IExternalServiceAdapter<Post>
    {

        private readonly IExternalService<PostServiceDTO> _externalService;

        public PostExternalServiceAdapter(IExternalService<PostServiceDTO> externalService)
        {
            _externalService = externalService;
        }

        public async Task<IEnumerable<Post>> GetDataAsync()
        {
            
            var postServiceDtos = await _externalService.GetContentAsync();

            var posts = postServiceDtos.Select(dto => new Post
            {
                Id = dto.Id,
                Title = dto.Title,
                Body = dto.Body
            });
            return posts;

        }
    }
}
