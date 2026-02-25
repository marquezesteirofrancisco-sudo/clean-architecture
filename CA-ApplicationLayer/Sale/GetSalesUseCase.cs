using CL_EnterpriseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_ApplicationLayer
{
    public class GetSalesUseCase
    {

        private readonly IRepository<Sale> _repository;

        public GetSalesUseCase(IRepository<Sale> repository)
            => _repository = repository;
       
        public async Task<IEnumerable<Sale>> ExecuteAsync() 
            => await _repository.GetAllAsync();

        public async Task<Sale> ExecuteAsync(int id)
            => await _repository.GetByIdAsync(id);

    }
}
