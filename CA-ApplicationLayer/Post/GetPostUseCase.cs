using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CL_EnterpriseLayer;

namespace CA_ApplicationLayer
{
    public class GetPostUseCase
    {
        private readonly IExternalServiceAdapter<Post> _adapter;

        public GetPostUseCase(IExternalServiceAdapter<Post> adapter)
        {
            _adapter = adapter;
        }

        public async Task<IEnumerable<Post>> ExecuteAsync()
        {
            return await _adapter.GetDataAsync();
        }
    }
}
